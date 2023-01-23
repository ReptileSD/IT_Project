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
        private readonly UserInteractor _service;

        public UserTest()
        {
            _mock = new Mock<IUserRepository>();
            _service = new UserInteractor(_mock.Object);
        }

        [Fact]
        public void UserNotFound()
        {
            var result = _service.GetUserByLogin(String.Empty);

            Assert.Contains("User not found", result.Error);
            Assert.True(result.isFailure);

            _mock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => null);
            result = _service.GetUserByLogin("qwertyuiop");
            Assert.Contains("User not found", result.Error);
            Assert.True(result.isFailure);
        }

        [Fact]
        public void SignUpWithEmpty()
        {
            var result = _service.Register(new User(1, "123", "123", string.Empty, "123"));

            Assert.True(result.isFailure);
            Assert.Contains("User creating error", result.Error);
        }

        [Fact]
        public void SignUpAlreadyExists()
        {
            _mock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => new User(1, "a", "a", "a", "a"));

            var result = _service.Register(new User(1, "a", "a", "a", "a"));

            Assert.True(result.isFailure);
            Assert.Contains("User with this Username already exists", result.Error);
        }

        [Fact]
        public void EmptyPassword()
        {
            var user = new User(1, "123", "123", "123", "");
            var check = user.IsValid();
            Assert.Contains("Empty password", check.Error);
            Assert.True(check.isFailure);
        }
        [Fact]
        public void EmptyUsername()
        {
            var user = new User(2, "123", "123", "", "123");
            var check = user.IsValid();
            Assert.Contains("Empty Username", check.Error);
            Assert.True(check.isFailure);
        }
        [Fact]
        public void EmptyPhone()
        {
            var user = new User(3, "", "123", "123", "123");
            var check = user.IsValid();
            Assert.Contains("Empty phone", check.Error);
            Assert.True(check.isFailure);
        }
        [Fact]
        public void EmptyFullname()
        {
            var user = new User(4, "123", "", "123", "123");
            var check = user.IsValid();
            Assert.Contains("Empty fullname", check.Error);
            Assert.True(check.isFailure);
        }

    }
}
