namespace ESP.Models
{
    public class Block
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<CheckBlock> CheckBlocks { get; set; } = new List<CheckBlock>();
    }
}
