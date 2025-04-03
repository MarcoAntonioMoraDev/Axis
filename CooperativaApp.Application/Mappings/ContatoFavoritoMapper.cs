using AutoMapper;
using CooperativaApp.Application.DTOs;
using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Application.Mappings
{
    public class ContatoFavoritoMapper : Profile
    {
        public ContatoFavoritoMapper()
        {
            CreateMap<ContatoFavoritoDTOResponse, ContatoFavorito>();
            CreateMap<ContatoFavorito, ContatoFavoritoDTOResponse>();
            CreateMap<ContatoFavoritoDTORequest, ContatoFavorito>()
                .ForMember(dest => dest.ChavePix, opt => opt.MapFrom(src => src.ChavePix));
        }
    }
}