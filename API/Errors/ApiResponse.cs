using System;

namespace API.Error
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);

        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad request,you have made",
                401 => "Authorized, you are not",
                404 => "resource Found , you are not",
                500 => "Errors not defined,Error",
                _ => null 
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}