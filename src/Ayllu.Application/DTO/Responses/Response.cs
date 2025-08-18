namespace Ayllu.Application.DTO.Responses;

public class Response 
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
}
public class Response<T> : Response
{
    public T Data { get; set; } = default!;
}
