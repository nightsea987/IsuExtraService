using Isu.Extra.Models;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities
{
    public class Lesson
    {
        public Lesson(string lecturer, int auditorium, string startTimeOfLesson, string endTimeOfLesson, string dayOfWeek)
        {
            string.IsNullOrWhiteSpace(lecturer);
            ArgumentNullException.ThrowIfNull(auditorium);
            string.IsNullOrWhiteSpace(startTimeOfLesson);
            string.IsNullOrWhiteSpace(endTimeOfLesson);
            string.IsNullOrWhiteSpace(dayOfWeek);
            ValidateDayOfWeek(dayOfWeek);
            ValidateLessonDuration(startTimeOfLesson, endTimeOfLesson);
            Lecturer = lecturer;
            Auditorium = auditorium;
            Day = dayOfWeek;
            StartTimeOfLesson = new LessonTime(startTimeOfLesson);
            EndTimeOfLesson = new LessonTime(endTimeOfLesson);
            ValidateIfStartTimeGreater(StartTimeOfLesson, EndTimeOfLesson);
        }

        public string Lecturer { get; }
        public int Auditorium { get; }
        public LessonTime StartTimeOfLesson { get; }
        public LessonTime EndTimeOfLesson { get; }
        public string Day { get; }

        public static bool ValidateLessonDuration(string startTime, string endTime)
        {
            int durationOfLessonInMinutes = 90;
            string.IsNullOrWhiteSpace(startTime);
            string.IsNullOrWhiteSpace(endTime);

            if (LessonTime.GetTimeInMinutes(endTime) - LessonTime.GetTimeInMinutes(startTime) != durationOfLessonInMinutes)
                throw LessonException.ViolatedInvariantOfLesson($"The lesson should last {durationOfLessonInMinutes} minutes");

            return true;
        }

        public static bool ValidateIfStartTimeGreater(LessonTime startTime, LessonTime endTime)
        {
            ArgumentNullException.ThrowIfNull(startTime);
            ArgumentNullException.ThrowIfNull(endTime);
            if (startTime > endTime)
                throw LessonException.ViolatedInvariantOfLesson("the lesson should not start later than the end of the lesson");

            return true;
        }

        public static bool ValidateDayOfWeek(string day)
        {
            string.IsNullOrWhiteSpace(day);
            if (!Enum.IsDefined(typeof(EnumDayOfWeek), day))
                throw LessonException.SpecifiedWrongDayOfWeek("Specify the full name of the day of the week");

            return true;
        }
    }
}
