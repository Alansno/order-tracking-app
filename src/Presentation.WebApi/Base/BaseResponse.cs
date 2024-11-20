using System.Net;
using Newtonsoft.Json;

namespace Presentation.WebApi.Base;

public class BaseResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
    public int StatusCode { get; set; }

    [JsonIgnore]
    public HttpStatusCode HttpStatus { get; set; }

    public BaseResponse() { } 

    public BaseResponse(object data, string message, bool success, HttpStatusCode httpStatus)
    {
        Data = data;
        Message = message;
        Success = success;
        StatusCode = (int)httpStatus;
        HttpStatus = httpStatus;
    }

    public static BaseResponse Ok(object data, string message = "Operation successful")
        => new BaseResponse(data, message, true, HttpStatusCode.OK);

    public static BaseResponse Created(object data, string message = "Model created successfully")
        => new BaseResponse(data, message, true, HttpStatusCode.Created);

    public static BaseResponse Error(string message, HttpStatusCode status)
        => new BaseResponse(null, message, false, status);
}