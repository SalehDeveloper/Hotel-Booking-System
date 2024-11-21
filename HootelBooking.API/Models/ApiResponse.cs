using System.Net;

namespace HootelBooking.API.Models
{
    public class ApiResponse<T> where T : class
    {


        public HttpStatusCode StatusCode { get; set; }

        public bool Success { get; set; }
        public string Message { get; set; }
        
        public T Data { get; set; }
        
      

     

        public ApiResponse(T data, string message ,
       HttpStatusCode statusCode )
        {
            Success = true;
            StatusCode = statusCode;
            Message = message;
            Data = data;
            
        }

        //constructor for an error response 
        public ApiResponse(HttpStatusCode statusCode, string message)
        {

            Success = false;
            StatusCode = statusCode;
            Message = message;
            Data = default(T);

        }

    }



}
