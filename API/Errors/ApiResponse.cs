

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int StatusCode,string Message = null)
        {
            this.StatusCode = StatusCode;
            this.Message = Message ?? GetDefaultMessage(StatusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessage(int StatusCode){
            return StatusCode switch{
                404 => "Not Found",
                400 => "Bad Request",
                401 => "You are not authorized",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}