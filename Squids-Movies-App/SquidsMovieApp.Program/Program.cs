using Autofac;
using AutoMapper;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic;
using System;
using System.Collections.Generic;
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
            AutomapperConfig.Initialzie();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var service = container.Resolve<MovieService>();
            var posts = service.GetAllMovies();

            foreach (var item in posts)
            {
                Console.WriteLine(item.Description);
            }


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
        }
    }
}
