using CodeStream.Models;
using CodeStream.Models.DTO;
using CodeStream.Pages.Admin.Skill;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CodeStream.Repositoy
{
    public class DurationRepository : IDurationRepository
    {
        private readonly IDbConnection _bd;

        public DurationRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));
        }

        public async Task<bool> CreateDuration(FormDurationDTO formDurationDTO)
        {
            try
            {
                var sql = "INSERT INTO CourseDuration (Name)" +
              "VALUES (@Name)";

                var durationCreated = await _bd.ExecuteAsync(sql, new { Name = formDurationDTO.Name });

                if (durationCreated != 0)
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

        public async Task<List<AdminDurationListDTO>> GetAdminDurationListDTOs()
        {
            var sql = "SELECT Id, Name, Active FROM CourseDuration";
            var durations = await _bd.QueryAsync<AdminDurationListDTO>(sql);
            return durations.ToList();
        }

        public async Task<List<CourseDuration>> GetCourseDurations()
        {
            var sql = "SELECT * FROM CourseDuration WHERE Active = 1";
            var durations = await _bd.QueryAsync<CourseDuration>(sql);
            return durations.ToList();
        }

        public async Task<FormDurationDTO> GetDuration(int durationId)
        {
            var sql = "SELECT * FROM CourseDuration WHERE Id = @Id";
            var duration = await _bd.QueryAsync<FormDurationDTO>(sql, new { Id = durationId });

            return duration.Single();
        }

        public async Task<bool> UpdateActiveStatus(bool active, int durationId)
        {
            var sql = "UPDATE CourseDuration SET Active = @Active  WHERE Id = @Id";



            var duration = await _bd.ExecuteAsync(sql, new { Active = !active, Id = durationId });

            if (duration > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateDuration(FormDurationDTO formDurationDTO)
        {

            try
            {
                var sql = "UPDATE CourseDuration SET Name = @Name " +
        "WHERE Id = @Id;";




                var duration = await _bd.ExecuteAsync(sql, new { Name = formDurationDTO.Name, Id = formDurationDTO.Id });

                if (duration > 0)
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
