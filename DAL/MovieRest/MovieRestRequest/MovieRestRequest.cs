using RestSharp;

namespace DAL.MovieRest.MovieRestRequest
{
    public class MovieRestRequest : RestRequest
    {
        public MovieRestRequest(Method method=Method.Get, string url="") : base(url, method)
        {
            
        }
    }
}