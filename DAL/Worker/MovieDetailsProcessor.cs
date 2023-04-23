using System;
using System.Linq;
using DAL.MovieRest.MovieRestClient;
using DAL.MovieRest.MovieRestRequest;
using DATA.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace DAL.Worker
{
    public class MovieDetailsProcessor
    {
        private readonly IConfiguration _config;
        private readonly string filmlerUrl;
        public MovieDetailsProcessor(IConfiguration config)
        {
            _config = config;
            filmlerUrl = _config.GetSection("TheMovieAPI").GetSection("getMoviesPage").Value;
        }

        public void GetMovies(ref int page,ref int totalLastPage)
        {
            try
            {
                MovieRestClient client = MovieRestClientFactory.GetNewMovieRestClient(filmlerUrl);
                MovieRestRequest sorgulaReq = null;

                using (var db = new MovieContext(new DbContextOptions<MovieContext>()))
                {
                    if (page != 1)
                    {
                        var lastpage = db.MovieEntity.LastOrDefault();
                        page = lastpage == null ? 1 : lastpage.page+1;
                    }

                    sorgulaReq = new MovieRestRequest();
                    sorgulaReq.AddParameter("page",page);
                    
                    RestResponse sorgulaResponse = client.Execute(sorgulaReq);
                    var sonuc=JsonConvert.DeserializeObject<MovieEntity>(sorgulaResponse.Content);

                    
                    //son sayfa film listesi çekildiğinde sayfa dolmamış ise ve yeni filmler gelmiş ise son sayfa silinecek ve yeniden eklenecek.
                    if (totalLastPage == sonuc.page && page != 1)
                    {
                        var sonPage = db.MovieEntity.Where(w => w.page == sonuc.page).FirstOrDefault();
                        db.MovieEntity.Remove(sonPage);
                    }

                    // var kayitliMi = db.MovieEntity.Where(w => w.page == sonuc.page);
                    // if (kayitliMi.Any())
                    // {
                    //     throw new Exception("Veritabanında kayıtlı filmler sayfası");
                    // }
                    
                    db.MovieEntity.Add(sonuc);
                    db.SaveChanges();
                    page++;
                    totalLastPage = sonuc.total_pages;
                }    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
             
        }
    }
}