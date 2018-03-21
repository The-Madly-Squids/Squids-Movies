using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SquidsMovieApp.Common;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic;

namespace SquidsMovieApp.Tests.Service
{
    [TestClass]
    public class MovieServiceTests
    {
        [ClassInitialize]
        public static void InitilizeAutomapper(TestContext context)
        {
            AutomapperConfiguration.Initialize();
        }

        [TestMethod]
        public void AddMovieShouldCorrectlyAddMovieToDb_WhenCalledWithValidData()
        {
            // arrange
            var effortContext = new MovieAppDBContext(
                        Effort.DbConnectionFactory.CreateTransient());
            var mapperMock = new Mock<IMapper>();

            var movieObjectToReturn = new Movie()
            {
                Title = "Test Movie Object",
                Runtime = 120
            };

            mapperMock.Setup(x => x.Map<Movie>(It.IsAny<MovieModel>()))
                .Returns(movieObjectToReturn);

            var movieModelToAdd = new MovieModel()
            {
                Title = "Test Movie model",
                Runtime = 120
            };

            // Act
            var sut = new MovieService(effortContext, mapperMock.Object);
            sut.AddMovie(movieModelToAdd);

            Assert.AreEqual(1, effortContext.Movies.Count());
        }

        [TestMethod]
        public void RemoveMovieShould_CorrectlyRemoveMovieFromDB()
        {
            // Arrange
            var effortContext = new MovieAppDBContext(
                Effort.DbConnectionFactory.CreateTransient());

            var mapperMock = new Mock<IMapper>();

            var movieDtoToRemove = new MovieModel()
            {
                Title = "Test title",
                Runtime = 120
            };

            var movieObjectToReturn = new Movie()
            {
                Title = "Test title",
                Runtime = 120
            };

            mapperMock.Setup(x => x.Map<Movie>(It.IsAny<MovieModel>()))
               .Returns(movieObjectToReturn);
            // Is this the correct way to simulate adding a movie in DB?
            // mock somehow ?
            effortContext.Movies.Add(movieObjectToReturn);
            effortContext.SaveChanges();

            // act
            var sut = new MovieService(effortContext, mapperMock.Object);
            sut.RemoveMovie(movieDtoToRemove);

            //Assert
            Assert.AreEqual(0, effortContext.Movies.Count());
        }

        [TestMethod]
        public void GetAllMoviesShould_ReturnCorrectDataWhenCalled()
        {
            // Arrange
            var effortContext = new MovieAppDBContext(
                Effort.DbConnectionFactory.CreateTransient());

            var mapperMock = new Mock<IMapper>();


            // Fill DB with movies that will be returned
            for (int i = 0; i < 10; i++)
            {
                var movieToReturn = new Movie()
                {
                    Title = "Test title" + i,
                    Runtime = 120 + i
                };
                effortContext.Movies.Add(movieToReturn);
            }
            effortContext.SaveChanges();

            // Act
            var sut = new MovieService(effortContext, mapperMock.Object);
            var result = sut.GetAllMovies();
            // is this correct? Can you use ProjectTo in test method?
            var expectedResult = effortContext.Movies.ProjectTo<MovieModel>();

            foreach (var movieModel in result)
            {
                bool exists = expectedResult.Any(x => x.MovieId == movieModel.MovieId);
                Assert.IsTrue(exists);
            }
        }
    }
}
