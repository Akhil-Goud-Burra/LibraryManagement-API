using LibraryManagement_API.DTO.DeSerializers.Stream;
using LibraryManagement_API.DTO.Serializers;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement_API.RepositoryPattern.IRepository
{
    public interface IRepositoryStream
    {
        public RestDTO<Models.Stream[]> GetAll_Stream(String baseUrl);

        public RestDTO<Models.Stream?> Create_Stream(String baseUrl , CreateStreamDTO create_stream_dto);

        public RestDTO<Models.Stream?> Update_Stream(String baseUrl, int id, UpdateStreamDTO update_stream_dto);
    }
}
