using DragonBall.Aplication.Common;
using DragonBall.Aplication.DTOs;

namespace DragonBall.Aplication.Interfaces
{
    public interface IDragonBallService
    {
        Task<IEnumerable<CharacterDTO>> GetAllCharacters();
        Task<CharacterDTO> GetCharacterById(int id);
        Task<IEnumerable<TransformationDTO>> GetAllTransformations();

        Task<Result> SyncCharacters();

        Task<IEnumerable<CharacterDTO>> GetCharactersByNameAsync(string name);
        Task<IEnumerable<CharacterDTO>> GetCharactersByAffiliationAsync(string affiliation);
    }
}
