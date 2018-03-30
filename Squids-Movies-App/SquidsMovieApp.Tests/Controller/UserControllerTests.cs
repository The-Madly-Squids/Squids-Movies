using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SquidsMovieApp.Core.Factories;
using SquidsMovieApp.Core.Factories.Contracts;
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
            // : 'Invalid setup on a non-virtual (overridable in VB) member
            // because the mock was not interface
            var factoryMock = new Mock<IUserFactory>();

            var firstName = "Katerina";
            var lastName = "Bozhilova";
            var age = 20;
            var email = "kateto1998@abv.bg";
            var nickName = "masterProgrammer1998";
            var password = "1234567";
            var isAdmin = false;
            var moneyBalance = 128382382.255M;

            // Could use It.IsAny<> here aswell
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

        [TestMethod]
        public void RemoveUserShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.GetUser(It.IsAny<string>()))
                .Returns(new UserModel());

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.RemoveUser("randomString@abv.com");

            // Assert
            userServiceMock.Verify(x => x.RemoveUser(It.IsAny<UserModel>()), Times.Once);
        }

        [TestMethod]
        public void GetAllUsersShouldCorrectlyInvokeService_WhenCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.GetAllUsers())
                .Returns(new List<UserModel>());

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.GetAllUsers();

            // Assert
            userServiceMock.Verify(x => x.GetAllUsers(), Times.Once);
        }

        [TestMethod]
        public void GetUserShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.GetUser(It.IsAny<string>()))
                .Returns(new UserModel());

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.GetUser("randomName");

            // Assert
            userServiceMock.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void GetUserByEmailShouldCorrectlyInvokeService_WhenCalledWithValidUserEmail()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.GetUserByEmail(It.IsAny<string>()))
                .Returns(new UserModel());

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.GetUserByEmail("randomEmail@abv.com");

            // Assert
            userServiceMock.Verify(x => x.GetUserByEmail(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void GetLikedParticipantsShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.GetLikedParticipants(It.IsAny<UserModel>()))
                .Returns(new List<ParticipantModel>());

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            IEnumerable<ParticipantModel> participants =
                sut.GetLikedParticipants("randomUserName");

            // Assert
            userServiceMock.Verify(x => x.GetLikedParticipants(It.IsAny<UserModel>()), Times.Once);
        }

        [TestMethod]
        public void GetLikedMoviesShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.GetLikedMovies(It.IsAny<UserModel>()))
                .Returns(new List<MovieModel>());

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.GetLikedMovies("randomUserName");

            // Assert
            userServiceMock.Verify(x => x.GetLikedMovies(It.IsAny<UserModel>()), Times.Once);
        }

        [TestMethod]
        public void GetBoughtMoviesShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.GetBoughtMovies(It.IsAny<UserModel>()))
                .Returns(new List<MovieModel>());

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.GetBoughtMovies("randomUserName");

            // Assert
            userServiceMock.Verify(x => x.GetBoughtMovies(It.IsAny<UserModel>()), Times.Once);
        }

        [TestMethod]
        public void GetFollowersShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.GetFollowers(It.IsAny<UserModel>()))
                .Returns(new List<UserModel>());

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.GetFollowers("randomUserName");

            // Assert
            userServiceMock.Verify(x => x.GetFollowers(It.IsAny<UserModel>()), Times.Once);
        }

        [TestMethod]
        public void GetFollowedShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.GetFollowed(It.IsAny<UserModel>()))
                .Returns(new List<UserModel>());

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.GetFollowed("randomUserName");

            // Assert
            userServiceMock.Verify(x => x.GetFollowed(It.IsAny<UserModel>()), Times.Once);
        }

        [TestMethod]
        public void GetMoneyBalanceShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.GetMoneyBalance(It.IsAny<UserModel>()))
                .Returns(It.IsAny<decimal>());

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.GetMoneyBalance("randomUserName");

            // Assert
            userServiceMock.Verify(x => x.GetMoneyBalance(It.IsAny<UserModel>()), Times.Once);
        }

        [TestMethod]
        public void AddMoneyToBalanceShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.AddMoneyToBalance(It.IsAny<UserModel>(), It.IsAny<decimal>()))
                .Verifiable();

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.AddMoneyToBalance("randomUserName", 1000M);

            // Assert
            userServiceMock.Verify(x => x.AddMoneyToBalance(It.IsAny<UserModel>(), It.IsAny<decimal>()), Times.Once);
        }

        [TestMethod]
        public void LikeParticipantShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.LikeParticipant(It.IsAny<UserModel>(), It.IsAny<ParticipantModel>()))
                .Verifiable();

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.LikeParticipant("randomUserName", "randomFirstNameParticipant", "randomLastName");

            // Assert
            userServiceMock.Verify(x => x.LikeParticipant(It.IsAny<UserModel>(), It.IsAny<ParticipantModel>()), Times.Once);
        }

        [TestMethod]
        public void FollowUserShouldCorrectlyInvokeService_WhenCalledWithValidUserName()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var participantServiceMock = new Mock<IParticipantService>();
            var mapperMock = new Mock<IMapper>();
            var factoryMock = new Mock<IUserFactory>();

            userServiceMock.Setup(x => x.FollowUser(It.IsAny<UserModel>(), It.IsAny<UserModel>()))
                .Verifiable();

            var sut = new UserController(userServiceMock.Object, participantServiceMock.Object,
              mapperMock.Object, factoryMock.Object);

            // Act
            sut.FollowUser("randomUserName", "randomUserToFollow");

            // Assert
            userServiceMock.Verify(x => x.FollowUser(It.IsAny<UserModel>(), It.IsAny<UserModel>()), Times.Once);
        }
    }
}
