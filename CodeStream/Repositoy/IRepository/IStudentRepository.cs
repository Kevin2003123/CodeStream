using CodeStream.Models.DTO;

namespace CodeStream.Repositoy.IRepository
{
    public interface IStudentRepository
    {
        public Task<bool> InsertStudent(RegisterStudentDTO registerStudentDTO);
        public Task<bool> VerifyStudent(LoginDTO loginDTO);
        public Task<List<AdminStudentListDTO>> GetAdminStudentListDTOs();
        public Task<bool> UpdateActiveStatus(bool active, int studentId);
        public Task<bool> CreateStudent(FormStudentDTO formStudentDTO);
        public Task<bool> UpdateStudent(FormStudentDTO formStudentDTO, bool isPasswordChange);
        public Task<FormStudentDTO> GetStudent(int studentId);
    }
}
