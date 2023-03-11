namespace Education.EFCoreExample.Data.Entities
{
    public class Blog : IBlog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Post> Posts { get; } = new();
    }

    public interface IBlog {
        int Id { get; set; }
    }
}
