using DATA.Entity.MovieBaseEntities;

namespace DATA.Entity
{
    public class Notlar :MovieBaseEntityWithId
    {
        public string Icerik { get; set; }
        public Film film { get; set; }
        
        public Users kullanici { get; set; }
    }
}