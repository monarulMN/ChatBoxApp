using System.ComponentModel.DataAnnotations;

namespace To_Do_List.Models
{
    public enum PriorityLevel
    {
        Log = 0,
        Medium = 1,
        High = 2
    }
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter valid title")]
        public string Title { get; set; }

        public bool IsDone { get; set; } = false;

        [Required]
        public PriorityLevel Priority { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
