using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Models
{
    public class Participant
    {
        public Participant()
           
        {
            this.Movies = new HashSet<Movie>();
        }

        public int ParticipantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }

    }


}
