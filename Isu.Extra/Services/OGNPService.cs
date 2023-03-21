using Isu.Extra.Entities;

namespace Isu.Extra.Services
{
    public class OgnpService : IOgnpService
    {
        private List<MegaFaculty> _listOfMegaFaculties;
        public OgnpService()
        {
            _listOfMegaFaculties = new List<MegaFaculty>();
        }

        public IReadOnlyList<MegaFaculty> ListOfMegaFaculties => _listOfMegaFaculties.AsReadOnly();

        public MegaFaculty AddMegaFaculty(string nameOfMegaFaculty, IReadOnlyList<char> lettersOfMegaFaculty)
        {
            string.IsNullOrWhiteSpace(nameOfMegaFaculty);
            ArgumentNullException.ThrowIfNull(nameof(lettersOfMegaFaculty));
            var megaFaculty = new MegaFaculty(nameOfMegaFaculty, lettersOfMegaFaculty);
            return AddMegaFaculty(megaFaculty);
        }

        public OgnpCourse AddCourseToMegaFaculty(MegaFaculty megaFaculty, string nameOfOgnpCourse)
        {
            ArgumentNullException.ThrowIfNull(nameof(megaFaculty));
            string.IsNullOrWhiteSpace(nameOfOgnpCourse);

            return megaFaculty.AddOGNPCourse(nameOfOgnpCourse);
        }

        public MegaFaculty AddMegaFaculty(MegaFaculty megaFaculty)
        {
            ArgumentNullException.ThrowIfNull(nameof(megaFaculty));
            if (_listOfMegaFaculties.Any(m => m.NameOfMegaFaculty == megaFaculty.NameOfMegaFaculty))
                throw new Exception();
            _listOfMegaFaculties.Add(megaFaculty);
            return megaFaculty;
        }

        public MegaFaculty? FindMegaFaculty(string nameOfMegaFaculty)
        {
            string.IsNullOrWhiteSpace(nameOfMegaFaculty);
            return _listOfMegaFaculties.SingleOrDefault(s => s.NameOfMegaFaculty == nameOfMegaFaculty);
        }

        public IReadOnlyList<ExtendedStudent> FindStudents(OgnpGroup ognpGroup)
        {
            ArgumentNullException.ThrowIfNull(nameof(ognpGroup));

            return (IReadOnlyList<ExtendedStudent>)_listOfMegaFaculties
                .SelectMany(s => s.ListOfOGNPCourses)
                .SelectMany(s => s.ListOfOgnpGroups)
                .Where(g => g.NameOfOGNPGroup == ognpGroup.NameOfOGNPGroup)
                .SelectMany(s => s.ListOfStudents);
        }

        public IReadOnlyList<OgnpGroup> FindFlow(OgnpCourse ognpCourse)
        {
            ArgumentNullException.ThrowIfNull(nameof(ognpCourse));

            return (IReadOnlyList<OgnpGroup>)_listOfMegaFaculties
                .SelectMany(s => s.ListOfOGNPCourses)
                .Where(g => g.NameOfOgnpCourse == ognpCourse.NameOfOgnpCourse);
        }

        public void AddStudentToOgnpCourse(ExtendedStudent extendedStudent, string nameOfOgnpCourse)
        {
            ArgumentNullException.ThrowIfNull(nameof(extendedStudent));
            string.IsNullOrWhiteSpace(nameOfOgnpCourse);
            OgnpCourse? ognpCourse = _listOfMegaFaculties
                .SelectMany(m => m.ListOfOGNPCourses)
                .SingleOrDefault(c => c.NameOfOgnpCourse == nameOfOgnpCourse);
            if (ognpCourse == null)
                throw new Exception();
            ognpCourse.AddToOgnpCourse(extendedStudent);
        }

        public MegaFaculty GetMegaFacultyFromOgnpGroup(OgnpGroup ognpGroup)
        {
            MegaFaculty? megaFaculty = _listOfMegaFaculties
                .SingleOrDefault(m => m.ListOfOGNPCourses
                .Any(s => s.ListOfOgnpGroups
                .Any(g => g.NameOfOGNPGroup == ognpGroup.NameOfOGNPGroup)));
            if (megaFaculty == null)
                throw new Exception();
            return megaFaculty;
        }
    }
}
