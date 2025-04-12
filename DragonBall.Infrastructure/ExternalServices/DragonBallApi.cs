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
            var response = await _httpClient.GetStringAsync("/api/characters");
            var result = JsonConvert.DeserializeObject<IResponse>(response, GetJsonSerializerSettings());
            return result.Items.ToList();
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"/characters/{id}");
            var result = JsonConvert.DeserializeObject<Character>(response, GetJsonSerializerSettings());
            return result;
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
