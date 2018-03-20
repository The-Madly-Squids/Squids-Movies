using Autofac;
using AutoMapper;
using SquidsMovieApp.Common;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic;
using SquidsMovieApp.Program.Controllers;
using SquidsMovieApp.Utilities.Parsers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Program
{
    class Program
    {
        static void Main()
        {
            //AutomapperConfig.Initialzie();

            //var builder = new ContainerBuilder();
            //builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            //var container = builder.Build();
            //var service = container.Resolve<MovieService>();
            //var posts = service.GetAllMovies();

            //foreach (var item in posts)
            //{
            //    Console.WriteLine(item.Description);
            //}


            //using (var ctx = new MovieAppDBContext())
            //{
            //    //var movieService = new MovieService(ctx);
            //    //var allMovies = movieService.GetAllMovies(ctx);
            //    //foreach (var item in allMovies)
            //    //{
            //    //    Console.WriteLine(item.Description);
            //    //}

            //    var movie = ctx.Movies.FirstOrDefault();

            //    // this is what actually automapper does in the below map
            //    // it maps according to name and type
            //    //MovieModel movie1 = new MovieModel()
            //    //{
            //    //    Description = movie.Description
            //    //};


            //    AutomapperConfig.Initialzie();
            //    MovieModel movieModel = Mapper.Map<MovieModel>(movie);
            //    Console.WriteLine(movieModel);

            //}

            //Init();
            //var builder = new ContainerBuilder();
            //builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            //var container = builder.Build();
            //var controller = container.Resolve<MovieController>();
            //controller.CreateMovie("terminator 3", "not as good", 2007, 180);
            //var movies = controller.GetAllMovies();

            //foreach (var movie in movies)
            //{
            //    Console.WriteLine(movie.Name);
            //}

            string moviesPath = @"..\..\..\SquidsMovieApp.Utilities\JsonData\moviesTest.json";
            var parser = new Parser();
            var movies = parser.ParseMovies(moviesPath);

            var ctx = new MovieAppDBContext();


        }

        //private static void Init()
        //{
        //    AutomapperConfiguration.Initialize();
        //}
    }
}
