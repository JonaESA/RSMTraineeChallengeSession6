
using DragonBall.Domain.Entities;
using DragonBall.Domain.Interfaces.ExternalServices;
using DragonBall.Domain.Models;
using System.Net.Http.Json;

namespace DragonBall.Infrastructure.ExternalServices
{
    public class DragonBallApi(HttpClient httpClient) : IDragonBallApi
    {
        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            var result = new List<CharacterApiDTO>();
            string nextUrl = "https://dragonball-api.com/api/characters?limit=58";

            while (!string.IsNullOrEmpty(nextUrl))
            {
                var response = await httpClient.GetFromJsonAsync<CharacterApiResponse>(nextUrl);
                result.AddRange(response.Items);
                nextUrl = response.Links?.Next;
            }

            return result
                .Where(c => c.DeletedAt == null)
                .Select(c => new Character
                {
                    Name = c.Name,
                    Ki = c.Ki,
                    Race = c.Race,
                    Gender = c.Gender,
                    Description = c.Description,
                    Affiliation = c.Affiliation,
                }).ToList();
        }

        public async Task<List<Transformation>> GetAllTransformations()
        {
            var response = await httpClient.GetFromJsonAsync<List<TransformationApiDTO>>("https://dragonball-api.com/api/transformations");
            return response
                .Where(t => t.DeletedAt == null)
                .Select(t => new Transformation
                {
                    Name = t.Name,
                    Ki = t.Ki,
                }).ToList();
        }
    }
}
