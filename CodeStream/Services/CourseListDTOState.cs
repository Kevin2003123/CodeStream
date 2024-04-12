using CodeStream.Models.DTO;

namespace CodeStream.Services
{
    public class CourseListDTOState
    {
        public List<CourseListDTO> courseListDTOs = new List<CourseListDTO>();
        public event EventHandler? StateChangeHandler;
        private List<CourseListDTO> _currentCourseListDTOs = new List<CourseListDTO>();
        private void StateHasChanged() { 
            StateChangeHandler?.Invoke(this, EventArgs.Empty);
        
        }

        public List<CourseListDTO>GetCourseListDTOs()
        {
            return _currentCourseListDTOs;
        }

        public void SetCourseListDTOs()
        {
            _currentCourseListDTOs = courseListDTOs;
            StateHasChanged();
        }
    }
}
