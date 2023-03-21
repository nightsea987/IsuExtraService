using Isu.Entities;
using Isu.Tools;

namespace Isu.Extra.Entities
{
    public class ExtendedGroup
    {
        private List<ExtendedStudent> _listOfExtendedStudents;
        public ExtendedGroup(Group group, Schedule schedule)
        {
            ArgumentNullException.ThrowIfNull(nameof(group));
            ArgumentNullException.ThrowIfNull(nameof(schedule));
            ScheduleOfGroup = schedule;
            _listOfExtendedStudents = new List<ExtendedStudent>();
            Group = group;
        }

        public Group Group { get; }
        public Schedule ScheduleOfGroup { get; }

        public IReadOnlyList<ExtendedStudent> ListOfExtendedStudents => _listOfExtendedStudents.AsReadOnly();

        public int GetMaxNumberOfStudents()
        {
            return Group.GetMaxNumberOfStudents();
        }

        public void DeleteFromGroup(ExtendedStudent extendedStudent)
        {
            ArgumentNullException.ThrowIfNull(nameof(extendedStudent));
            Group.DeleteFromGroup(extendedStudent.OldStudent);

            if (_listOfExtendedStudents.Find(x => x.OldStudent.Id == extendedStudent.OldStudent.Id) == null)
                throw new StudentNotFoundException("Cannot delete student because he's not found");

            _listOfExtendedStudents.Remove(extendedStudent);
        }

        public void AddToGroup(ExtendedStudent extendedStudent)
        {
            ArgumentNullException.ThrowIfNull(nameof(extendedStudent));
            _listOfExtendedStudents.Add(extendedStudent);
            Group.AddToGroup(extendedStudent.OldStudent);
        }
    }
}
