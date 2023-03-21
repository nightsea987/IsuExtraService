using Isu.Extra.Models;
using Isu.Tools;

namespace Isu.Extra.Entities
{
    public class OgnpGroup
    {
        private const int MaxNumberOfStudents = 20;
        private List<ExtendedStudent> _listOfExtendedStudents;

        public OgnpGroup(OgnpGroupName nameOfOGNPGroup, Schedule schedule)
        {
            ArgumentNullException.ThrowIfNull(nameof(nameOfOGNPGroup));
            ArgumentNullException.ThrowIfNull(nameof(schedule));
            _listOfExtendedStudents = new List<ExtendedStudent>();
            NameOfOGNPGroup = nameOfOGNPGroup;
            ScheduleOfOGNPGroup = schedule;
        }

        public OgnpGroupName NameOfOGNPGroup { get; }
        public Schedule ScheduleOfOGNPGroup { get; }

        public IReadOnlyList<ExtendedStudent> ListOfStudents => _listOfExtendedStudents.AsReadOnly();

        public static int GetMaxNumberOfStudents()
        {
            return MaxNumberOfStudents;
        }

        public void DeleteFromOGNPGroup(ExtendedStudent student)
        {
            ArgumentNullException.ThrowIfNull(nameof(student));

            if (_listOfExtendedStudents.Find(x => x.OldStudent.Id == student.OldStudent.Id) == null)
                throw new StudentNotFoundException("Cannot delete student because he's not found");

            _listOfExtendedStudents.Remove(student);
        }

        internal void AddToOgnpGroup(ExtendedStudent student)
        {
            ArgumentNullException.ThrowIfNull(nameof(student));

            if (_listOfExtendedStudents.Any(x => x.OldStudent.Id == x.OldStudent.Id))
                throw new StudentAlreadyInGroupException("Student with this name is already in group", student.OldStudent.Name);

            if (_listOfExtendedStudents.Count >= MaxNumberOfStudents)
                throw new ExceededMaxNumberOfStudentsException("Maximum number of students exceeded", MaxNumberOfStudents);

            _listOfExtendedStudents.Add(student);
        }
    }
}