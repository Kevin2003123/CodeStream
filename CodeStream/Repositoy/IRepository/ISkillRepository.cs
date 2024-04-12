using CodeStream.Models;
using CodeStream.Models.DTO;

namespace CodeStream.Repositoy.IRepository
{
    public interface ISkillRepository
    {
        public Task<List<Skill>> GetSkills();
        public Task<List<Skill>> GetSkillByCourseId(int id);
        public Task<bool> CreateCourseSkill(List<FormCourseSkillDTO> skills);
        public Task<bool> UpdateCourseSkill(List<FormCourseSkillDTO> skills);
        public Task<List<AdminSkillListDTO>> GetAdminSkillListDTOs();
        public Task<bool> UpdateActiveStatus(bool active, int skillId);
        public Task<bool> CreateSkill(FormSkillDTO formSkillDTO);
        public Task<FormSkillDTO> GetSkill(int skillId);
        public Task<bool> UpdateSkill(FormSkillDTO FormSkillDTO);
    }
}
