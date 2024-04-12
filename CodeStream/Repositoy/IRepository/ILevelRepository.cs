using CodeStream.Models;
using CodeStream.Models.DTO;

namespace CodeStream.Repositoy.IRepository
{
    public interface ILevelRepository
    {
        public Task<List<CourseLevel>> GetCourseLevels();
        public Task<List<AdminLevelListDTO>> GetAdminLevelListDTOs();
        public Task<bool> UpdateActiveStatus(bool active, int levelId);
        public Task<bool> CreateLevel(FormLevelDTO formLevelDTO);
        public Task<FormLevelDTO> GetLevel(int levelId);
        public Task<bool> UpdateLevel(FormLevelDTO formLevelDTO);
    }
}
