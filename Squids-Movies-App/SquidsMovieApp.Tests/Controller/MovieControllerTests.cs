using System;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SquidsMovieApp.Common;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.DTO;
using SquidsMovieApp.DTO.Contracts;
using SquidsMovieApp.Logic;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.Models;
using SquidsMovieApp.WPF.Controllers;

namespace SquidsMovieApp.Tests.Controller
{
    [TestClass]
    public class MovieControllerTests
    {
        [ClassInitialize]
        public static void InitilizeAutomapper(TestContext context)
        {
            AutomapperConfiguration.Initialize();
        }

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
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var mockRoleService = new Mock<IRoleService>();
            var mockMapper = new Mock<IMapper>();
            var mockFactory = new Mock<IMovieModelFactory>();

            var controller = new MovieController(mockMovieService.Object,
                mockRoleService.Object, mockMapper.Object, mockFactory.Object);
            mockMovieService.Setup(x => x.RemoveMovie(It.IsAny<MovieModel>()))
                .Verifiable();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => controller.RemoveMovie(""));

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
            var mockMovieService = new Mock<IMovieService>();
            var mockRoleService = new Mock<IRoleService>();
            var mockMapper = new Mock<IMapper>();
            var mockFactory = new Mock<IMovieModelFactory>();

            var controller = new MovieController(mockMovieService.Object,
                mockRoleService.Object, mockMapper.Object, mockFactory.Object);

            // Act
            controller.GetAllMovies();

            // Assert
            mockMovieService.Verify(x => x.GetAllMovies(), Times.Once);
        }

        [TestMethod]
        public void AddMovieParticipantShouldSucessfullyInvokeService_WhenCalledWithValidParameters()
        {
            //Arrange
            var effortContext = new MovieAppDBContext(
                        Effort.DbConnectionFactory.CreateTransient());
            var mockMovieService = new Mock<IMovieService>();
            var mockRoleService = new Mock<IRoleService>();
            var mockMapper = new Mock<IMapper>();
            var mockFactory = new Mock<IMovieModelFactory>();

            var controller = new MovieController(mockMovieService.Object,
                mockRoleService.Object, mockMapper.Object, mockFactory.Object);
            
            var movieObjectToReturn = new Movie()
            {
                Title = "Test Movie Object",
                Runtime = 120
            };

            var movieModelToAdd = new MovieModel()
            {
                Title = "Test Movie model",
                Runtime = 120
            };

            mockMapper.Setup(x => x.Map<Movie>(It.IsAny<MovieModel>()))
               .Returns(movieObjectToReturn);

            var participantModelToAdd = new ParticipantModel()
            {
                FirstName = "Pesho",
                LastName = "Markov",
                Age = 21
            };

            var participantObjectToReturn = new Participant()
            {
                FirstName = "Pesho",
                LastName = "Markov",
                Age = 21
            };

            //mockMovieService.Setup(x => x.AddMovieParticipant(It.IsAny<MovieModel>(), It.IsAny<ParticipantModel>(), It.IsAny<string>()))
            //    .Verifiable();
            mockMovieService.Setup(x => x.AddMovieParticipant(movieModelToAdd, participantModelToAdd, "someRole"))
                .Verifiable();


            mockMapper.Setup(x => x.Map<Participant>(It.IsAny<ParticipantModel>()))
               .Returns(participantObjectToReturn);
            var sut = new MovieService(effortContext, mockMapper.Object);

            effortContext.Movies.Add(movieObjectToReturn);
            effortContext.Participants.Add(participantObjectToReturn);
            // Act
            controller.AddMovieParticipant("Test Movie Model", "Pesho", "Markov", "someRole");
            //sut.AddMovie(movieModelToAdd);

            // Assert  TODO
            //Assert.AreEqual(1, effortContext.Participants.Count());
            mockMovieService.Verify(x => x.AddMovieParticipant(It.IsAny<MovieModel>(), It.IsAny<ParticipantModel>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void AddMovieParticipantShouldThrow_WhenCalledWithInvalidParameters()
        {
            var mockMovieService = new Mock<IMovieService>();
            var mockRoleService = new Mock<IRoleService>();
            var mockMapper = new Mock<IMapper>();
            var mockFactory = new Mock<IMovieModelFactory>();
            var someMovie = new Mock<IMovieModel>();
            var someParticipant = new Mock<IParticipantModel>();

            var controller = new MovieController(mockMovieService.Object,
                mockRoleService.Object, mockMapper.Object, mockFactory.Object);

                mockMovieService.Setup(x => x.AddMovieParticipant(It.IsAny<MovieModel>(), It.IsAny<ParticipantModel>(), It.IsAny<string>()))
                .Verifiable();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => controller.AddMovieParticipant("", "", "", ""));

        }


    }
}
