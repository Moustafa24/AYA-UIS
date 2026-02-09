using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Respones
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }

        // Success response
        public static Response<T> SuccessResponse(T data)
        {
            return new Response<T>
            {
                Success = true,
                Data = data,
                Error = null
            };
        }

        // Error response
        public static Response<T> ErrorResponse(string error)
        {
            return new Response<T>
            {
                Success = false,
                Data = default(T),
                Error = error
            };
        }
    }
}