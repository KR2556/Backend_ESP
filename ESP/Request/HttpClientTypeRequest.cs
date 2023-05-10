namespace ESP.Request
{
    public class HttpClientTypeRequest
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<int> ClientTypeIds = new List<int>();
    }
}
