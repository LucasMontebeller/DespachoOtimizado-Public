using Microsoft.AspNetCore.Http;

namespace DespachoOtimizado.Infra.Data
{
    public static class FormCollectionExtensions
    {
        public static async Task<byte[]> ToArrayBytes(this IFormCollection formCollection)
        {
            using (var memoryStream = new MemoryStream())
            {
                foreach (var formFille in formCollection.Files)
                {
                    await formFille.CopyToAsync(memoryStream);
                }

                return memoryStream.ToArray();
            }
        }
    }
}