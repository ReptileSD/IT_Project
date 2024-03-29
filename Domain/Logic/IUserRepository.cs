﻿using Domain.Models;

namespace Domain.Logic
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserExists(string login);
        User GetUserByLogin(string login);
        bool CreateUser(User user);
    }
}
