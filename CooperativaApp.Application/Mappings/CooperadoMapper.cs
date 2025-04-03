using AutoMapper;
using CooperativaApp.Application.DTOs;
using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Application.Mappings
{
    public class CooperadoMapper : Profile
    {
        public CooperadoMapper()
        {
            CreateMap<CooperadoDTORequest, Cooperado>();
            CreateMap<Cooperado, CooperadoDTOResponse>();
            CreateMap<CooperadoDTOResponse, Cooperado>();
        }
    }
}