using System.ComponentModel.DataAnnotations;

namespace LibraryManagement_API.DTO.DeSerializers
{
    public class CreateStreamDTO
    {
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public string Name { get; set; } = null!;
    }
}
