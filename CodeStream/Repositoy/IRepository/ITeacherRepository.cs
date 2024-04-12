using CodeStream.Models.DTO;
using System;

namespace CodeStream.Repositoy.IRepository
{
    public interface ITeacherRepository
    {
        public Task<List<GetSelectListTeacherDTO>> GetSelectListTeacherDTO();
        public Task<List<AdminTeacherListDTO>> GetAdminTeacherListDTOs();
        public Task<bool> UpdateActiveStatus(bool active, int teacherId);
        public Task<bool> CreateTeacher(FormTeacherDTO formTeacherDTO);
        public Task<FormTeacherDTO> GetTeacher(int TeacherId);
        public Task<bool> UpdateTeacher(FormTeacherDTO formTeacherDTO , bool isPasswordChange);
        public Task<TeacherDetailDTO> GetTeacherDetail(int TeacherId);
    }
}
