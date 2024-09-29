using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TelkomLearn.Models
{
    public class UserStreak
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; } 

        public int StreakDays { get; set; } // Current streak days
        public int MaxStreakDays { get; set; } // Initial max streak days

        // Navigation property to Identity User
        public virtual ApplicationUser User { get; set; }
    }
}
