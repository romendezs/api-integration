using DragonBall.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonBall.Domain.Interfaces.ExternalServices
{
    public interface IDragonBallApiService
    {
        Task<List<Character>> GetCharactersAsync();
        Task<Character> GetCharacterByIdAsync(int id);
    }
}
