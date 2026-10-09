using System.Net;
namespace FirstSprintProject;

public class ApiResult<T> : ApiBaseResult
{
    // Возвращаемые данные метода
    public required T Data { get; set; }
}

public class ApiBaseResult
{
    public required bool Success { get; set; }
    public required HttpStatusCode StatusCode { get; set; }
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
    public required string Message { get; set; }
}
