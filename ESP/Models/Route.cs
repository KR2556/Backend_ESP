namespace ESP.Models
{
    public class Route : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        

        public List<CheckCode> CheckCodes { get; set; } = new List<CheckCode>();
        public List<ProhibitionCode> ProhibitionCodes { get; set; } = new List<ProhibitionCode>();
    }
}
