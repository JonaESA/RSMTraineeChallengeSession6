using DragonBall.Domain.Entities;

namespace DragonBall.Domain.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Character>> GetAllCharacters();
        Task<Character> GetCharacterById(int id);
        Task<IEnumerable<Transformation>> GetAllTransformations();

        Task<bool> AnyAsync();
        Task AddRangeAsync(IEnumerable<Character> characters);
        Task SaveChangesAsync();

        Task<IEnumerable<Character>> GetCharactersByNameAsync(string name);
        Task<IEnumerable<Character>> GetCharactersByAffiliationAsync(string affiliation);
    }
}
