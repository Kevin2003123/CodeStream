using CodeStream.Models;

namespace CodeStream.Services
{
    public class FilterState
    {
        public List<CourseDuration> Duration { get; set; } = new List<CourseDuration>();
        public List<CourseLevel> Level { get; set; } = new List<CourseLevel>();
        public List<Skill> Skill { get; set; } = new List<Skill>();
        public event EventHandler? StateChangeHandler;

        private List<CourseDuration> _currentDuration = new List<CourseDuration>();
        private List<CourseLevel> _currentLevel = new List<CourseLevel>();
        private List<Skill> _currentSkill = new List<Skill>();

        private void StateHasChanged()
        {
            StateChangeHandler?.Invoke(this, EventArgs.Empty);

        }

        public List<CourseDuration> GetDuration()
        {
            return _currentDuration;

        }

        public List<CourseLevel> GetLevel()
        {
            return _currentLevel;

        }

        public List<Skill> GetSkill()
        {
            return _currentSkill;
        }

        public void SetFilter()
        {
            _currentDuration = Duration;
            _currentLevel = Level;
            _currentSkill = Skill;
            StateHasChanged();
        }
    }
}

