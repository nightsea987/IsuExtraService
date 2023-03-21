using Isu.Extra.Models;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities
{
    public class MegaFaculty
    {
        private List<OgnpCourse> _listOfOGNPCourses;
        public MegaFaculty(string nameOfMegaFaculty, IReadOnlyList<char> lettersOfMegaFaculty)
        {
            string.IsNullOrWhiteSpace(nameOfMegaFaculty);
            ArgumentNullException.ThrowIfNull(nameof(lettersOfMegaFaculty));
            _listOfOGNPCourses = new List<OgnpCourse>();
            NameOfMegaFaculty = nameOfMegaFaculty;
            LettersOfMegaFaculty = lettersOfMegaFaculty;
        }

        public string NameOfMegaFaculty { get; }
        public IReadOnlyList<OgnpCourse> ListOfOGNPCourses => _listOfOGNPCourses.AsReadOnly();
        public IReadOnlyList<char> LettersOfMegaFaculty { get; }

        public bool ValidateIfCourseInMegaFaculty(OgnpCourse ognpCourse)
        {
            return _listOfOGNPCourses.Any(c => c.NameOfOgnpCourse == ognpCourse.NameOfOgnpCourse);
        }

        public bool ValidateIfCourseNotInMegaFaculty(OgnpCourse ognpCourse)
        {
            return _listOfOGNPCourses.All(c => c.NameOfOgnpCourse != ognpCourse.NameOfOgnpCourse);
        }

        public OgnpCourse AddOGNPCourse(string nameOfCourse)
        {
            string.IsNullOrWhiteSpace(nameOfCourse);
            var course = new OgnpCourse(nameOfCourse);
            if (ValidateIfCourseInMegaFaculty(course))
                throw MegaFacultyException.UnableToAddCourse();
            _listOfOGNPCourses.Add(course);
            return course;
        }

        public void AddOGNPCourse(OgnpCourse ognpCourse)
        {
            ArgumentNullException.ThrowIfNull(nameof(ognpCourse));
            if (ValidateIfCourseInMegaFaculty(ognpCourse))
                throw MegaFacultyException.UnableToAddCourse();
            _listOfOGNPCourses.Add(ognpCourse);
        }

        public void AddStudentToCourse(ExtendedStudent student, OgnpCourse ognpCourse)
        {
            ArgumentNullException.ThrowIfNull(nameof(ognpCourse));
            ArgumentNullException.ThrowIfNull(nameof(student));

            if (ValidateIfCourseNotInMegaFaculty(ognpCourse))
                throw MegaFacultyException.UnableToAccessCourse();

            if (LettersOfMegaFaculty.Contains(student.OldStudent.StudentGroup.NameOfGroup.NameOfFaculty))
                throw MegaFacultyException.UnableToEnrollStudent();
            ognpCourse.AddToOgnpCourse(student);
        }

        public void AddOgnpGroupToCourse(OgnpCourse ognpCourse, string nameOfOgnpGroup, Schedule schedule)
        {
            string.IsNullOrWhiteSpace(nameOfOgnpGroup);
            ArgumentNullException.ThrowIfNull(nameof(schedule));
            ArgumentNullException.ThrowIfNull(nameof(ognpCourse));

            var newOgnpGroup = new OgnpGroup(new OgnpGroupName(nameOfOgnpGroup), schedule);
            if (ValidateIfCourseNotInMegaFaculty(ognpCourse))
                throw MegaFacultyException.UnableToAccessCourse();
            ognpCourse.AddOgnpGroup(newOgnpGroup);
        }

        public bool ContainsLetterOfFaculty(char letter)
        {
            ArgumentNullException.ThrowIfNull(nameof(letter));
            return LettersOfMegaFaculty.Any(l => l == letter);
        }
    }
}
