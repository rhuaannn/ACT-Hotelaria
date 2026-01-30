namespace ACT_Hotelaria.ApiResponse;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public List<string> ErroMessage { get; set; } = default!;
    
    public static ApiResponse<T>SuccesResponse(T data) => new() {Success = true, Data = data};
    public static ApiResponse<T>ErrorResponse(List<string> erroMessage) => new() {Success = false, ErroMessage = erroMessage};
}