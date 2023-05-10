using ESP.Models;

namespace ESP.Request
{
    public class HttpBlockRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
