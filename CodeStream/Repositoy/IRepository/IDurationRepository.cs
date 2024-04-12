using CodeStream.Models;
using CodeStream.Models.DTO;

namespace CodeStream.Repositoy.IRepository
{
    public interface IDurationRepository
    {
        public Task<List<CourseDuration>> GetCourseDurations();
        public Task<List<AdminDurationListDTO>> GetAdminDurationListDTOs();
        public Task<bool> UpdateActiveStatus(bool active, int durationId);
        public Task<bool> CreateDuration(FormDurationDTO formDurationDTO);
        public Task<FormDurationDTO> GetDuration(int durationId);
        public Task<bool> UpdateDuration(FormDurationDTO formDurationDTO);
    }
}
