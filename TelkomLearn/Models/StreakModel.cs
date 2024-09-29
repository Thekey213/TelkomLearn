namespace TelkomLearn.Models
{
    public class StreakModel
    {
        public int StreakDays { get; set; } // Current streak days
        public int MaxStreakDays { get; set; } // Maximum streak days

        // Calculate the percentage of the health bar
        public int StreakPercentage => ((MaxStreakDays - StreakDays) * 100) / MaxStreakDays;
    }
}
