using AutoMapper;
using DragonBall.Aplication.Common;
using DragonBall.Aplication.DTOs;
using DragonBall.Aplication.Interfaces;
using DragonBall.Domain.Interfaces;
using DragonBall.Domain.Interfaces.ExternalServices;

namespace DragonBall.Aplication.Services
{
    public class DragonBallService(IRepository repository,
        IMapper mapper,
        HttpClient httpClient,
        IDragonBallApi dragonBallApi) : IDragonBallService
    {
        public async Task<IEnumerable<CharacterDTO>> GetAllCharacters()
        {
            var characters = await repository.GetAllCharacters();
            return mapper.Map<IEnumerable<CharacterDTO>>(characters);
        }

        public async Task<IEnumerable<TransformationDTO>> GetAllTransformations()
        {
            var transformations = await repository.GetAllTransformations();
            return mapper.Map<IEnumerable<TransformationDTO>>(transformations);
        }

        public async Task<CharacterDTO> GetCharacterById(int id)
        {
            var character = await repository.GetCharacterById(id);

            if (character == null)
            {
                throw new KeyNotFoundException($"Character with ID {id} was not found.");
            }

            return mapper.Map<CharacterDTO>(character);
        }

        public async Task<IEnumerable<CharacterDTO>> GetCharactersByNameAsync(string name)
        {
            var characters = await repository.GetCharactersByNameAsync(name);

            if (!characters.Any())
            {
                throw new KeyNotFoundException($"No characters found with name containing '{name}'.");
            }

            return mapper.Map<IEnumerable<CharacterDTO>>(characters);
        }

        public async Task<IEnumerable<CharacterDTO>> GetCharactersByAffiliationAsync(string affiliation)
        {
            var characters = await repository.GetCharactersByAffiliationAsync(affiliation);

            if (!characters.Any())
            {
                throw new KeyNotFoundException($"No characters found with affiliation '{affiliation}'.");
            }

            return mapper.Map<IEnumerable<CharacterDTO>>(characters);
        }

        // Sync me ٩(◕‿◕｡)۶
        public async Task<Result> SyncCharacters()
        {
            var existingCharacters = await repository.AnyAsync();
            var existingTransformations = await repository.AnyAsync();

            if (existingCharacters || existingTransformations)
                return Result.Failure("Clean up relational tables first.");

            var allCharacters = await dragonBallApi.GetAllCharacters();
            var allTransformations = await dragonBallApi.GetAllTransformations();

            var saiyanCharacters = allCharacters
                .Where(c => c.Race == "Saiyan")
                .ToList();

            var zFighterNames = saiyanCharacters
                .Where(c => c.Affiliation == "Z Fighter")
                .Select(c => c.Name)
                .ToList();

            var filteredTransformations = allTransformations
                .Where(t => zFighterNames.Any(name => t.Name.Contains(name)))
                .ToList();

            // Relacionar transformaciones
            foreach (var character in saiyanCharacters)
            {
                character.Transformations = filteredTransformations
                    .Where(t => t.Name.Contains(character.Name))
                    .ToList();
            }

            await repository.AddRangeAsync(saiyanCharacters);
            await repository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
