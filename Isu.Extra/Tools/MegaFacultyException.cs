namespace Isu.Extra.Tools
{
    public class MegaFacultyException : Exception
    {
        private MegaFacultyException(string message)
            : base(message) { }

        public static MegaFacultyException UnableToAddCourse()
        {
            return new MegaFacultyException("This ognp course is already exist");
        }

        public static MegaFacultyException UnableToAccessCourse()
        {
            return new MegaFacultyException("The course you need does not exist");
        }

        public static MegaFacultyException UnableToEnrollStudent()
        {
            return new MegaFacultyException("The student belongs to this megafaculty");
        }
    }
}
