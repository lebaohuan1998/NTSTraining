using System;
namespace NTS.Common.Models
{
    public class ApiResultModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }       
    }

    public class ApiResultModel
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }

    public class ApiResultConstants
    {
        public const int StatusCodeSuccess = 1;
        public const int StatusCodeError = 2;
        public const int StatusCodeValidateError = 3;
    }

    public class ErrorValidateModel
    {
        public string Message { get; set; }
    }
}