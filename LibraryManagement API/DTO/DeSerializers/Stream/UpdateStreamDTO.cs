using LibraryManagement_API.Custom_Validators.Stream_Validators;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement_API.DTO.DeSerializers.Stream
{
    public class UpdateStreamDTO
    {
        [Required(ErrorMessage = "Enter the Stream Id")]
        [Range(0, 100)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the Stream Name")]
        [MinLength(1)]
        [MaxLength(20)]
        [StreamNameValidator(ErrorMessage = "values not specified in AllowedValuesAttribute")]
        public string Name { get; set; } = null!;
    }
}
