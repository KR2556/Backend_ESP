namespace ESP.Request
{
    public class HttpProcessRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<int> CheckBlockIds { get; set; } = new List<int>();

        public List<int> CheckCodeIds { get; set; } = new List<int>();
        public List<int> SubjectIds { get; set; } = new List<int>();
        public List<int> ProhibitionCodeIds { get; set; } = new List<int>();

    }
}
