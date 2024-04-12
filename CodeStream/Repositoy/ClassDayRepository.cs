using CodeStream.Models;
using CodeStream.Models.DTO;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CodeStream.Repositoy
{
    public class ClassDayRepository : IClassDayRepository
    {
        private readonly IDbConnection _bd;

        public ClassDayRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));
        }

        public async Task<bool> CreateAllClassDay(List<ClassDay> classDay)
        {
            var sql = "INSERT INTO ClassDay (ClassWeekId, StartHour, EndHour, CourseId) VALUES (@ClassWeekId, @StartHour, @EndHour, @CourseId)";

            try
            {
                foreach (var c in classDay)
                {
                    await _bd.ExecuteAsync(sql, new { ClassWeekId = c.ClassWeekId, StartHour = c.StartHour, EndHour = c.EndHour, CourseId = c.CourseId });
                }

                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }

         
           
        }

        public async Task<List<ClassDayListDTO>> GetAllByCourseId(int Id)
        {
            var sql = "SELECT Id, ClassWeekId, StartHour, EndHour FROM ClassDay " +
                $"WHERE CourseId = {Id}";

           var days = await _bd.QueryAsync<ClassDayListDTO>(sql);
           return days.ToList();

        }

        public async Task<List<ClassDay>> GetAllByCourseIdForForm(int Id)
        {
            var sql = "SELECT Id, ClassWeekId, StartHour, EndHour FROM ClassDay " +
               $"WHERE CourseId = {Id}";

            var days = await _bd.QueryAsync<ClassDay>(sql);
            return days.ToList();
        }

        public async Task<bool> UpdateAllClassDay(List<ClassDay> classDay)
        {
            var sql = "INSERT INTO ClassDay (ClassWeekId, StartHour, EndHour, CourseId) VALUES (@ClassWeekId, @StartHour, @EndHour, @CourseId)";
            var sql2 = "DELETE FROM ClassDay WHERE CourseId = @CourseId";
            

            try
            {


                await _bd.ExecuteAsync(sql2, new {CourseId = classDay.First().CourseId});

                foreach (var c in classDay)
                {
                   
                    await _bd.ExecuteAsync(sql, new { ClassWeekId = c.ClassWeekId, StartHour = c.StartHour, EndHour = c.EndHour, CourseId = c.CourseId });
                }

                return true;

            }
            catch (Exception)
            {
                return false;
               
            }
        }
    }
}
