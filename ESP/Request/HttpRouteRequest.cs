namespace ESP.Request
{
    public class HttpRouteRequest
    {
        public int Id {get;set;}
        public string Name {get;set;} = string.Empty;
        public string Code {get;set;} = string.Empty;


        public List<int> ProhibitionCodeIds { get; set; } = new List<int>();

        public List<int> CheckCodeIds { get; set; } = new List<int>();
    }
}
