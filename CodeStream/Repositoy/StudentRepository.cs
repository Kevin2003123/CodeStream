using CodeStream.Models;
using CodeStream.Models.DTO;
using CodeStream.Pages.Admin.Teacher;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;

namespace CodeStream.Repositoy
{
    public class StudentRepository : IStudentRepository
    {

        private readonly IDbConnection _bd;
       


        public StudentRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));
           
        }

        public async Task<bool> CreateStudent(FormStudentDTO formStudentDTO)
        {
            try
            {
                var sql = "INSERT INTO UserAccount (Email, Password, RolId) " +
               "VALUES (@Email, @Password, @RolId) " +
               "SELECT SCOPE_IDENTITY() AS UltimoIDInsertado;";

                var sql2 = "INSERT INTO Student (Name, UserId, Phone)" +
                    "VALUES (@Name, @UserId, @Phone)";

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(formStudentDTO.Password);

                var studentId = await _bd.ExecuteScalarAsync<int>(sql, new { Email = formStudentDTO.Email, Password = hashedPassword, RolId = 2 });

                if (studentId != 0)
                {

                    var studentCreated = await _bd.ExecuteAsync(sql2, new { Name = formStudentDTO.Name, UserId = studentId, Phone = formStudentDTO.Phone });

                    if (studentCreated != 0)
                    {
                        return true;
                    }

                }


                return false;
            }
            catch (Exception)
            {

                return false;
            }
           

        }

        public async Task<List<AdminStudentListDTO>> GetAdminStudentListDTOs()
        {

            
            var sql = "SELECT s.Id, s.Name, s.UserId, u.Email, s.Active, s.Phone FROM Student s " +
                "INNER JOIN UserAccount u ON s.UserId = u.Id " +
                "INNER JOIN Rol r ON r.Id = u.RolId " +
                "WHERE r.Id = 2";
            var students = await _bd.QueryAsync<AdminStudentListDTO>(sql);
            return students.ToList();
        }

        public async Task<FormStudentDTO> GetStudent(int studentId)
        {
            var sql = "SELECT s.Id, s.Name, s.Phone, u.Email , u.Password FROM Student s " +
           "INNER JOIN UserAccount u ON u.Id = s.UserId " +
           "WHERE s.Id = @Id";


            var student = await _bd.QueryAsync<FormStudentDTO>(sql, new { Id = studentId });
            return student.Single();
        }

        public async Task<bool> InsertStudent(RegisterStudentDTO registerStudentDTO)
        {

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerStudentDTO.Password);

      
            var userId = 0;
                try
                {
      
                 userId = await _bd.ExecuteScalarAsync<int>(
                    "INSERT INTO UserAccount (Email, Password, RolId) VALUES (@Email, @Password, @RolId); SELECT CAST(SCOPE_IDENTITY() as int);",
                    new { Email = registerStudentDTO.Email, Password = hashedPassword, RolId = 2 });


      

                if(userId > 0) {
                    await _bd.ExecuteAsync(
                     "INSERT INTO Student (Name, Phone, UserId) VALUES (@Name, @Phone, @UserId)",
                     new { Name = registerStudentDTO.Name, Phone = registerStudentDTO.Phone, UserId = userId });
                }
             

              

                    return true;
                }
                catch (Exception ex)
                {

                if (userId > 0)
                {
                    var sql = "DELETE FROM UserAccount WHERE id = @UserId";

                    await _bd.ExecuteAsync(sql, new {UserId = userId});

                }
                    Console.WriteLine($"Error al insertar estudiante: {ex.Message}");
                    return false;
                }

            
        }

        public async Task<bool> UpdateActiveStatus(bool active, int studentId)
        {
            var sql = "UPDATE Student SET Active = @Active  WHERE Id = @Id";



            var student = await _bd.ExecuteAsync(sql, new { Active = !active, Id = studentId });

            if (student > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateStudent(FormStudentDTO formStudentDTO, bool isPasswordChange)
        {
            try
            {
                var sql = "UPDATE UserAccount SET Email = @Email, Password = @Password " +
          "WHERE Id = @Id;";


                var sql2 = "UPDATE Student SET Name = @Name, Phone = @Phone " +
                    "WHERE Id = @Id; SELECT UserId From Student Where Id = @Id";

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(formStudentDTO.Password);

                var userId = await _bd.ExecuteScalarAsync<int>(sql2, new { Id = formStudentDTO.Id, Name = formStudentDTO.Name, Phone = formStudentDTO.Phone });

                if (userId > 0)
                {
                    var usuario = await _bd.ExecuteAsync(sql, new { Email = formStudentDTO.Email, Password = isPasswordChange ? hashedPassword : formStudentDTO.Password, Id = userId });

                    if (usuario > 0)
                    {
                        return true;
                    }


                }


                return false;
            }
            catch (Exception)
            {

                return false;
            }
           
    
}

        public async  Task<bool> VerifyStudent(LoginDTO loginDTO)
        {

            var sql = "SELECT * FROM UserAccount WHERE Email = @Email";

            var user = await _bd.QueryFirstOrDefaultAsync<User>(sql, new {Email = loginDTO.Email});

            if (user != null)
            {
                bool isMatch = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
                if (isMatch)
                {
                    
                 return true;
                 }
                 
                   
                }

            return false;
            
        }
    }
}
