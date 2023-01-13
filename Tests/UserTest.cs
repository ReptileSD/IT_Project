using Xunit;
using Moq;

using Domain.Models;
using Domain.Logic;
using Domain.UseCases;
using System;

namespace Tests
{
    public class UserTest
    {
        private readonly Mock<IUserRepository> _mock;
        private readonly UserService _service;

        public UserTest()
        {
            _mock = new Mock<IUserRepository>();
            _service = new UserService(_mock.Object);
        }

        [Fact]
        public void UserNotFound()
        {
            var result = _service.GetUserByLogin(String.Empty);

            Assert.Equal("User not found", result.Error);
            Assert.True(result.Failure);

            _mock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => null);
            result = _service.GetUserByLogin("qwertyuiop");
            Assert.Equal("User not found", result.Error);
            Assert.True(result.Failure);
        }

        [Fact]
        public void SignUpWithEmpty()
        {
            var result = _service.Register(new User(1, "123", "123", string.Empty, "123"));

            Assert.True(result.Failure);
            Assert.Equal("User creating error", result.Error);
        }

        [Fact]
        public void SignUpAlreadyExists()
        {
            _mock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => new User(1, "a", "a", "a", "a"));

            var result = _service.Register(new User(1, "a", "a", "a", "a"));

            Assert.True(result.Failure);
            Assert.Equal("User with this username already exists", result.Error);
        }

        [Fact]
        public void EmptyPassword()
        {
            var user = new User(1, "123", "123", "123", "");
            var check = user.IsValid();
            Assert.Equal("Empty password", check.Error);
            Assert.True(check.Failure);
        }
        [Fact]
        public void EmptyUsername()
        {
            var user = new User(2, "123", "123", "", "123");
            var check = user.IsValid();
            Assert.Equal("Empty username", check.Error);
            Assert.True(check.Failure);
        }
        [Fact]
        public void EmptyPhone()
        {
            var user = new User(3, "", "123", "123", "123");
            var check = user.IsValid();
            Assert.Equal("Empty phone", check.Error);
            Assert.True(check.Failure);
        }
        [Fact]
        public void EmptyFullname()
        {
            var user = new User(4, "123", "", "123", "123");
            var check = user.IsValid();
            Assert.Equal("Empty fullname", check.Error);
            Assert.True(check.Failure);
        }

    }
}
