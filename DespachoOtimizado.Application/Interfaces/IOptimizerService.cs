namespace DespachoOtimizado.Application
{
    public interface IOptimizerService
    {
        public Task<OptimizerResponseDTO> SendRequest(byte[] content, string fileName, string contentType);
    }
}