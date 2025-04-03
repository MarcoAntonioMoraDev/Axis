using AutoMapper;
using CooperativaApp.Application.DTOs;
using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Application.Mappings
{
    public class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            CreateMap<UsuarioDTORequest, Usuario>();

            CreateMap<Usuario, UsuarioDTORequest>();
        }
    }
}