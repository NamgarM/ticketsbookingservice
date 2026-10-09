namespace FirstSprintProject.Dtos.Responses;

public class ApiResult<T> : ApiBaseResult
{
    public required T Data { get; set; }
}