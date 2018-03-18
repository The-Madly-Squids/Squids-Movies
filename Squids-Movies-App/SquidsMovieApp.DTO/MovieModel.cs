using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.DTO
{
    public class MovieModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public int? Rating { get; set; }
        public int? RunningTime { get; set; }
    }
}
