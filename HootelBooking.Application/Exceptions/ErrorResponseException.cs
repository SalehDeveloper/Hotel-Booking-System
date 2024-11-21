using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Exceptions
{
    public class ErrorResponseException:Exception
    {
        public  int StatusCode { get; }

        public string Error { get; }
        public ErrorResponseException(int statusCode ,string message , string error):base(message)
        {

            Error = error;
            StatusCode = statusCode;   
        }
    }
}
