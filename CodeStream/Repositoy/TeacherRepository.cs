using CodeStream.Models;
using CodeStream.Models.DTO;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CodeStream.Repositoy
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IDbConnection _bd;

        public TeacherRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));
        }

        public async Task<bool> CreateTeacher(FormTeacherDTO formTeacherDTO)
        {
            try
            {
                var sql = "INSERT INTO UserAccount (Email, Password, RolId) " +
               "VALUES (@Email, @Password, @RolId) " +
               "SELECT SCOPE_IDENTITY() AS UltimoIDInsertado;";

                var sql2 = "INSERT INTO Teacher (Name, UserId, Phone, Description, Facebook, LinkedIn, X, Instagram, Youtube)" +
                    "VALUES (@Name, @UserId, @Phone, @Description, @Facebook, @LinkedIn, @X, @Instagram, @Youtube)";

                var sql3 = "SELECT * FROM UserAccount WHERE Email = @Email";

                var user = await _bd.QueryFirstOrDefaultAsync<User>(sql3, new { Email = formTeacherDTO.Email });

                if (user != null)
                {
                    return false;
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(formTeacherDTO.Password);

                var teacherId = await _bd.ExecuteScalarAsync<int>(sql, new { Email = formTeacherDTO.Email, Password = hashedPassword, RolId = 3 });

                if (teacherId != 0)
                {

                    var teacherCreated = await _bd.ExecuteAsync(sql2, new { Name = formTeacherDTO.Name, UserId = teacherId, Phone = formTeacherDTO.Phone, Description = formTeacherDTO.Description, Facebook = formTeacherDTO.Facebook, LinkedIn = formTeacherDTO.LinkedIn, Instagram = formTeacherDTO.Instagram, Youtube = formTeacherDTO.Youtube, X = formTeacherDTO.X });

                    if (teacherCreated != 0)
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


        public async Task<List<AdminTeacherListDTO>> GetAdminTeacherListDTOs()
        {
            var sql = "SELECT t.Id, t.Name, t.UserId, u.Email, t.Active, t.Phone FROM Teacher t " +
                 "INNER JOIN UserAccount u ON t.UserId = u.Id " +
                 "INNER JOIN Rol r ON r.Id = u.RolId " +
                 "WHERE r.Id = 3";
           var teachers = await _bd.QueryAsync<AdminTeacherListDTO>(sql);
           return teachers.ToList();

        }



        public async Task<List<GetSelectListTeacherDTO>> GetSelectListTeacherDTO()
        {
            var sql = "SELECT Id, Name FROM Teacher WHERE Active = 1";
            var teachers = await _bd.QueryAsync<GetSelectListTeacherDTO>(sql);

            return teachers.ToList();

        }

        public async Task<FormTeacherDTO> GetTeacher(int TeacherId)
        {
            var sql = "SELECT t.Id, t.Name, t.Description, t.Phone, u.Email , u.Password, t.Facebook, t.LinkedIn, t.X, t.Instagram, t.Youtube FROM Teacher t " +
                "INNER JOIN UserAccount u ON u.Id = t.UserId " +
                "WHERE t.Id = @Id";


          var teacher = await _bd.QueryAsync<FormTeacherDTO>(sql, new { Id = TeacherId });
          return teacher.Single();

        }

        public async Task<TeacherDetailDTO> GetTeacherDetail(int TeacherId)
        {
             var sql = "SELECT t.Id, t.Name, t.Description, t.Phone, u.Email, t.Facebook, t.LinkedIn, t.X, t.Instagram, t.Youtube FROM Teacher t " +
                "INNER JOIN UserAccount u ON u.Id = t.UserId " +
                "WHERE t.Id = @Id";


          var teacher = await _bd.QueryAsync<TeacherDetailDTO>(sql, new { Id = TeacherId });
          return teacher.Single();
        }

        public async Task<bool> UpdateActiveStatus(bool active , int teacherId)
        {
            var sql = "UPDATE Teacher SET Active = @Active  WHERE Id = @Id";



            var course = await _bd.ExecuteAsync(sql, new { Active = !active, Id = teacherId });

            if (course > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateTeacher(FormTeacherDTO formTeacherDTO, bool isPasswordChange)
        {
            try
            {
                var sql = "UPDATE UserAccount SET Email = @Email, Password = @Password " +
           "WHERE Id = @Id;";


                var sql2 = "UPDATE Teacher SET Name = @Name, Phone = @Phone, Description = @Description, Facebook=@Facebook, LinkedIn=@LinkedIn, X=@X, Instagram=@Instagram, Youtube=@Youtube " +
                    "WHERE Id = @Id; SELECT UserId From Teacher Where Id = @Id; SELECT UserId From Teacher Where Id = @Id";

                var sql3 = "SELECT * FROM UserAccount WHERE Email = @Email";

                var user = await _bd.QueryFirstOrDefaultAsync<User>(sql3, new { Email = formTeacherDTO.Email });


           




                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(formTeacherDTO.Password);

                var userId = await _bd.ExecuteScalarAsync<int>(sql2, new { Id = formTeacherDTO.Id, Name = formTeacherDTO.Name, Phone = formTeacherDTO.Phone, Description = formTeacherDTO.Description, Facebook=formTeacherDTO.Facebook, LinkedIn=formTeacherDTO.LinkedIn, Instagram=formTeacherDTO.Instagram, Youtube=formTeacherDTO.Youtube, X=formTeacherDTO.X });

                if (userId > 0)
                {
                    var usuario = await _bd.ExecuteAsync(sql, new { Email = formTeacherDTO.Email, Password = isPasswordChange ? hashedPassword : formTeacherDTO.Password, Id = userId });

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
    }
}
