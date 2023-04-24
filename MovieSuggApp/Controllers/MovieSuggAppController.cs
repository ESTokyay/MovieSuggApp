using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DAL.Context;
using DATA.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using MovieSuggApp.Model.Vm;
using MovieSuggApp.Util.BuildToken;

namespace MovieSuggApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieSuggAppController : ControllerBase
    {
        private readonly MovieContext db;
        private readonly IConfiguration config;
        public MovieSuggAppController(MovieContext _db,IConfiguration _config)
        {
            db = _db;
            config  = _config;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Users>> Register(UsersVm yeniKullaniciBilgi)
        {
            var userMevcutMu = db.Kullanicilar.Where(w => w.UserName == yeniKullaniciBilgi.UserName);
            if (userMevcutMu.Any())
            {
                return BadRequest("Bu kullanıcı sistemde mevcut.");
            }
            
            Users user = new Users();
            CreatePasswordHash(yeniKullaniciBilgi.Password, out byte[] passwordHash,out byte[] passwordSalt);
            user.UserName = yeniKullaniciBilgi.UserName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Email = yeniKullaniciBilgi.Email;
            
            db.Kullanicilar.Add(user);
            db.SaveChanges();
            
            return Ok();
        }
        [HttpPost("login")]
        public IActionResult login(UsersVm girisBilgileri)
        {
            var user = db.Kullanicilar.Where(w => w.UserName == girisBilgileri.UserName);
            if (!user.Any())
            {
                return BadRequest("Bu kullanıcı sistemde mevcut değil.");
            }

            var kullanici = user.FirstOrDefault();
            if (!VerifyPasswordHash(girisBilgileri.Password, kullanici.PasswordHash,kullanici.PasswordSalt))
            {
                return BadRequest("Parola sistemde ki kullanıcı ile eşleşmiyor.");
            }

            return Created("", new BuildToken().CreateToken(config,kullanici));
        }
        
        // GET: api/MovieSuggApp
        [Authorize]
        [HttpGet("TumFilmListesiSayfa")]
        public IEnumerable<object> TumFilmListesiSayfa(int sayfaBuyuklugu)
        {
            try
            {
                int count = db.Film.Count();
                int grupSayisi = (int)Math.Ceiling((double)count / sayfaBuyuklugu);    //ondalık sayı olsa bile yukarı yuvarlar.   Grup sayısını belirtecek.

                return Enumerable.Range(0, grupSayisi)
                    .Select(i => db.Film.Skip(i * sayfaBuyuklugu).Take(sayfaBuyuklugu).Select(s=>new 
                    {
                        Id=s.EntityId,
                        FilmAdi=s.title
                    }).ToList())
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Beklenmeyen hata:" + e.Message);
            }
        }
        
        [Authorize]
        [HttpGet("FilmGoruntuleId")]
        public object FilmGoruntuleId(int filmId)
        {
            try
            {
                var userName=User.Identity.Name;
                var filmMevcutMu = db.Film.AsNoTracking().Include(w=>w.puanlar_list).Include(w=>w.notlar_list).Where(w => w.EntityId == filmId);
                if (filmMevcutMu.Any())
                {
                    var film = filmMevcutMu.FirstOrDefault();
                    
                    var goruntuleFilm = new FilmBilgi();
                    goruntuleFilm.Id = film.EntityId;
                    goruntuleFilm.FilmAdi = film.title;
                    goruntuleFilm.Aciklama = film.overview;
                    goruntuleFilm.Yetiskin = film.adult;
                    goruntuleFilm.Dil = film.original_language;
                    goruntuleFilm.OySayisi = film.puanlar_list.Count;
                    goruntuleFilm.OyOrtalamasi= film.puanlar_list.Sum(s=>s.Puan)/film.puanlar_list.Count;
                    goruntuleFilm.kullaniciNotlari = db.Notlar.AsNoTracking().Include(w => w.kullanici).Where(w => w.kullanici.UserName == userName).ToList();
                    goruntuleFilm.kullaniciPuanlari = db.Puanlar.AsNoTracking().Include(w => w.kullanici).Where(w => w.kullanici.UserName == userName).ToList();
                        
                    return goruntuleFilm;
                }
                else
                {
                    return new
                    {
                        Hata="Verilen 'Id' ile listede film bulunamadı."
                    };
                }
            }
            catch (Exception e)
            {
                throw new Exception("Beklenmeyen hata:" + e.Message);
            }
            
        }
        
        [Authorize]
        [HttpPost("FilmNotPuanEkle")]
        public string FilmNotPuanEkle(string not,int puan,int secilenFilmId)
        {
            if(!(puan >= 1 && puan <= 10))
            {
                return "1 ile 10 arasında bir puan giriniz.";
            }
            
            var filmMevcutMu = db.Film.Where(w => w.EntityId == secilenFilmId);
            if (filmMevcutMu.Any())
            {
                try
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Film film = filmMevcutMu.FirstOrDefault();
                
                        var userName = User.Identity.Name;
                        var user =db.Kullanicilar.Where(w=>w.UserName == userName).FirstOrDefault();
                        
                        Notlar eklenecekNot=new Notlar();
                        eklenecekNot.Icerik = not;
                        eklenecekNot.film = film;
                        eklenecekNot.kullanici = user;
                        db.Notlar.Add(eklenecekNot);

                        Puanlar eklenecekPuan = new Puanlar();
                        eklenecekPuan.Puan = puan;
                        eklenecekPuan.kullanici = user;
                        eklenecekPuan.film = film;
                        db.Puanlar.Add(eklenecekPuan);

                        db.SaveChanges();
                        
                        transaction.Commit();
                    }

                    return "Not ve puan film için eklendi.";
                }
                catch (Exception e)
                {
                    return "Not veya puan eklenirken hata oluştu.Hata : "+e.Message;
                }
                
            }
            else
            {
                return "Verilen 'Id' ile listede film bulunamadı.";

            }
            
        }
        
        [Authorize]
        [HttpGet("FilmTavsiyeEt")]
        public string FilmTavsiyeEt(string aliciMail,int secilenFilmId)
        {
            //mail işlemleri
            string gondericiMail = config.GetSection("MailSenderSettings").GetSection("SenderMail").Value;
            string gondericiPass = config.GetSection("MailSenderSettings").GetSection("Password").Value;
            
            
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(gondericiMail, gondericiPass);
            
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress(gondericiMail);
            ePosta.To.Add(aliciMail);
            ePosta.Subject = "Film Tavsiyesi";
            
            var film = db.Film.AsNoTracking().Where(w => w.id == secilenFilmId);
            if (film.Any())
            {
                ePosta.Body= film.FirstOrDefault().title  + " = Bu filmi izlemenizi tavsiye ediyorum.";    
            }
            else
            {
                return "Verilen 'Id' ile listede film bulunamadı.";

            }

            try
            {
                client.Send(ePosta);
                return "E-posta başarıyla gönderildi.";
            }
            catch (Exception ex)
            {
                return "E-posta gönderme hatası: " + ex.Message;
            }
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
