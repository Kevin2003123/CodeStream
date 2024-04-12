namespace CodeStream.Services
{
    public class CourseFilterState
    {
        public List<int> Duration { get; set; } = new List<int>();
        public List<int> Level { get; set; } = new List<int>();
        public List<int> Skill { get; set; } = new List<int>();
        public int page {get; set;} = 1;
        public event EventHandler? StateChangeHandler;

        private List<int> _currentDuration = new List<int>();
        private List<int> _currentLevel = new List<int>();
        private List<int> _currentSkill = new List<int>();
        private int _currentPage;
        private void StateHasChanged()
        {
            StateChangeHandler?.Invoke(this, EventArgs.Empty);

        }

        public List<int> GetDuration()
        {
            return _currentDuration;

        }

        public List<int> GetLevel()
        {
            return _currentLevel;

        }

        public List<int> GetSkill()
        {
            return _currentSkill;
        }

        public int GetPage()
        {
            return _currentPage;
        }

        public void SetFilter()
        {
            _currentDuration = Duration;
            _currentLevel = Level;
            _currentSkill = Skill;
            _currentPage = page;
            StateHasChanged();
        }
    }
}
