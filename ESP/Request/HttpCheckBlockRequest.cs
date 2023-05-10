
namespace ESP.Request
{
    public class HttpCheckBlockRequest
    {
        public int Id { get; set; }
        public int BlockId { get; set; }
        public string ShortName { get; set; } = null!;
        public List<string> Subjects { get; set; } = null!;
        public List<int> CheckCodeIds { get; set; } = new List<int>();
    }
}
