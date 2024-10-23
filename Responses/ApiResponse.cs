public class ApiResponse<T>
{
    public bool IsSuccess { get; init; }
    public List<string>? Messages { get; init; } = [];
    public T? Data { get; init; }

    private ApiResponse(bool isSuccess, string? message, List<string>? messages, T? data)
    {
        IsSuccess = isSuccess;
        if (message != null) Messages.Add(message);
        Messages = messages;
        Data = data;
    }

    public static ApiResponse<T> Success(List<string>? messages, T? data, string message = "Success")
    => new ApiResponse<T>(true, message, messages, data);

    public static ApiResponse<T> Fail(List<string>? messages, T? data, string message = "Fail")
    => new ApiResponse<T>(false, message, messages, data);
}