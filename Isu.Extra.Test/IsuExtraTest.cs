using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Extra.Tools;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test
{
    public class IsuExtraTest
    {
        private ExtraIsuService _service;
        public IsuExtraTest()
        {
            _service = new ExtraIsuService();
        }

        [Fact]
        public void InputWrongFormatLesson()
        {
            Assert.Throws<LessonException>(() =>
            {
                var schedule = new Schedule();
                schedule.AddLesson(new Lesson("S", 322, "15:20", "16:50", "Skauhdkuah"));
            });
        }

        [Fact]
        public void AddStudentToOgnpCourse()
        {
            var group = new Group(new GroupName("M3205"));
            var schedule = new Schedule();
            schedule.AddLesson(new Lesson("S", 322, "15:20", "16:50", "Monday"));
            var extendedGroup = new ExtendedGroup(group, schedule);
            var student = new ExtendedStudent(new Student("Sasha", group), extendedGroup);
            var megaFaculty = new MegaFaculty("ТИНТ", new List<char> { 'R', 'N' }.AsReadOnly());
            _service.OgnpService.AddMegaFaculty(megaFaculty);
            _service.OgnpService.AddCourseToMegaFaculty(megaFaculty, "КИБ");
            _service.OgnpService.AddCourseToMegaFaculty(megaFaculty, "ML");
            _service.OgnpService.AddCourseToMegaFaculty(megaFaculty, "Web");

            Assert.Throws<OgnpCourseException>(() =>
            {
                _service.OgnpService.AddStudentToOgnpCourse(student, "КИБ");
                _service.OgnpService.AddStudentToOgnpCourse(student, "ML");
                _service.OgnpService.AddStudentToOgnpCourse(student, "Web");
            });
        }

        [Fact]
        public void TimeIntersection()
        {
            var group = new Group(new GroupName("M3205"));
            var schedule1 = new Schedule();
            schedule1.AddLesson(new Lesson("S", 322, "15:20", "16:50", "Wednesday"));
            var extendedGroup = new ExtendedGroup(group, schedule1);
            var student = new ExtendedStudent(new Student("Sasha", group), extendedGroup);

            var schedule2 = new Schedule();
            schedule2.AddLesson(new Lesson("T", 422, "15:20", "16:50", "Wednesday"));
            var megaFaculty = new MegaFaculty("ТИНТ", new List<char> { 'R', 'N' }.AsReadOnly());
            _service.OgnpService.AddMegaFaculty(megaFaculty);
            OgnpCourse newCourse = _service.OgnpService.AddCourseToMegaFaculty(megaFaculty, "КИБ");

            newCourse.AddOgnpGroup(new OgnpGroup(new OgnpGroupName("КИБ3.3"), schedule2));

            Assert.Throws<OgnpCourseException>(() =>
            {
                megaFaculty.AddStudentToCourse(student, newCourse);
            });
        }
    }
}
