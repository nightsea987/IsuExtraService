 namespace Isu.Extra.Entities
{
    public class Schedule
    {
        private List<Lesson> _listOfLessons;
        public Schedule()
        {
            _listOfLessons = new List<Lesson>();
        }

        public IReadOnlyList<Lesson> ListOfLessons => _listOfLessons.AsReadOnly();

        public bool IsTimeIntersected(Lesson newLesson)
        {
            ArgumentNullException.ThrowIfNull(nameof(newLesson));
            if (_listOfLessons.Any(s => (s.StartTimeOfLesson == newLesson.StartTimeOfLesson) && (s.EndTimeOfLesson == newLesson.EndTimeOfLesson) && (s.Day == newLesson.Day)))
                return true;
            if (_listOfLessons.Any(s => (s.Day == newLesson.Day) && (s.StartTimeOfLesson > newLesson.StartTimeOfLesson && s.EndTimeOfLesson < newLesson.StartTimeOfLesson)))
                return true;
            if (_listOfLessons.Any(s => (s.Day == newLesson.Day) && (s.StartTimeOfLesson > newLesson.EndTimeOfLesson && s.EndTimeOfLesson < newLesson.EndTimeOfLesson)))
                return true;
            return false;
        }

        public bool IsSchedulesIntersected(Schedule otherSchedule)
        {
            ArgumentNullException.ThrowIfNull(otherSchedule);
            return otherSchedule.ListOfLessons.Any(l => IsTimeIntersected(l));
        }

        public bool IsLecturerIntersected(Lesson newLesson)
        {
            ArgumentNullException.ThrowIfNull(newLesson);
            return _listOfLessons.Any(s => (s.Lecturer == newLesson.Lecturer));
        }

        public bool IsAuditoriumIntersected(Lesson newLesson)
        {
            ArgumentNullException.ThrowIfNull(newLesson);
            return _listOfLessons.Any(s => (s.Auditorium == newLesson.Auditorium));
        }

        public void AddLesson(Lesson newLesson)
        {
            ArgumentNullException.ThrowIfNull(newLesson);
            if (IsTimeIntersected(newLesson))
                throw new Exception();
            _listOfLessons.Add(newLesson);
        }
    }
}