using CodeStream.Modelos;
using CodeStream.Models.DTO;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CodeStream.Repositoy
{
    public class CourseRepository : ICourseRepository
    {

        private readonly IDbConnection _bd;

        public CourseRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));
        }


        public async Task<List<CourseListDTO>> GetCourses()
        {
            var sql = "SELECT c.Id, c.Name, c.Description, c.Price, c.StartDate, c.EndDate, t.Name as TeacherName, cd.Name as Duration , cl.Name as Level, c.Image FROM Course c " +
                "INNER JOIN Teacher t ON c.TeacherId = t.Id " +
                "INNER JOIN CourseDuration cd ON cd.Id = c.CourseDurationId " +
                "INNER JOIN CourseLevel cl ON cl.Id = c.CourseLevelId ";
            var courses = await _bd.QueryAsync<CourseListDTO>(sql);
            return courses.ToList();

        }

        public async Task<List<AdminCourseListDTO>> GetAdminCourses()
        {
            var sql = "SELECT c.Id, c.Name, c.Price, c.StartDate, c.EndDate, t.Name as TeacherName, cd.Name as Duration , cl.Name as Level, c.Image, c.Active FROM Course c " +
                "INNER JOIN Teacher t ON c.TeacherId = t.Id " +
                "INNER JOIN CourseDuration cd ON cd.Id = c.CourseDurationId " +
                "INNER JOIN CourseLevel cl ON cl.Id = c.CourseLevelId ";
            var courses = await _bd.QueryAsync<AdminCourseListDTO>(sql);
            return courses.ToList();

        }

        public async Task<CourseDetailtDTO> GetCourseDetail(int Id)
        {
            var sql = "SELECT c.Id, c.Name, c.Description, c.Description2, c.Price, c.StartDate, c.EndDate, t.Id as TeacherId, t.Name as TeacherName, cd.Name as Duration , cl.Name as Level, c.Image FROM Course c " +
                "INNER JOIN Teacher t ON c.TeacherId = t.Id " +
                "INNER JOIN CourseDuration cd ON cd.Id = c.CourseDurationId " +
                "INNER JOIN CourseLevel cl ON cl.Id = c.CourseLevelId " +
                $"WHERE c.Id = {Id}";

            var course = await _bd.QueryAsync<CourseDetailtDTO>(sql);
            return course.Single();
        }
        


        public async Task<List<CourseListDTO>> GetPaginationFilterCourses(List<int> durations, List<int> levels, List<int> skills, string searchName, int page = 1, int pageSize = 9)
        {

          
            var sql = "SELECT c.Id, c.Name, c.Description, c.Price, c.StartDate, c.EndDate, t.Id as TeacherId, t.Name as TeacherName, cd.Name as Duration, cl.Name as Level, c.Image " +
                      "FROM Course c " +
                      "INNER JOIN Teacher t ON c.TeacherId = t.Id " +
                      "INNER JOIN CourseDuration cd ON cd.Id = c.CourseDurationId " +
                      "INNER JOIN CourseLevel cl ON cl.Id = c.CourseLevelId " +
                      "WHERE c.Id > 0 AND c.Active = 1 "; // Cambiar por el valor de filtrado adecuado si es necesario

            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                sql += "AND c.Name LIKE @SearchName ";
                parameters.Add("@SearchName", "%" + searchName + "%");
            }

            if (durations != null && durations.Count > 0)
            {
                sql += "AND (";
                for (int i = 0; i < durations.Count; i++)
                {
                    if (i > 0)
                        sql += " OR ";
                    sql += "cd.Id = @Duration" + i + " ";
                    parameters.Add("@Duration" + i, durations[i]);
                }
                sql += ")";
            }

            if (levels != null && levels.Count > 0)
            {
                sql += "AND (";
                for (int i = 0; i < levels.Count; i++)
                {
                    if (i > 0)
                        sql += " OR ";
                    sql += "cl.Id = @Level" + i + " ";
                    parameters.Add("@Level" + i, levels[i]);
                }
                sql += ")";
            }

            if (skills != null && skills.Count > 0)
            {
                sql += "AND (";
                for (int i = 0; i < skills.Count; i++)
                {
                    if (i > 0)
                        sql += " OR ";
                    sql += "c.Id IN (SELECT CourseId FROM CourseSkill WHERE SkillId = @Skill" + i + ") ";
                    parameters.Add("@Skill" + i, skills[i]);
                }
                sql += ")";
            }

            sql += " ORDER BY c.Id " +
                   "OFFSET @Offset ROWS " +
                   "FETCH NEXT @PageSize ROWS ONLY";

            parameters.Add("@Offset", (page - 1) * pageSize);
            parameters.Add("@PageSize", pageSize);

            var courses = await _bd.QueryAsync<CourseListDTO>(sql, parameters);
            return courses.ToList();
        }

        public async Task<List<CourseCartDTO>> GetCourseCarts(List<int> courses)
        {

            string parameters = string.Join(",", courses);
            var sql = "SELECT c.Id, c.Name, c.Price, t.Name as TeacherName, cd.Name as Duration , cl.Name as Level, c.Image FROM Course c " +
                 "INNER JOIN Teacher t ON c.TeacherId = t.Id " +
                 "INNER JOIN CourseDuration cd ON cd.Id = c.CourseDurationId " +
                 "INNER JOIN CourseLevel cl ON cl.Id = c.CourseLevelId " +
                 $"WHERE c.Id IN ({parameters})";

            var getcourses = await _bd.QueryAsync<CourseCartDTO>(sql);
            return getcourses.ToList();
        }

        public async Task<int> CreateCourse(FormCourseDTO createCourse)
        {
            var sql = "INSERT INTO Course (Name, Description, Price, StartDate, EndDate, TeacherId, Image, Description2, CourseLevelId, CourseDurationId) VALUES " +
                "(@Name, @Description, @Price, @StartDate, @EndDate, @TeacherId, @Image, @Description2, @CourseLevelId, @CourseDurationId); " +
                "SELECT SCOPE_IDENTITY() AS UltimoIDInsertado;";

            var id = await _bd.ExecuteScalarAsync<int>(sql, createCourse);

            return id;
        }

        public async Task<FormCourseDTO> getCourseById(int id)
        {
            var sql = "SELECT * FROM Course WHERE Id = @Id";

            var course = await _bd.QueryAsync<FormCourseDTO>(sql, new {Id = id});

            return course.Single();
        }


        public async Task<bool> UpdateCourse(FormCourseDTO createCourse)
        {
            var sql = "UPDATE Course SET Name = @Name, Description = @Description, Price = @Price, StartDate = @StartDate, EndDate = @EndDate, TeacherId = @TeacherId, Image = @Image, Description2 = @Description2, CourseLevelId = @CourseLevelId, CourseDurationId = @CourseDurationId " +
                "WHERE Id = @Id";

            var courseUpdated = await _bd.ExecuteAsync(sql, createCourse);

            if(courseUpdated > 0)
            {
                return true;
            }


            return false;
        }

        public async Task<bool> UpdateCourseActiveStatus(bool active, int CourseId)
        {
             var sql = "UPDATE Course SET Active = @Active  WHERE Id = @Id";


            
             var course =  await _bd.ExecuteAsync(sql, new {Active = !active , Id = CourseId});
            
         if(course > 0)
            {
                return true;
            }

         return false;
        }

        public async Task<List<CourseListDTO>> GetPaginationFilterMyCourses(List<int> durations, List<int> levels, List<int> skills, string searchName, int page, int pageSize, int userId)
        {
            var sql = "SELECT c.Id, c.Name, c.Description, c.Price, c.StartDate, c.EndDate, t.Id as TeacherId, t.Name as TeacherName, cd.Name as Duration, cl.Name as Level, c.Image " +
                     "FROM Course c " +
                     "INNER JOIN Teacher t ON c.TeacherId = t.Id " +
                     "INNER JOIN CourseDuration cd ON cd.Id = c.CourseDurationId " +
                     "INNER JOIN CourseLevel cl ON cl.Id = c.CourseLevelId " +
                     "INNER JOIN PaymentCourse pc On pc.CourseId = c.Id " +
                     "INNER JOIN Payment p ON p.Id = pc.PaymentId " +
                     "WHERE c.Id > 0 AND p.UserId = @UserId AND c.Active = 1 "; // Cambiar por el valor de filtrado adecuado si es necesario

            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                sql += "AND c.Name LIKE @SearchName ";
                parameters.Add("@SearchName", "%" + searchName + "%");
            }

            if (durations != null && durations.Count > 0)
            {
                sql += "AND (";
                for (int i = 0; i < durations.Count; i++)
                {
                    if (i > 0)
                        sql += " OR ";
                    sql += "cd.Id = @Duration" + i + " ";
                    parameters.Add("@Duration" + i, durations[i]);
                }
                sql += ")";
            }

            if (levels != null && levels.Count > 0)
            {
                sql += "AND (";
                for (int i = 0; i < levels.Count; i++)
                {
                    if (i > 0)
                        sql += " OR ";
                    sql += "cl.Id = @Level" + i + " ";
                    parameters.Add("@Level" + i, levels[i]);
                }
                sql += ")";
            }

            if (skills != null && skills.Count > 0)
            {
                sql += "AND (";
                for (int i = 0; i < skills.Count; i++)
                {
                    if (i > 0)
                        sql += " OR ";
                    sql += "c.Id IN (SELECT CourseId FROM CourseSkill WHERE SkillId = @Skill" + i + ") ";
                    parameters.Add("@Skill" + i, skills[i]);
                }
                sql += ")";
            }

            sql += " ORDER BY c.Id " +
                   "OFFSET @Offset ROWS " +
                   "FETCH NEXT @PageSize ROWS ONLY";

            parameters.Add("@Offset", (page - 1) * pageSize);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@UserId", userId);
            var courses = await _bd.QueryAsync<CourseListDTO>(sql, parameters);
            return courses.ToList();
        }
    }
    }
