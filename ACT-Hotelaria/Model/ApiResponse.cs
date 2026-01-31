namespace ACT_Hotelaria.ApiResponse;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; } 
    public T Data { get; set; }
    public List<string> ErroMessage { get; set; } = default!;
    public static ApiResponse<T>SuccesResponse(T data, int statusCode) => new() {Success = true, Data = data, StatusCode = statusCode};
    public static ApiResponse<T>ErrorResponse(List<string> erroMessage, int statusCode) => new() {Success = false, ErroMessage = erroMessage, StatusCode = statusCode};
}