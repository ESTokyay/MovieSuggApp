using System;
using System.Threading;
using System.Threading.Tasks;
using DAL.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace MovieSuggApp.Util.WorkerServices
{
    public class MovieTakeWorkerServices : BackgroundService
    {
        private readonly ILogger<MovieTakeWorkerServices> _logger;
        private readonly IConfiguration _conf;
        private int page = 1;
        private int totalLastPage=1;

        public MovieTakeWorkerServices(ILogger<MovieTakeWorkerServices> logger,IConfiguration conf_inj)
        {
            _logger = logger;
            _conf = conf_inj;
        }

        private void start_GetMovies_API_Call()
        {
            try
            {
                MovieDetailsProcessor p = new MovieDetailsProcessor(_conf);
                p.GetMovies(ref page,ref totalLastPage);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    DateTime now = DateTime.Now;
                  //  int sec = now.Minute;
                    int sec = 0;
                 
                
                    if(sec == 0 && totalLastPage >= page) //saat başı çalışacak.
                    {
                        _logger.LogInformation("Film apiden veri alıyor.");
                        start_GetMovies_API_Call();
                    }
                    _logger.LogInformation("Film apiden veri alıyor.");
                    await Task.Delay(new TimeSpan(0, 25, 0), stoppingToken);
                
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Hata: "+e.Message);
            }
            _logger.LogInformation("Background servis durduruldu.");
        }
    }
}





