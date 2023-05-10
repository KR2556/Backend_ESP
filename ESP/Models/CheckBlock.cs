namespace ESP.Models
{
    public class CheckBlock
    {
        public int Id { get; set; }
        public string ShortName { get; set; } = null!;
        public int? BlockId { get; set; }
        public Block? Block { get; set; }
        public List<CheckCode> CheckCodes { get; set; } = new List<CheckCode>();
        public List<SubjectType> SubjectTypes { get; set; } = new List<SubjectType>();
        public List<Process> Processes { get; set; } = new List<Process>();
    }
}
