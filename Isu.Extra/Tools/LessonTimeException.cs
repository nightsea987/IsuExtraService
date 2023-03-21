namespace Isu.Extra.Tools
{
    public class LessonTimeException : Exception
    {
        private LessonTimeException(string message)
            : base(message) { }

        public static LessonTimeException NotMatchTheFormat(string message)
        {
            return new LessonTimeException(message);
        }

        public static LessonTimeException TimeIsOutOfBounds(string message)
        {
            return new LessonTimeException(message);
        }
    }
}
