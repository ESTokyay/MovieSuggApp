using DATA.Entity.MovieBaseEntities;

namespace DATA.Entity
{
    public class Puanlar: MovieBaseEntityWithId
    {
        public int Puan { get; set; }
        public Film film { get; set; }
        public Users kullanici { get; set; }
    }
}