namespace ESP.Request
{
    public class HttpCheckRequest
    {
        public List<int> Subjects { get; set; } = null!;
        public bool? IsNewClient { get; set; }
    }
}
