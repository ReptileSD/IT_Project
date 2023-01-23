using Domain.Models;
using Domain.Logic;

namespace Domain.UseCases
{
    public class UserInteractor
    {
        private readonly IUserRepository _db;

        public UserInteractor(IUserRepository db)
        {
            _db = db;
        }

        public Result<User> Register(User user)
        {
            var check = user.IsValid();
            if (check.isFailure)
                return Result.Fail<User>(check.Error);

            if (_db.GetUserByLogin(user.Username) != null)
                return Result.Fail<User>("User with this username already exists.");
            if (_db.Create(user))
            {
                _db.Save();
                return Result.Ok(user);
            }

            return Result.Fail<User>("User creating error");
        }
        public Result<User> GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<User>("Login error.");

            return _db.GetUserByLogin(login) != null ? Result.Ok(_db.GetUserByLogin(login)!) : Result.Fail<User>("User not found");
        }

        public Result<User> IsUserExists(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<User>("Login error");
            var res = _db.GetUserByLogin(login);
            return res != null ? Result.Ok(res) : Result.Fail<User>("User not found");
        }
    }
}