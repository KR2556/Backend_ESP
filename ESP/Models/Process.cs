namespace ESP.Models
{
    public class Process
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<CheckBlock> CheckBlocks { get; set; } = new List<CheckBlock>();
        public List<CheckCode> CheckCodes { get; set; } = new List<CheckCode>();
        public List<ProhibitionCode> ProhibitionCodes { get; set; } = new List<ProhibitionCode>();
        public List<SubjectType> SubjectTypes { get; set; } = new List<SubjectType>();

    }
}
