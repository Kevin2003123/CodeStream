using CodeStream.Models;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CodeStream.Repositoy
{
    public class ClassWeekRepository : IClassWeekRepository
    {

        private readonly IDbConnection _bd;

        public ClassWeekRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));
        }


        public async Task<List<WeekDay>> GetAll()
        {
            var sql = "SELECT * FROM ClassWeek";
            var weekDays = await _bd.QueryAsync<WeekDay>(sql);
            return weekDays.ToList();
        }
    }
}
