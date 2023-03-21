using Isu.Entities;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities
{
    public class ExtendedStudent
    {
        public const int MaxCountOfOgnpGroups = 2;
        private List<OgnpGroup> _listOfOgnpGroups;
        public ExtendedStudent(Student student, ExtendedGroup extendedGroup)
        {
            ArgumentNullException.ThrowIfNull(student);
            ArgumentNullException.ThrowIfNull(extendedGroup);

            OldStudent = student;
            Group = extendedGroup;
            _listOfOgnpGroups = new List<OgnpGroup>();
        }

        public Student OldStudent { get; }
        public ExtendedGroup Group { get; }
        public IReadOnlyList<OgnpGroup> ListOfOgnpGroups => _listOfOgnpGroups.AsReadOnly();
        public void ChangeStudentGroup(ExtendedGroup newGroup)
        {
            OldStudent.ChangeStudentGroup(newGroup.Group);
        }

        public void RemoveStudentFromOgnp(OgnpGroup ognpGroup)
        {
            ArgumentNullException.ThrowIfNull(ognpGroup);
            _listOfOgnpGroups.Remove(ognpGroup);
        }

        internal void JoinOgnpGroup(OgnpGroup ognpGroup)
        {
            ArgumentNullException.ThrowIfNull(ognpGroup);
            if (_listOfOgnpGroups.Count >= MaxCountOfOgnpGroups)
                throw ExtendedStudentException.ExceededMaxNumberOfOgnpGroups(MaxCountOfOgnpGroups);
            _listOfOgnpGroups.Add(ognpGroup);
        }
    }
}
