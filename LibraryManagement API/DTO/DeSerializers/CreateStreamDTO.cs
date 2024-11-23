using LibraryManagement_API.Custom_Validators.Stream_Validators;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement_API.DTO.DeSerializers
{
    public class CreateStreamDTO
    {
        [Required(ErrorMessage = "Enter the Stream Name")]
        [MinLength(1)]
        [MaxLength(10)]
        [StreamNameValidator(ErrorMessage = "values not specified in AllowedValuesAttribute")]
        public string Name { get; set; } = null!;
    }
}
