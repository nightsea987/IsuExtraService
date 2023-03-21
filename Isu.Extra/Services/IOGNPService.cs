using Isu.Extra.Entities;

namespace Isu.Extra.Services
{
    public interface IOgnpService
    {
        MegaFaculty AddMegaFaculty(string nameOfMegaFaculty, IReadOnlyList<char> lettersOfMegaFaculty);

        OgnpCourse AddCourseToMegaFaculty(MegaFaculty megaFaculty, string nameOfOgnpCourse);

        MegaFaculty AddMegaFaculty(MegaFaculty megaFaculty);

        MegaFaculty? FindMegaFaculty(string nameOfMegaFaculty);

        IReadOnlyList<ExtendedStudent> FindStudents(OgnpGroup ognpGroup);

        IReadOnlyList<OgnpGroup> FindFlow(OgnpCourse ognpCourse);

        void AddStudentToOgnpCourse(ExtendedStudent extendedStudent, string nameOfOgnpCourse);
    }
}
