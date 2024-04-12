using CodeStream.Authentication;
using CodeStream.Models.DTO;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CodeStream.Repositoy
{
    public class UserRepository : IUserRepository
    {

        private readonly IDbConnection _bd;



        public UserRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));

        }

        public async Task<UserSession> GetUserByEmail(string email)
        {

            var sql2 = "SELECT r.Rol FROM UserAccount u  " +
                "INNER JOIN Rol r ON r.Id = u.RolId  " +
                "WHERE Email = @Email";

            var rol = await _bd.ExecuteScalarAsync<string>(sql2, new { Email = email });

            if(rol != null)
            {

                if(rol == "student")
                {
                    var sql = "SELECT u.Id, s.Name, u.Email, r.Rol FROM UserAccount u " +
                     "INNER JOIN Rol r ON u.RolId = r.Id " +
                     "INNER JOIN Student s ON s.UserId = u.Id " +
                     "WHERE u.Email = @Email";

                    var user = await _bd.QueryFirstOrDefaultAsync<UserInfoDTO>(sql, new { Email = email });
                    if (user != null)
                    {
                        var userSession = new UserSession
                        {
                            Id = user.Id.ToString(),
                            Name = user.Name,
                            Email = user.Email,
                            Rol = user.Rol,
                        };
                        return userSession;
                    }
                }
                else if (rol == "teacher")
                {
                    var sql = "SELECT u.Id, t.Name, u.Email, r.Rol FROM UserAccount u " +
                     "INNER JOIN Rol r ON u.RolId = r.Id " +
                     "INNER JOIN Teacher t ON t.UserId = u.Id " +
                     "WHERE u.Email = @Email";

                    var user = await _bd.QueryFirstOrDefaultAsync<UserInfoDTO>(sql, new { Email = email });
                    if (user != null)
                    {
                        var userSession = new UserSession
                        {
                            Id = user.Id.ToString(),
                            Name = user.Name,
                            Email = user.Email,
                            Rol = user.Rol,
                        };
                        return userSession;
                    }
                }
                else if (rol == "admin")
                {
                    var sql = "SELECT u.Id,u.Email, r.Rol FROM UserAccount u " +
                    "INNER JOIN Rol r ON u.RolId = r.Id " +
                    "WHERE u.Email = @Email";

                    var user = await _bd.QueryFirstOrDefaultAsync<UserInfoDTO>(sql, new { Email = email });
                    if (user != null)
                    {
                        var userSession = new UserSession
                        {
                            Id = user.Id.ToString(),
                            Name = "Admin",
                            Email = user.Email,
                            Rol = user.Rol,
                        };
                        return userSession;
                    }

                }


            }





            return new UserSession();
            
        }
    }
}
