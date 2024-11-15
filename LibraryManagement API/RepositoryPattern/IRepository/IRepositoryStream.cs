using LibraryManagement_API.DTO.Serializers;

namespace LibraryManagement_API.RepositoryPattern.IRepository
{
    public interface IRepositoryStream
    {
        public GetAllDTO<Models.Stream[]> GetAll(String baseUrl);
    }
}
