using Isu.Extra.Tools;

namespace Isu.Extra.Models
{
    public record LessonTime
    {
        public const int LowerLimitOfHours = 8;
        public const int UpperLimitOfHours = 21;
        public const int LowerLimitOfMinutes = 0;
        public const int UpperLimitOfMinutes = 59;
        public const int LengthOfTimeFormat = 5;
        public LessonTime(string time)
        {
            string.IsNullOrWhiteSpace(time);
            Validate(time);
            Time = time;
        }

        public string Time { get; }
        public static bool operator >(LessonTime time1, LessonTime time2)
        {
            ArgumentNullException.ThrowIfNull(time1);
            ArgumentNullException.ThrowIfNull(time2);
            return GetTimeInMinutes(time1.Time) > GetTimeInMinutes(time2.Time);
        }

        public static bool operator <(LessonTime time1, LessonTime time2)
        {
            ArgumentNullException.ThrowIfNull(time1);
            ArgumentNullException.ThrowIfNull(time2);
            return GetTimeInMinutes(time1.Time) < GetTimeInMinutes(time2.Time);
        }

        public static bool IsMatchedToTimeFormat(string time)
        {
            string.IsNullOrWhiteSpace(time);
            return char.IsDigit(time[0]) && char.IsDigit(time[1]) && char.IsDigit(time[3]) && char.IsDigit(time[4]) && time[2] == ':';
        }

        public static int GetHours(string time)
        {
            string.IsNullOrWhiteSpace(time);
            return int.Parse(time.Substring(0, 2));
        }

        public static int GetMinutes(string time)
        {
            string.IsNullOrWhiteSpace(time);
            return int.Parse(time.Substring(3, 2));
        }

        public static void Validate(string time)
        {
            string.IsNullOrWhiteSpace(time);
            if (time.Length != LengthOfTimeFormat)
                throw LessonTimeException.NotMatchTheFormat("The length of time format should be 5");

            if (!IsMatchedToTimeFormat(time))
                throw LessonTimeException.NotMatchTheFormat("The format of time lesson - NN:NN");

            if (GetHours(time) > UpperLimitOfHours || GetHours(time) < LowerLimitOfHours)
                throw LessonTimeException.TimeIsOutOfBounds($"The bounds of hours of lesson from {LowerLimitOfHours} to {UpperLimitOfHours}");

            if (GetMinutes(time) > UpperLimitOfMinutes || GetMinutes(time) < LowerLimitOfMinutes)
                throw LessonTimeException.TimeIsOutOfBounds($"The bounds of minutes of lesson from {LowerLimitOfMinutes} to {UpperLimitOfMinutes}");
        }

        public static int GetTimeInMinutes(string time)
        {
            string.IsNullOrWhiteSpace(time);
            int minutesInHour = 60;
            return (GetHours(time) * minutesInHour) + GetMinutes(time);
        }
    }
}
