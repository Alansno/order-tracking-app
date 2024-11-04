using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Custom.ResultPattern
{
    public struct Unit
    {
        public static readonly Unit Value = new Unit();
    }
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }
        public HttpStatusCode StatusCode { get; }

        protected Result(T value, bool isSuccess, string error, HttpStatusCode statusCode)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, null, HttpStatusCode.OK);

        public static Result<T> Failure(string error, HttpStatusCode statusCode) => new Result<T>(default, false, error, statusCode);
    }

    public static class Result
    {
        public static Result<Unit> Success() => Result<Unit>.Success(Unit.Value);
        public static Result<Unit> Failure(string error, HttpStatusCode statusCode) => Result<Unit>.Failure(error, statusCode);
    }
}
