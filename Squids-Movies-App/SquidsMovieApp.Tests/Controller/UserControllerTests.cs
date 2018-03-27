using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SquidsMovieApp.Core.Factories;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.WPF.Controllers;

namespace SquidsMovieApp.Tests.Controller
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void AddUserShouldCorrectlyInvokeService_WhenCalledWithValidData()
        {

            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<UserModelFactory>();

            var firstName = "Katerina";
            var lastName = "Bozhilova";
            var age = 20;
            var email = "kateto1998@abv.bg";
            var nickName = "masterProgrammer1998";
            var password = "1234567";
            var isAdmin = false;
            var moneyBalance = 128382382.255M;

            factoryMock.Setup(x => x.CreateUserModel(firstName, lastName, age, nickName,
                email, password, isAdmin, moneyBalance))
                .Returns(new UserModel());

            // Act
            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
                mapperMock.Object, factoryMock.Object);
            sut.CreateUser(firstName, lastName, nickName, email, password, age, moneyBalance, isAdmin);

            // Assert
            userServiceMock.Verify(x => x.AddUser(It.IsAny<UserModel>()), Times.Once);

        }
    }
}
