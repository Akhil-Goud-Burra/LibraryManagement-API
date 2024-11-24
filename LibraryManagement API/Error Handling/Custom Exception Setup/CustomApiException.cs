namespace LibraryManagement_API.Error_Handling.Custom_Exception_Setup
{
    public class CustomApiException : Exception
    {
        public int StatusCode { get; set; }
        public CustomApiException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }

    }
}
