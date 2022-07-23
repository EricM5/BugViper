using System.ComponentModel.DataAnnotations;

namespace BugViper.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        
        public int Priority { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DueDate { get; set; }
        public int ExpectedHours { get; set; }
        public string PostedBy { get; set; }
    
    }
}
