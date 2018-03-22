using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Models
{
    public class Participant
    {
        public Participant()

        {
            //this.Movies = new HashSet<Movie>();
            this.ActorLikedBy = new HashSet<User>();
            this.DirectorLikedBy = new HashSet<User>();
            //this.Roles = new HashSet<Role>();
        }

        public int ParticipantId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }
        public int? Age { get; set; }

        [StringLength(250, MinimumLength = 2)]
        public string Bio { get; set; }

        //public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<User> ActorLikedBy { get; set; }
        public virtual ICollection<User> DirectorLikedBy { get; set; }

        //public virtual ICollection<Role> Roles { get; set; }
    }


}
