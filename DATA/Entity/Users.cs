using System.Collections.Generic;
using DATA.Entity.MovieBaseEntities;

namespace DATA.Entity
{
    public class Users : MovieBaseEntityWithId
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        
        public List<Puanlar> puanlar {get; set; }
        public List<Notlar> notlar {get; set; }
    }
}