using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Models
{
    public class Result<T> where T : class
    { 
        public T? Data { get; set; }    

        public int Status { get; set; } 
        public bool IsSuccess { get; set; } 

        public string Message { get; set; }




        //Successfull retriving
        public Result(T data, int status, string message)
        {
            Data = data;
            IsSuccess = true;
            Message = message;
            Status = status;
        }

        public Result (int status, string error )
        {
            Data = default (T);
            IsSuccess = false;
            Message = error;
            Status = status;
        }
    }
}
