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
    //        Mapper.Initialize(cfg => cfg.CreateMap<User, UserModel>()
    //.ForMember(d => d.UserId, opt => opt.MapFrom(c => c.UserId))
    //.ForMember(d => d.FirstName, opt => opt.MapFrom(c => c.FirstName))
    //.ForMember(d => d.LastName, opt => opt.MapFrom(c => c.LastName))
    //.ForMember(d => d.Age, opt => opt.MapFrom(c => c.Age))
    //.ForMember(d => d.Username, opt => opt.MapFrom(c => c.Username))
    //.ForMember(d => d.Password, opt => opt.MapFrom(c => c.Password))
    //.ForMember(d => d.IsAdmin, opt => opt.MapFrom(c => c.IsAdmin))
    //.ForMember(d => d.MoneyBalance, opt => opt.MapFrom(c => c.MoneyBalance))
    //.ForMember(d => d.(ICollection < ParticipantModel > LikedParticipants), opt => opt.MapFrom(c => c.(ICollection < ParticipantModel > LikedParticipants)))

            // old - without reflection 
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<MovieModel, Movie>().ReverseMap();
            //});
        }
    }
}
