namespace project.Helpers
{
    public static class TimeHelper
    {
        public static string FormatDuration(int duration)
        {
            if (duration < 60)
                return $"{duration} minutes";
            else
            {
                int hours = duration / 60;
                int minutes = duration % 60;
                return minutes == 0 ? $"{hours} hour(s)" : $"{hours} hour(s) and {minutes} minute(s)";
            }
        }
    }
}
