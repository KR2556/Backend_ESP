namespace ESP.Models
{
    public class SubjectType : IModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<CheckBlock> CheckBlocks { get; set; } = new List<CheckBlock>();

        public List<Process> Processes { get; set; } = new List<Process>();
        public List<ClientType> ClientTypes { get; set; } = new List<ClientType>();
    }
}
