using DragonBall.Domain.Entities;

namespace DragonBall.Domain.Interfaces.ExternalServices
{
    public interface IDragonBallApi
    {
        Task<IEnumerable<Character>> GetAllCharacters();
        Task<List<Transformation>> GetAllTransformations();
    }
}
