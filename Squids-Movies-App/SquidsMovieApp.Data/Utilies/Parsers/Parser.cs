using Newtonsoft.Json;
using SquidsMovieApp.Data.Utilities.Parsers.Models;
using System.Collections.Generic;
using System.IO;

namespace SquidsMovieApp.Data.Utilities.Parsers
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
