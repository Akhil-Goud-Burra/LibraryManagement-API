using System.ComponentModel.DataAnnotations;

namespace LibraryManagement_API.Custom_Validators.Stream_Validators
{
    public class StreamNameValidator : ValidationAttribute
    {
        private readonly string[] _allowedValues;

        public StreamNameValidator()
        {
            _allowedValues = new[] { "cse", "eee", "it", "datasciece"};
        }

        public override bool IsValid(object? stream_name)
        {
            if (stream_name != null && stream_name.GetType() == typeof(string))
            {
                var StreamName = (string)stream_name; // Explicit casting

                return _allowedValues.Contains(StreamName.ToLower());
            }
            else return false;
        }
    }
}
