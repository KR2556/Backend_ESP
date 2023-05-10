namespace ESP.Request
{
    public class HttpCheckCodeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
