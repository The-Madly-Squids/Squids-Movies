using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;


namespace SquidsMovieApp.Data.Models
{
    public partial class User 
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }      
        public int MoneyBalance { get;set; }

        public virtual ICollection<Participant> LikedActors { get; set; }
        public virtual ICollection<Participant> LikedDirectors { get; set; }
        public virtual ICollection<Movie> LikedMovies { get; set; }
        public virtual ICollection<Movie> BoughtMovies { get; set; }
        public virtual ICollection<User> Following { get; set; }
        public virtual ICollection<User> Followers { get; set; }

    }
}
