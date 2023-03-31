namespace TestApi.DAL.Response
{
    public class ResponseErrorMessages
    {
        public int StatusCode { get; set; }
        public bool TaskStatus { get; set; } = false;
        public string? Message { get; set; }
        public List<ErrorModel> FieldError { get; set; } = new List<ErrorModel>();
    }
    public class ErrorModel
    {
        public string? FieldName { get; set; }
        public string? Message { get; set; }
    }
}
