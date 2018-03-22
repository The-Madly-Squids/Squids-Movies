using Newtonsoft.Json;
using SquidsMovieApp.Utilities.Parsers.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SquidsMovieApp.Utilities.Parsers
{
    public class Parser
    {
        public ICollection<MovieParsedModel> ParseMovies(string path)
        {
            return JsonConvert.DeserializeObject<List<MovieParsedModel>>(File.ReadAllText(path));
        }

        public ActorParsedModel ParseParticipantBio(string participant)
        {
            return JsonConvert.DeserializeObject<ActorParsedModel>(participant);
        }
    }
}
