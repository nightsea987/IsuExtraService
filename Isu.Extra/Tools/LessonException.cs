namespace Isu.Extra.Tools
{
    public class LessonException : Exception
    {
        private LessonException(string message)
            : base(message) { }

        public static LessonException ViolatedInvariantOfLesson(string message)
        {
            return new LessonException(message);
        }

        public static LessonException SpecifiedWrongDayOfWeek(string message)
        {
            return new LessonException(message);
        }
    }
}
