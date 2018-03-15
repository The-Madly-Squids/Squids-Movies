using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string GenreType { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
