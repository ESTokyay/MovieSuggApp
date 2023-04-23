using System.Collections.Generic;
using DATA.Entity.MovieBaseEntities;

namespace DATA.Entity
{
    public class Film: MovieBaseEntityWithId
    {
        public bool adult { get; set; }
        public bool video { get; set; }
        public string backdrop_path { get; set; }
        //public List<long> genre_ids { get; set; }
        public int id { get; set; }
        public int vote_count { get; set; }
        public string original_language { get; set; }
        public string release_date { get; set; }
        public string original_title { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public decimal vote_average { get; set; }
        public decimal popularity { get; set; }
        
        public int MovieEntityId { get; set; }
        public MovieEntity MovieEntity { get; set; }
        
        public List<Notlar> notlar_list { get; set; }
        public List<Puanlar> puanlar_list { get; set; }
    }
}