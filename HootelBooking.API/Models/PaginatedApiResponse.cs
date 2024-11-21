using System.Net;

namespace HootelBooking.API.Models
{
    public class PaginatedApiResponse<T> : ApiResponse<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int TotalPages {  get; set; }    
        public bool HasPreviousPage { get; set;  } 
        public bool HasNextPage { get; set;  }
        public PaginatedApiResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
                         
        }

        public PaginatedApiResponse(T data, int currentPage, int totalItems, int pageSize, int totalPages , bool hasPreviousPage, bool hasNextPage, string message, HttpStatusCode statusCode) : base(data, message, statusCode)
        { 
          CurrentPage = currentPage;    
          TotalItems = totalItems;
          PageSize = pageSize;
          TotalPages = totalPages;
          HasPreviousPage = hasPreviousPage;
          HasNextPage = hasNextPage;
        }


    }



}
