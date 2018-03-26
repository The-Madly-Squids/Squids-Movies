using SquidsMovieApp.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Models
{
    public class Participant
    {
        public Participant()
        {
            this.Movies = new HashSet<Movie>();
            this.ParticipantLikedByUser = new HashSet<User>();
        }

        public int ParticipantId { get; set; }
        [Required]
        [StringLength(GlobalConstants.MaxParticipantFirstNameLength, MinimumLength = GlobalConstants.MinParticipantFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxParticipantLastNameLength, MinimumLength = GlobalConstants.MinParticipantLastNameLength)]
        public string LastName { get; set; }

        [Range(GlobalConstants.MinParticipantAge, GlobalConstants.MaxParticipantAge)]
        public int? Age { get; set; }

        [StringLength(GlobalConstants.MaxParticipantBioLength, MinimumLength = GlobalConstants.MinParticipantBioLength)]
        public string Bio { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<User> ParticipantLikedByUser { get; set; }
    }


}
