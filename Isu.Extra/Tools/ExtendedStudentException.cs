namespace Isu.Extra.Tools
{
    public class ExtendedStudentException : Exception
    {
        private ExtendedStudentException(string message)
            : base(message) { }

        public static ExtendedStudentException ExceededMaxNumberOfOgnpGroups(int maxCountOfGroups)
        {
            return new ExtendedStudentException($"Student can choose only {maxCountOfGroups} courses");
        }
    }
}