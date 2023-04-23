using System.Collections.Generic;
using DATA.Entity.MovieBaseEntities;

namespace DATA.Entity
{
    public class MovieEntity:MovieBaseEntityWithId
    {
        public int page {get; set; }
        public int total_results {get; set; }
        public int total_pages {get; set; }
        public List<Film> results {get; set; }
        
    }
}