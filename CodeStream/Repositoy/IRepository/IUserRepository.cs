using CodeStream.Authentication;

namespace CodeStream.Repositoy.IRepository
{
    public interface IUserRepository
    {
        public Task<UserSession>GetUserByEmail(string email);
    }
}
