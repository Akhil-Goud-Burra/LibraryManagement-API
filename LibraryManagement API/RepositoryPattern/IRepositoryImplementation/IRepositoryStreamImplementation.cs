using Azure.Core;
using LibraryManagement_API.DTO.DeSerializers.Stream;
using LibraryManagement_API.DTO.Serializers;
using LibraryManagement_API.Error_Handling.Custom_Exception_Setup;
using LibraryManagement_API.Models;
using LibraryManagement_API.RepositoryPattern.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.IO;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryManagement_API.RepositoryPattern.IRepositoryImplementation
{
    public class IRepositoryStreamImplementation : IRepositoryStream
    {
        private readonly MyDbContext _appDbContext;

        public IRepositoryStreamImplementation(MyDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }


        public RestDTO<Models.Stream?> Create_Stream(string baseUrl, CreateStreamDTO model)
        {
            // Validating Incomming Request Attributes
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new CustomApiException("BadRequest: Stream name cannot be null or empty.", 400);
            }

            var Given_Stream_Name = model.Name.Trim().ToLower();

            // Check if a stream with the same name already exists
            var Created_Stream = _appDbContext.Streams.Any(stream => stream.Name == Given_Stream_Name);

            if (Created_Stream == false) // If Stream Not Exists
            {
                // If no stream exists, create a new one
                var New_Stream = new Models.Stream
                {
                    Name = Given_Stream_Name,
                };

                _appDbContext.Streams.Add(New_Stream);

                try 
                { 
                    _appDbContext.SaveChanges();  
                } 
                catch(Exception) 
                { 
                    throw new CustomApiException("BadRequest: Error saving the stream to the database. Please try again later", 400); 
                }

                return new RestDTO<Models.Stream?>()
                {
                    Data = New_Stream,

                    Links = new List<DTO.Additional_Context.LinkDTO>
                    {
                        new DTO.Additional_Context.LinkDTO($"{baseUrl}/api/StreamController", "self", "POST"),
                    }
                };
            }
            else // If Stream Exists
            {
                throw new CustomApiException("Conflict: The requested data already exists and cannot be created again.", 409);
            }
        }


        public RestDTO<Models.Stream[]> GetAll_Stream(string baseUrl)
        {
            // Check if the collection contains any records
            var Fetch_Data = _appDbContext.Streams.Any();

            if (Fetch_Data)
            {
                return new RestDTO<Models.Stream[]>()
                {
                    Data = _appDbContext.Streams.ToArray(),

                    Links = new List<DTO.Additional_Context.LinkDTO>
                    {
                        new DTO.Additional_Context.LinkDTO($"{baseUrl}/api/StreamController", "self", "GET"),
                    }
                };
            }
            else
            {
                throw new CustomApiException("Success, No Content: The Requested Collection is Empty", 204);
            }
        }



        

        public RestDTO<Models.Stream?> Update_Stream(string baseUrl, int id, UpdateStreamDTO model)
        {
            if (id != model.Id)
            {
                throw new CustomApiException("BadRequest: Provided Id didn't matched", 400);
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new CustomApiException("BadRequest: Stream name cannot be null or empty.", 400);
            }
         
            // Checking Stream Id Exist in the Databse or Not
            var Stream_To_Update = _appDbContext.Streams.SingleOrDefault(stream => stream.Id == id );
            if (Stream_To_Update == null)
            {
                throw new CustomApiException("NotFound: The stream you are trying to update does not exist.", 404);
            }

            // Checking Stream Name Exist in the Database or Not
            var Given_Stream_Name = model.Name.Trim().ToLower();

            bool Stream_Name_Existence = _appDbContext.Streams.Any(stream => stream.Name == Given_Stream_Name);

            if (Stream_Name_Existence)
            {
                throw new CustomApiException("BadRequest: The stream you are trying to update already exist.", 400);
            }

            try
            {
                Stream_To_Update.Name = Given_Stream_Name;

                _appDbContext.SaveChanges();

                // Return the updated resource
                return new RestDTO<Models.Stream?>()
                {
                    Data = Stream_To_Update,
                    Links = new List<DTO.Additional_Context.LinkDTO>
                    {
                        new DTO.Additional_Context.LinkDTO($"/api/StreamController/{id}", "self", "PUT"),
                    }
                };
            }
            catch (DbUpdateException)
            {
                throw new CustomApiException("BadRequest in Update_Stream: Error while updating the stream. Please try again later.", 400);
            }
            
        }



        public RestDTO<Models.Stream?> Delete_Stream(string baseUrl, int id, DeleteStreamDTO model)
        {
            if (id != model.Id)
            {
                throw new CustomApiException("BadRequest: Provided Id didn't matched", 400);
            }

            // Checking Stream Id Exist in the Databse or Not If Exist, it pulls Data
            var Stream_To_Delete = _appDbContext.Streams.FirstOrDefault(stream => stream.Id == id);
            if (Stream_To_Delete == null)
            {
                throw new CustomApiException("NotFound: The stream you are trying to delete does not exist.", 404);
            }

            try
            {
                // Remove the stream from the database
                _appDbContext.Streams.Remove(Stream_To_Delete);

                // Save changes to the database
                _appDbContext.SaveChanges();

                // Return the updated resource
                return new RestDTO<Models.Stream?>()
                {
                    Data = Stream_To_Delete,
                    Links = new List<DTO.Additional_Context.LinkDTO>
                    {
                        new DTO.Additional_Context.LinkDTO($"/api/StreamController/{id}", "self", "PUT"),
                    }
                }; 
            }
            catch(DbUpdateException)
            {
                throw new CustomApiException("BadRequest: Databse Error, Trying to delete but unable to delete the resource", 400);
            }
        }


    }
}
