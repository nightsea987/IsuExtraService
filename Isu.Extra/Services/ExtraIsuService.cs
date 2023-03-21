using Isu.Extra.Entities;
using Isu.Models;

namespace Isu.Extra.Services
{
    public class ExtraIsuService
    {
        public ExtraIsuService()
        {
            ExtendedIsuService = new ExtendedIsuService();
            OgnpService = new OgnpService();
        }

        public ExtendedIsuService ExtendedIsuService { get; }
        public OgnpService OgnpService { get; }

        public IReadOnlyList<ExtendedStudent> GetNotSignedUpStudents(GroupName groupName)
        {
            ExtendedGroup? group = ExtendedIsuService.FindGroup(groupName);
            if (group == null)
                throw new Exception();
            return (IReadOnlyList<ExtendedStudent>)group.ListOfExtendedStudents.Where(s => s.ListOfOgnpGroups.Count == 0);
        }

        public void ChangeStudentGroup(ExtendedStudent student, ExtendedGroup newGroup)
        {
            ArgumentNullException.ThrowIfNull(nameof(student));
            ArgumentNullException.ThrowIfNull(nameof(newGroup));
            student.ChangeStudentGroup(newGroup);
            MegaFaculty firstMegaFaculty = OgnpService.GetMegaFacultyFromOgnpGroup(student.ListOfOgnpGroups[0]);
            MegaFaculty lastMegaFaculty = OgnpService.GetMegaFacultyFromOgnpGroup(student.ListOfOgnpGroups[1]);

            if (firstMegaFaculty.ContainsLetterOfFaculty(newGroup.Group.NameOfGroup.NameOfFaculty))
                student.RemoveStudentFromOgnp(student.ListOfOgnpGroups[0]);
            if (lastMegaFaculty.ContainsLetterOfFaculty(newGroup.Group.NameOfGroup.NameOfFaculty))
                student.RemoveStudentFromOgnp(student.ListOfOgnpGroups[1]);
        }
    }
}
