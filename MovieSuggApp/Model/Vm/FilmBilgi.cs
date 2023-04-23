using System.Collections.Generic;
using DATA.Entity;

namespace MovieSuggApp.Model.Vm
{
    public class FilmBilgi
    {
        public long Id { get; set; }
        public long GelenEntityId { get; set; }
        public string FilmAdi { get; set; }
        public long OySayisi { get; set; }
        public float OyOrtalamasi { get; set; }
        public bool Yetiskin { get; set; }
        public string Dil { get; set; }
        public string Aciklama { get; set; }
        
        public List<Notlar> kullaniciNotlari { get; set; }
        public List<Puanlar> kullaniciPuanlari { get; set; }
        
    }
}