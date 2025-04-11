using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DragonBall.Infrastructure.ExternalServices
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
            var response = await _httpClient.GetAsync("https://dragonball-api.com/api/character");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Character>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://dragonball-api.com/api/character/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Character>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
