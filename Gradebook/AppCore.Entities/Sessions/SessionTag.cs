using System.ComponentModel.DataAnnotations;

namespace AppCore.Entities
{
    public class SessionTag
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        [MaxLength(10)]
        public string Tag { get; set; }
    }
}