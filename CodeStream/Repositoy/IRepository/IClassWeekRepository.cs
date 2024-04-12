using CodeStream.Models;

namespace CodeStream.Repositoy.IRepository
{
    public interface IClassWeekRepository
    {

        public Task<List<WeekDay>> GetAll();

    }
}
