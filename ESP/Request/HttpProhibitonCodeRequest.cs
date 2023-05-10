namespace ESP.Request
{
    public class HttpProhibitonCodeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }

        public string StartDate { get; set; } = null!;
        public string? EndDate { get; set; }
        public int CheckCodeId { get; set; }
    }
}
