using System.Net;

namespace ECommerce.Shared.Models;

public record ServiceResult(HttpStatusCode StatusCode, string Message = "");

public record ServiceResult<T>(T Data, HttpStatusCode StatusCode, string Message = "")
    : ServiceResult(StatusCode, Message) where T : class;
