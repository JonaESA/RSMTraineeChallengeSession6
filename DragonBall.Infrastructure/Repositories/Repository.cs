using DragonBall.Domain.Entities;
using DragonBall.Domain.Interfaces;
using DragonBall.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DragonBall.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await context
                .Characters
                .Include(c => c.Transformations)
                .ToListAsync();
        }

        public async Task<Character> GetCharacterById(int id)
        {
            var character = await context
                .Set<Character>()
                .Include(c => c.Transformations)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (character == null)
            {
                throw new InvalidOperationException($"Character with ID {id} not found.");
            }

            return character;
        }

        public async Task<IEnumerable<Transformation>> GetAllTransformations()
        {
            return await context
                .Transformations
                .ToListAsync();
        }

        // For the POST method that synchronizes characters and transformations
        public async Task<bool> AnyAsync() =>
            await context.Characters.AnyAsync();

        public async Task AddRangeAsync(IEnumerable<Character> characters) =>
            await context.Characters.AddRangeAsync(characters);

        public async Task SaveChangesAsync() =>
            await context.SaveChangesAsync();
        // The End?

        public async Task<IEnumerable<Character>> GetCharactersByNameAsync(string name)
        {
            return await context
                .Characters
                .Include(c => c.Transformations)
                .AsNoTracking()
                .Where(c => EF.Functions.Like(c.Name, $"%{name}%"))
                .ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetCharactersByAffiliationAsync(string affiliation)
        {
            return await context
                .Characters
                .Include(c => c.Transformations)
                .AsNoTracking()
                .Where(c => c.Affiliation == affiliation)
                .ToListAsync();
        }
    }
}
