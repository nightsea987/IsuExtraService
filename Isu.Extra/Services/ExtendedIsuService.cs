using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Models;
using Isu.Tools;

namespace Isu.Extra.Services
{
    public class ExtendedIsuService
    {
        private List<ExtendedGroup> _listOfExtendedGroups;

        public ExtendedIsuService()
        {
            _listOfExtendedGroups = new List<ExtendedGroup>();
        }

        public IReadOnlyList<ExtendedGroup> ListOfExtendedGroups => _listOfExtendedGroups.AsReadOnly();

        public ExtendedGroup? FindGroup(GroupName groupName)
        {
            return _listOfExtendedGroups.SingleOrDefault(s => s.Group.NameOfGroup == groupName);
        }

        public ExtendedGroup AddGroup(GroupName name, Schedule schedule)
        {
            if (FindGroup(name) != null)
                throw new GroupAlreadyExistsException("Group with this name exists", name.NameOfGroup);

            var group = new ExtendedGroup(new Group(name), schedule);
            _listOfExtendedGroups.Add(group);
            return group;
        }

        public ExtendedStudent AddStudent(ExtendedGroup group, string name)
        {
            string.IsNullOrWhiteSpace(name);
            ExtendedStudent? studentCheck = _listOfExtendedGroups
                .SelectMany(g => g.ListOfExtendedStudents)
                .SingleOrDefault(s => (s.OldStudent.StudentGroup.NameOfGroup == group.Group.NameOfGroup && s.OldStudent.Name == name));
            if (studentCheck != null)
                throw new StudentAlreadyInGroupException("Student with this name is already in group", name);

            var newStudent = new ExtendedStudent(new Student(name, group.Group), group);
            group.AddToGroup(newStudent);
            return newStudent;
        }

        public ExtendedStudent GetStudent(int id)
        {
            ExtendedStudent? student = _listOfExtendedGroups.SelectMany(g => g.ListOfExtendedStudents).SingleOrDefault(s => s.OldStudent.Id == id);
            if (student == null)
                throw new StudentNotFoundException("Cannot get student by this id");
            return student;
        }

        public IReadOnlyList<ExtendedStudent> FindStudents(GroupName groupName)
        {
            ArgumentNullException.ThrowIfNull(nameof(groupName));

            return (IReadOnlyList<ExtendedStudent>)_listOfExtendedGroups
                .Where(g => g.Group.NameOfGroup == groupName)
                .SelectMany(s => s.ListOfExtendedStudents);
        }

        public IReadOnlyList<ExtendedStudent> FindStudents(CourseNumber courseNumber)
        {
            ArgumentNullException.ThrowIfNull(nameof(courseNumber));

            return (IReadOnlyList<ExtendedStudent>)_listOfExtendedGroups
                .Where(g => g.Group.NameOfGroup.NumberOfCourse == courseNumber)
                .SelectMany(s => s.ListOfExtendedStudents);
        }

        public IReadOnlyList<ExtendedGroup> FindGroups(CourseNumber courseNumber)
        {
            ArgumentNullException.ThrowIfNull(nameof(courseNumber));

            return (IReadOnlyList<ExtendedGroup>)_listOfExtendedGroups.Where(g => g.Group.NameOfGroup.NumberOfCourse == courseNumber);
        }
    }
}
