namespace AppCore.Entities
{
    public class Talk
    {
        public int TalkId { get; set; }
        public virtual Camp Camp { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int Level { get; set; }
        public virtual Speaker Speaker { get; set; }
    }
}