namespace ESP.Response
{
    public class BaseResponse
    {
        public string Message { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
        public object Body { get; set; } = null!;
    }
}
