﻿using Domain.Models;
using Domain.Logic;


namespace Domain.UseCases
{
    public class UserService
    {
        private IUserRepository _db;

        public UserService(IUserRepository db)
        {
            _db = db;
        }

        public Result<User> Register(User user)
        {
            var check = user.IsValid();
            if (check.Failure)
                return Result.Fail<User>(check.Error);

            if (_db.GetUserByLogin(user.Username))
                return Result.Fail<User>("User with this username already exists.");


            return _db.CreateUser(user) ? Result.Ok(user) : Result.Fail<User>("Error while creating user.");
        }

        public Result<User> GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<User>("Login error.");

            return _db.GetUserByLogin(login) ? Result.Ok(_db.GetUserByLogin(login)) : Result.Fail<User>("User not found.");
        }

        public Result IsUserExists(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail("Login error.");

            return _db.GetUserByLogin(login) ? Result.Ok() : Result.Fail("User not found.");
        }
    }
}