using LibraryManagement_API.DTO.DeSerializers;
using LibraryManagement_API.DTO.Serializers;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement_API.RepositoryPattern.IRepository
{
    public interface IRepositoryStream
    {
        public GetAllDTO<Models.Stream[]> GetAll_Stream(String baseUrl);

        public GetAllDTO<Models.Stream?> Create_Stream(String baseUrl , CreateStreamDTO create_stream_dto);
    }
}
