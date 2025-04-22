using DragonBall.Domain.Interfaces.ExternalServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonBall.Domain.Models
{
    public class DragonBallApiService : IDragonBallApiService
    {
        private readonly HttpClient _httpClient;

        public DragonBallApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Character>> GetCharactersAsync()
        {
            var response = await _httpClient.GetStringAsync("/api/characters?limit=58");
            var result = JsonConvert.DeserializeObject<IResponse>(response, GetJsonSerializerSettings());
            return result.Items.ToList();
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/characters/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        // Handle 404 Not Found
                        throw new Exception($"Character with ID {id} was not found.");
                    }
                    else
                    {
                        // Handle other non-success status codes
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Error fetching character with ID {id}: {response.StatusCode} - {errorContent}");
                    }
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Character>(content, GetJsonSerializerSettings());
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error fetching character with ID {id}: {ex.Message}", ex);
            }
        }

        private static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                // Add any necessary settings here, e.g., converters, formatting, etc.
                Converters = new List<JsonConverter> { new StringEnumConverter() },
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
