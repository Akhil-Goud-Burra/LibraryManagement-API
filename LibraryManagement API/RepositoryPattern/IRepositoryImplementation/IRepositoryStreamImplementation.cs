using Azure.Core;
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

        public GetAllDTO<Models.Stream[]> GetAll(string baseUrl)
        {
            var query = _appDbContext.Streams;

            return new GetAllDTO<Models.Stream[]>()
            {
                Data = query.ToArray() ,

                Links = new List<DTO.Additional_Context.LinkDTO>
                {
                    new DTO.Additional_Context.LinkDTO($"{baseUrl}/BoardGames", "self", "GET"),
                }
            };
        }
    }
}
