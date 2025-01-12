using Newtonsoft.Json;
using SiliconSpace.Service.DTOs.SMS;
using SiliconSpace.Service.Interfaces;
using System.Text;

namespace SiliconSpace.Service.Services
{
    public class SmsService : ISmsService
    {
        private readonly HttpClient _httpClient;

        public SmsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendAsync(MessageForCreationDto dto)
        {
            try
            {
                var apiUrl = "https://notify.eskiz.uz/api/message/sms/send";

                var requestData = new
                {
                    mobile_phone = dto.PhoneNumber,
                    message = dto.MessageContent,
                    from = "4546", // Change to your nickname (as a string)
                };

                var jsonContent = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MDkyOTU1MDcsImlhdCI6MTcwNjcwMzUwNywicm9sZSI6InVzZXIiLCJzaWduIjoiMzUzMmM5ZDkyZjY2MGZiNjI3ODU4NGI5ZWI1OTAzOTUyMzBiYmI1MDE5MjEzYTBhNGQyOWRlMGMzNzU2ZTk5MCIsInN1YiI6IjUyODQifQ.BMMfCK7gg76VdUNQT0xHd7yZjTY4Danen8kkSww18HU");

                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                Console.WriteLine($"Failed to send SMS. Status code: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
