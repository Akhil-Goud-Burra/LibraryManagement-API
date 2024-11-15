using LibraryManagement_API.DTO.Additional_Context;

namespace LibraryManagement_API.DTO.Serializers
{
    public class GetAllDTO<T>
    {
        public T Data { get; set; } = default!;

        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}
