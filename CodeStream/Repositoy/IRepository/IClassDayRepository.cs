using CodeStream.Models;
using CodeStream.Models.DTO;

namespace CodeStream.Repositoy.IRepository
{
    public interface IClassDayRepository
    {
        public Task<List<ClassDayListDTO>> GetAllByCourseId(int Id);
        public Task<List<ClassDay>> GetAllByCourseIdForForm(int Id);

        public Task<bool> CreateAllClassDay(List<ClassDay> classDay);
        public Task<bool> UpdateAllClassDay(List<ClassDay> classDay);
    }
}
