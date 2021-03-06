﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SquidsMovieApp.Common;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic;
using SquidsMovieApp.Logic.Contracts;
using SquidsMovieApp.Models;
using SquidsMovieApp.WPF.Controllers;

namespace SquidsMovieApp.Tests.Controller
{
    [TestClass]
    public class MovieControllerTests
    {
        //[ClassInitialize]
        //public static void InitilizeAutomapper(TestContext context)
        //{
        //    AutomapperConfiguration.Initialize();
        //}

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

            mockMovieService.Setup(x => x.GetMovieByTitle(It.IsAny<String>()))
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
            controller.GetMovieByTitle("TestTitle");

            // Assert
            mockMovieService.Verify(x => x.GetMovieByTitle(It.IsAny<String>()), Times.Once);
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

            var movieObjectToReturn = new Movie()
            {
                Title = "TestMovie",
                Runtime = 120
            };

            var movieModelToAdd = new MovieModel()
            {
                Title = "TestMovie",
                Runtime = 120
            };

            mockMapper.Setup(x => x.Map<MovieModel>(It.IsAny<Movie>()))
               .Returns(movieModelToAdd);

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

            var roles = new List<Role>();
            roles.Add(new Role()
            {
                RoleName = "Terminator"
            });

            var listOfAllParticipants = new List<ParticipantModel>();
            listOfAllParticipants.Add(participantModelToAdd);

            mockMapper.Setup(x => x.Map<ParticipantModel>(It.IsAny<Participant>()))
               .Returns(participantModelToAdd);
            mockMovieService.Setup(x => x.AddMovieParticipant(It.IsAny<MovieModel>(), It.IsAny<ParticipantModel>(), It.IsAny<string>()))
                .Verifiable();
            mockMovieService.Setup(x => x.GetMovieByTitle(It.IsAny<string>()))
                .Returns(movieModelToAdd);
            mockMovieService.Setup(x => x.GetAllParticipantsPerMovie(It.IsAny<MovieModel>()))
                .Returns(listOfAllParticipants);
            mockRoleService.Setup(x => x.ParticipantRolesPerMovie(It.IsAny<ParticipantModel>(), It.IsAny<MovieModel>()))
                .Returns(roles);

            effortContext.Participants.Add(participantObjectToReturn);
            effortContext.Movies.Add(movieObjectToReturn);
            effortContext.SaveChanges();
            var controller = new MovieController(mockMovieService.Object,
               mockRoleService.Object, mockMapper.Object, mockFactory.Object);

            // Act
            controller.AddMovieParticipant("TestMovie", "Pesho", "Markov", "someRole");

            // Assert  TODO
            mockMovieService.Verify(x => x.AddMovieParticipant(It.IsAny<MovieModel>(), It.IsAny<ParticipantModel>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void AddMovieParticipantShouldThrow_WhenCalledWithInvalidParameters()
        {
            var mockMovieService = new Mock<IMovieService>();
            var mockRoleService = new Mock<IRoleService>();
            var mockMapper = new Mock<IMapper>();
            var mockFactory = new Mock<IMovieModelFactory>();
            var someMovie = new Mock<MovieModel>();
            var someParticipant = new Mock<ParticipantModel>();

            var controller = new MovieController(mockMovieService.Object,
                mockRoleService.Object, mockMapper.Object, mockFactory.Object);

            mockMovieService.Setup(x => x.AddMovieParticipant(It.IsAny<MovieModel>(), It.IsAny<ParticipantModel>(), It.IsAny<string>()))
            .Verifiable();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => controller.AddMovieParticipant("", "", "", ""));

        }


    }
}
