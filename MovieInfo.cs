using Newtonsoft.Json;

namespace WebAPI
{
    public class MovieInfo
    {
        public string movie;
        public int episode { get; set; }
        public string released { get; set; }

        public MovieInfo(string movie, int episode, string released)
        {
            this.movie = movie;
            this.episode = episode;
            this.released = released;
        }
    }

    
}