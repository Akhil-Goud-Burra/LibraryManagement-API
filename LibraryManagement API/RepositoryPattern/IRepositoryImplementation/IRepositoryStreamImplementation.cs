using Azure.Core;
using LibraryManagement_API.DTO.DeSerializers;
using LibraryManagement_API.DTO.Serializers;
using LibraryManagement_API.Models;
using LibraryManagement_API.RepositoryPattern.IRepository;
using System;

namespace LibraryManagement_API.RepositoryPattern.IRepositoryImplementation
{
    public class IRepositoryStreamImplementation : IRepositoryStream
    {
        private readonly MyDbContext _appDbContext;

        public IRepositoryStreamImplementation(MyDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }

        public GetAllDTO<Models.Stream?> Create_Stream(string baseUrl, CreateStreamDTO model)
        {

            // Check if a stream with the same name already exists
            var Created_Stream = _appDbContext.Streams
                                    .Where(stream => stream.Name == model.Name)
                                    .FirstOrDefault();

            // Is Stream Not Exists
            if (Created_Stream == null)
            {
                // If no stream exists, create a new one
                Created_Stream = new Models.Stream
                {
                    Name = model.Name,
                };

                _appDbContext.Streams.Add(Created_Stream);
                _appDbContext.SaveChanges();
            };

            return new GetAllDTO<Models.Stream?>()
            {
                Data = Created_Stream,

                Links = new List<DTO.Additional_Context.LinkDTO>
                {
                    new DTO.Additional_Context.LinkDTO($"{baseUrl}/api/StreamController", "self", "POST"),
                }
            };

        }

        public GetAllDTO<Models.Stream[]> GetAll_Stream(string baseUrl)
        {
            var query = _appDbContext.Streams;

            return new GetAllDTO<Models.Stream[]>()
            {
                Data = query.ToArray() ,

                Links = new List<DTO.Additional_Context.LinkDTO>
                {
                    new DTO.Additional_Context.LinkDTO($"{baseUrl}/api/StreamController", "self", "GET"),
                }
            };
        }
    }
}
