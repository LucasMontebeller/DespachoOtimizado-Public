using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DespachoOtimizado.Application.Services
{
    public class OptimizerService : IOptimizerService
    {
        // Mockado
        private const string ApiPath = "http://localhost:7071/api/fncHarvestOptmizer";
        public async Task<OptimizerResponseDTO> SendRequest(byte[] content, string fileName, string contentType)
        {
            using (var client = new HttpClient())
            {
                try
                {   
                    client.MaxResponseContentBufferSize = 256000;
                    
                    // Cria os parametros da request
                    
                    // Formato de envio
                    var formData = new MultipartFormDataContent();
                    // Conteudo
                    var byteArrayContent = new ByteArrayContent(content);
                    // Header Content-Type
                    byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                    formData.Add(byteArrayContent, "file", fileName);

                    // Faz a requisição
                    var response = await client.PostAsync(ApiPath, formData);

                    var optimizerResponseDTO = JsonConvert.DeserializeObject<OptimizerResponseDTO>(response.Content.ReadAsStringAsync().Result);
                    optimizerResponseDTO.Success = response.IsSuccessStatusCode;
                    
                    return optimizerResponseDTO;
                }
                catch (Exception)
                {
                    return new OptimizerResponseDTO() { Message = "Request failed." };
                }
            }

        }
    }
}