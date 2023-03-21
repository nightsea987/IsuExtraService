namespace Isu.Extra.Tools
{
    public class ScheduleException : Exception
    {
        private ScheduleException(string message)
            : base(message) { }

        public static ScheduleException ViolatedInvariantOfLesson(string message)
        {
            return new ScheduleException(message);
        }

        public static ScheduleException SpecifiedWrongDayOfWeek(string message)
        {
            return new ScheduleException(message);
        }
    }
}
