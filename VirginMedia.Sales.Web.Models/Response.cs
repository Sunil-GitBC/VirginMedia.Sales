using System.Net;

namespace VirginMedia.Sales.Web.Models
{
    public class Response<T> where T : class
    {
        public T? Data { get; set; }
        public PagingData PagingData { get; set; }
        public List<string>? Errors { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}