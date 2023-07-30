using Newtonsoft.Json;

namespace DespachoOtimizado.Application
{
    public struct OptimizerResponseDTO
    {
        public OptimizerResponseDTO(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        [JsonProperty("message")]
        public string Message { get; set; } 
        public bool Success { get; set; } = false;
    }
}