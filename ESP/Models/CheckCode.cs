namespace ESP.Models
{
    public class CheckCode : IModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public bool IsActive { get; set; }
        public List<ProhibitionCode> ProhibitionCodes { get; set; } = new List<ProhibitionCode>();
        public List<CheckBlock> CheckBlocks { get; set; } = new List<CheckBlock>();
        public List<Process> Processes { get; set; } = new List<Process>();
        public List<Route> Routes { get; set; } = new List<Route>();
    }
}
