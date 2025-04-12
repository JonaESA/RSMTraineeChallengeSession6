namespace DragonBall.Domain.Models
{
    public class CharacterApiResponse
    {
        public List<CharacterApiDTO> Items { get; set; }
        public CharacterLinks Links { get; set; }
    }

    public class CharacterLinks
    {
        public string Next { get; set; }
    }

    public class CharacterApiDTO
    {
        public string Name { get; set; }
        public string Ki { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string Affiliation { get; set; }
        public string DeletedAt { get; set; }
    }

    public class TransformationApiDTO
    {
        public string Name { get; set; }
        public string Ki { get; set; }
        public string DeletedAt { get; set; }
    }
}
