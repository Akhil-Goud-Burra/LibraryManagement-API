using System.ComponentModel.DataAnnotations;

namespace LibraryManagement_API.DTO.DeSerializers.Stream
{
    public class DeleteStreamDTO
    {
        [Required(ErrorMessage = "Enter the Stream Id")]
        [Range(0, 100)]
        public int Id { get; set; }
    }
}
