namespace LibraryManagement_API.Custom_Error_Responses
{
    public class ErrorResponse
    {
        public string? Message { get; set; }
        public string? Detail { get; set; }
        public string? TraceId { get; set; }
    }
}
