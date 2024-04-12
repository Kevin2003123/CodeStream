using CodeStream.Models;
using CodeStream.Models.DTO;

namespace CodeStream.Repositoy.IRepository
{
    public interface ICourseRepository
    {
        public Task<List<CourseListDTO>> GetCourses();
        public Task<List<CourseListDTO>> GetPaginationFilterCourses(List<int> durations, List<int> levels, List<int> skills, string searchName, int page, int pageSize);
        public Task<List<CourseListDTO>> GetPaginationFilterMyCourses(List<int> durations, List<int> levels, List<int> skills, string searchName, int page, int pageSize, int userId);
        public Task<CourseDetailtDTO> GetCourseDetail(int Id);
        public Task<List<CourseCartDTO>> GetCourseCarts(List<int> courses);
        public  Task<List<AdminCourseListDTO>> GetAdminCourses();
        public Task<int> CreateCourse(FormCourseDTO createCourse);
        public Task<bool> UpdateCourse(FormCourseDTO updateCourse);
        public Task<FormCourseDTO> getCourseById(int id);
        public Task<bool> UpdateCourseActiveStatus(bool active, int CourseId);
    }
}
