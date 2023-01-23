using Domain.Models;


namespace Domain.Logic
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetUserByLogin(string login);
    }
}