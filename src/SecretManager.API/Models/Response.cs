namespace SecretManager.API.Models;

public class Response<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? Error { get; init; }

    public static Response<T> Ok(T data) => new() { Success = true, Data = data, Error = null };
    public static Response<T> Fail(string error) => new() { Success = false, Error = error };

}