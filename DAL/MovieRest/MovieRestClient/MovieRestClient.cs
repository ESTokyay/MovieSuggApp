using System.Threading.Tasks;
using RestSharp;

namespace DAL.MovieRest.MovieRestClient
{
    public class MovieRestClientFactory
    {
        public static MovieRestClient GetNewMovieRestClient(string baseUrl = null)
        {
            MovieRestClient client = new MovieRestClient(baseUrl);
            return client;
        }
    }

    public class MovieRestClient:RestClient
    {
        public MovieRestClient(string _baseUrl) : base(_baseUrl)
        {
            
        }

        public RestResponse Execute(RestRequest request)
        {
            RestResponse response = base.Execute(request);
            return response;
        }

        public async Task<RestResponse> ExecuteAsync(RestRequest request)
        {
            RestResponse response = await base.ExecuteAsync(request);
            return response;
        }
    }
}