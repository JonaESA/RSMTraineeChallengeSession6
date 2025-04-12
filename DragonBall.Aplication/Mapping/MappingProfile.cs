using AutoMapper;
using DragonBall.Aplication.DTOs;
using DragonBall.Domain.Entities;

namespace DragonBall.Aplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Character, CharacterDTO>();
            CreateMap<Transformation, TransformationDTO>();
        }
    }
}
