using CodeStream.Models;
using CodeStream.Models.DTO;
using CodeStream.Pages.Admin.Duration;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CodeStream.Repositoy
{
    public class LevelRepository : ILevelRepository
    {
        private readonly IDbConnection _bd;

        public LevelRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));
        }

        public async Task<bool> CreateLevel(FormLevelDTO formLevelDTO)
        {
            try
            {
                var sql = "INSERT INTO CourseLevel (Name)" +
                             "VALUES (@Name)";

                var levelCreated = await _bd.ExecuteAsync(sql, new { Name = formLevelDTO.Name });

                if (levelCreated != 0)
                {
                    return true;
                }




                return false;
            }
            catch (Exception)
            {

                return false;
            }
          
        }

        public async Task<List<AdminLevelListDTO>> GetAdminLevelListDTOs()
        {
            var sql = "SELECT Id, Name, Active FROM CourseLevel";
            var levels = await _bd.QueryAsync<AdminLevelListDTO>(sql);
            return levels.ToList();
        }

        public async Task<List<CourseLevel>> GetCourseLevels()
        {
            var sql = "SELECT * FROM CourseLevel WHERE Active = 1";
            var levels = await _bd.QueryAsync<CourseLevel>(sql);
            return levels.ToList();
        }

        public async Task<FormLevelDTO> GetLevel(int levelId)
        {
            var sql = "SELECT * FROM CourseLevel WHERE Id = @Id";
            var level = await _bd.QueryAsync<FormLevelDTO>(sql, new { Id = levelId });

            return level.Single();
        }

        public async Task<bool> UpdateActiveStatus(bool active, int levelId)
        {
            var sql = "UPDATE CourseLevel SET Active = @Active  WHERE Id = @Id";



            var level = await _bd.ExecuteAsync(sql, new { Active = !active, Id = levelId });

            if (level > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateLevel(FormLevelDTO formLevelDTO)
        {
            try
            {
                var sql = "UPDATE CourseLevel SET Name = @Name " +
       "WHERE Id = @Id;";




                var level = await _bd.ExecuteAsync(sql, new { Name = formLevelDTO.Name, Id = formLevelDTO.Id });

                if (level > 0)
                {
                    return true;
                }





                return false;

            }
            catch (Exception)
            {

                return false;
            }
          
        }
    }
}
