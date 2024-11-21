using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Models
{
    public class PaginatedResult<T> : Result<T> where T : class
    {
        public int CurrentPage { get; set; }        
        public int TotalItems { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalItems / PageSize) : 0;

        public bool HasPerviousPage => CurrentPage- 1 >0 ;

        public bool HasNextPage => CurrentPage < TotalPages;

        public PaginatedResult(T data,int currentPage , int totalItems ,  int status, string message) 
            : base(data, status, message)
        {

                      
             
                CurrentPage = currentPage;
               
                TotalItems = totalItems;



            if (currentPage <= 0)
                CurrentPage = 1;

   

            if (PageSize > TotalItems)
                  PageSize = TotalItems;
            

        }

        public PaginatedResult(int status, string error) : base(status, error)
        {
            CurrentPage = 0;
            PageSize = 0;   
            TotalItems = 0;
          
        }


    }
}
