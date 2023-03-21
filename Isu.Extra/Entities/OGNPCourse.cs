using Isu.Extra.Tools;

namespace Isu.Extra.Entities
{
    public class OgnpCourse
    {
        private List<OgnpGroup> _listOfOgnpGroups;
        public OgnpCourse(string nameOfOGNPCourse)
        {
            string.IsNullOrWhiteSpace(nameOfOGNPCourse);
            _listOfOgnpGroups = new List<OgnpGroup>();
            NameOfOgnpCourse = nameOfOGNPCourse;
        }

        public IReadOnlyList<OgnpGroup> ListOfOgnpGroups => _listOfOgnpGroups.AsReadOnly();
        public string NameOfOgnpCourse { get; }

        public void AddOgnpGroup(OgnpGroup newOgnpGroup)
        {
            ArgumentNullException.ThrowIfNull(nameof(newOgnpGroup));
            if (ListOfOgnpGroups.Any(g => g.NameOfOGNPGroup == newOgnpGroup.NameOfOGNPGroup))
                throw new OgnpCourseException("Ognp group with this name already exists");
            _listOfOgnpGroups.Add(newOgnpGroup);
        }

        public void DeleteFromCourse(ExtendedStudent student)
        {
            ArgumentNullException.ThrowIfNull(nameof(student));
            OgnpGroup? group = _listOfOgnpGroups.SingleOrDefault(s => s.ListOfStudents.Contains(student));
            if (group == null)
                throw new Exception();
            group.DeleteFromOGNPGroup(student);
        }

        internal void AddToOgnpCourse(ExtendedStudent student)
        {
            ArgumentNullException.ThrowIfNull(nameof(student));
            if (student.ListOfOgnpGroups.Count >= ExtendedStudent.MaxCountOfOgnpGroups)
                throw new OgnpCourseException("The student has already enrolled in two courses");
            OgnpGroup? needGroup = _listOfOgnpGroups.SingleOrDefault(s => !s.ScheduleOfOGNPGroup.IsSchedulesIntersected(student.Group.ScheduleOfGroup));
            if (needGroup == null)
                throw new OgnpCourseException("The schedule of all groups intersects with the schedule of student");
            needGroup.AddToOgnpGroup(student);
            student.JoinOgnpGroup(needGroup);
        }
    }
}
