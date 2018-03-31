using AutoMapper;
using SquidsMovieApp.Common.Mapping;
using SquidsMovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.DTO
{
    public class GenreModel : IMapFrom<Genre>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string GenreType { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}
