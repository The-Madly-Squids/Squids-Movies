using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SquidsMovieApp.Common;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.WPF.Controllers;

namespace SquidsMovieApp.Tests.Controller
{
    [TestClass]
    public class MovieControllerTests
    {

        [TestMethod]
        public void AddMovieSuccessfullyInvokeService_WhenCalledWithValidData()
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var mockRoleService = new Mock<IRoleService>();
            var mockMapper = new Mock<IMapper>();
            var mockFactory = new Mock<IMovieModelFactory>();

            var controller = new MovieController(mockMovieService.Object,
                mockRoleService.Object, mockMapper.Object, mockFactory.Object);

            // Act
            controller.AddMovie("TestTitle", "nice movie", 1992, 120);

            //Assert
            mockMovieService.Verify(x => x.AddMovie(It.IsAny<MovieModel>()), Times.Once);
        }

        [TestMethod]
        public void AddMovieShouldThrow_WhenCalledWithInvalidParameters()
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var mockRoleService = new Mock<IRoleService>();
            var mockMapper = new Mock<IMapper>();
            var mockFactory = new Mock<IMovieModelFactory>();

            var controller = new MovieController(mockMovieService.Object,
                mockRoleService.Object, mockMapper.Object, mockFactory.Object);
            mockMovieService.Setup(x => x.AddMovie(It.IsAny<MovieModel>()))
                .Verifiable();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => controller.AddMovie("", "nice movie", 1992, 120));
        }

        [TestMethod]
        public void RemoveMovieShouldSuccesfullyInvokeService_WhenCalledWithValidParameters()
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var mockRoleService = new Mock<IRoleService>();
            var mockMapper = new Mock<IMapper>();
            var mockFactory = new Mock<IMovieModelFactory>();

            var controller = new MovieController(mockMovieService.Object,
                mockRoleService.Object, mockMapper.Object, mockFactory.Object);

            mockMovieService.Setup(x => x.GetMovie(It.IsAny<String>()))
                .Returns(new MovieModel());

            // Act
            controller.RemoveMovie("TestMovie");

            // Assert
            mockMovieService.Verify(x => x.RemoveMovie(It.IsAny<MovieModel>()), Times.Once);
        }

        [TestMethod]
        public void RemoveMovieShouldThrow_WhenCalledWithInvalidParameters()
        {
            // For Paco
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetMovieShouldSucessfullyInvokeService_WhenCalled()
        {
            var mockMovieService = new Mock<IMovieService>();
            var mockRoleService = new Mock<IRoleService>();
            var mockMapper = new Mock<IMapper>();
            var mockFactory = new Mock<IMovieModelFactory>();

            var controller = new MovieController(mockMovieService.Object,
                mockRoleService.Object, mockMapper.Object, mockFactory.Object);

            // Act
            controller.GetMovie("TestTitle");

            // Assert
            mockMovieService.Verify(x => x.GetMovie(It.IsAny<String>()), Times.Once);
        }

        [TestMethod]
        public void GetAllMovieShouldSucessfullyInvokeService_WhenCalled()
        {
            // For Tony
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddMovieParticipantShouldSucessfullyInvokeService_WhenCalledWithValidParameters()
        {
            // For Paco
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddMovieParticipantShouldThrow_WhenCalledWithInvalidParameters()
        {
            // For Tony
            throw new NotImplementedException();
        }


    }
}
