using AutoMapper;
using CooperativaApp.Application.DTOs;
using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Application.Mappings
{
    public class CooperativaMapper : Profile
    {
        public CooperativaMapper()
        {
            CreateMap<CooperativaDTORequest, Cooperativa>();

            CreateMap<Cooperativa, CooperativaDTOResponse>()
                .ForMember(dest => dest.Cooperados, opt => opt.MapFrom(src => src.Cooperados));
        }
    }
}