namespace ESP.Models
{
    public class ProhibitionCode
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CheckCodeId { get; set; }
        public CheckCode? CheckCode { get; set; } = null!;

        public List<Process> Processes = new List<Process>();
        public List<Route> Routes { get; set; } = new List<Route>();
        
        
    }
}
