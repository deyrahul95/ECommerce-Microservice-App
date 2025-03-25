using System.Net;

namespace ECommerce.Shared.Models;

public record ServiceResult(HttpStatusCode StatusCode, string Message = "");

public record ServiceResult<T>(HttpStatusCode StatusCode, string Message = "", T? Data = null)
    : ServiceResult(StatusCode, Message) where T : class;
