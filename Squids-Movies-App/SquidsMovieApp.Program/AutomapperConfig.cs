using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SquidsMovieApp.Models;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Program
{
    public static class AutomapperConfig
    {
        public static void Initialzie()
        {
            // old - without reflection 
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<MovieModel, Movie>().ReverseMap();
            //});
        }
    }
}
