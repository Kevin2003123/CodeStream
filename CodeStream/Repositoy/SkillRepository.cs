using CodeStream.Models;
using CodeStream.Models.DTO;
using CodeStream.Pages.Admin.Student;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CodeStream.Repositoy
{
    public class SkillRepository : ISkillRepository
    {
        private readonly IDbConnection _bd;

        public SkillRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));
        }

        public async Task<bool> CreateCourseSkill(List<FormCourseSkillDTO> skills)
        {
            var sql = "INSERT INTO CourseSkill (CourseId, SkillId) VALUES (@CourseId, @SkillId)";
            try
            {
                foreach (var s in skills)
                {
                    await _bd.ExecuteAsync(sql, s);
                }

            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }

        public async Task<bool> CreateSkill(FormSkillDTO formSkillDTO)
        {
            try
            {
                var sql = "INSERT INTO Skill (Name)" +
              "VALUES (@Name)";

                var skillCreated = await _bd.ExecuteAsync(sql, new { Name = formSkillDTO.Name });

                if (skillCreated != 0)
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

        public async Task<List<AdminSkillListDTO>> GetAdminSkillListDTOs()
        {
            var sql = "SELECT Id, Name, Active FROM Skill";
            var skills = await _bd.QueryAsync<AdminSkillListDTO>(sql);
            return skills.ToList();
        }

        public async Task<FormSkillDTO> GetSkill(int skillId)
        {
            var sql = "SELECT * FROM Skill WHERE Id = @Id";
            var skills = await _bd.QueryAsync<FormSkillDTO>(sql, new { Id = skillId });

            return skills.Single();

        }

        public async Task<List<Skill>> GetSkillByCourseId(int id)
        {
            var sql = $"SELECT s.Id, s.Name FROM CourseSkill cs " +
                "INNER JOIN Skill s ON cs.SkillId = s.Id " +
                $"WHERE CourseId = {id}";
            var skills = await _bd.QueryAsync<Skill>(sql);
            return skills.ToList();
        }

        public async Task<List<Skill>> GetSkills()
        {
            var sql = "SELECT * FROM Skill";
            var skills = await _bd.QueryAsync<Skill>(sql);
            if (skills != null)
            {
                return skills.ToList();
            }

            return new List<Skill>();

        }

        public async Task<bool> UpdateActiveStatus(bool active, int skillId)
        {

            var sql = "UPDATE Skill SET Active = @Active  WHERE Id = @Id";



            var skill = await _bd.ExecuteAsync(sql, new { Active = !active, Id = skillId });

            if (skill > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateCourseSkill(List<FormCourseSkillDTO> skills)
        {
            //var sql = "UPDATE CourseSkill SET CourseId = @CourseId, SkillId = @SkillId;";
            var sql = "INSERT INTO CourseSkill (CourseId, SkillId) VALUES (@CourseId, @SkillId)";
            var sql2 = "DELETE FROM CourseSkill WHERE CourseId = @CourseId";
            try
            {
                await _bd.ExecuteAsync(sql2, new { CourseId = skills.First().CourseId });
                foreach (var s in skills)
                {
                    await _bd.ExecuteAsync(sql, s);
                }

                return true;

            }
            catch (Exception)
            {
                return false;

            }

        }

        public async Task<bool> UpdateSkill(FormSkillDTO FormSkillDTO)
        {
            try
            {
                var sql = "UPDATE Skill SET Name = @Name " +
            "WHERE Id = @Id;";




                var skill = await _bd.ExecuteAsync(sql, new { Name = FormSkillDTO.Name, Id = FormSkillDTO.Id });

                if (skill > 0)
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
