namespace ESP.Models
{
    public class ClientType
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<SubjectType> SubjectTypes = new List<SubjectType>();

    }
}
