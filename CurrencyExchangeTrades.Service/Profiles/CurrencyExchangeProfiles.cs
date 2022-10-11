using AutoMapper;
using CurrencyExchangeTrades.Domain.Entity;
using CurrencyExchangeTrades.Service.Dto;

namespace CurrencyExchangeTrades.Service.Profiles
{
    public class CurrencyExchangeProfiles : Profile
    {
        public CurrencyExchangeProfiles()
        {
            _ = CreateMap<Trade, TradeDto>()
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.From.Symbol))
                .ForMember(dest => dest.FromDefinition, opt => opt.MapFrom(src => src.From.Definition))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.To.Symbol))
                .ForMember(dest => dest.ToDefinition, opt => opt.MapFrom(src => src.To.Definition))
                .ForMember(dest => dest.TradeType, opt => opt.MapFrom(src => src.Type.ToString()));

            _ = CreateMap<TradeInputDto, TradeDto>()
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.FromSymbol))
                .ForMember(dest => dest.FromId, opt => opt.MapFrom(src => src.From))
                .ForMember(dest => dest.FromDefinition, opt => opt.Ignore())
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.ToSymbol))
                .ForMember(dest => dest.ToId, opt => opt.MapFrom(src => src.To))
                .ForMember(dest => dest.ToDefinition, opt => opt.Ignore());


            _ = CreateMap<TradeDto, Trade>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TradeType))
                .ForMember(dest => dest.From, opt => opt.Ignore())
                .ForMember(dest => dest.To, opt => opt.Ignore())
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(stc=>DateTime.Now));

            _ = CreateMap<CurrencySymbol, CurrencySymbolDto>();
            _ = CreateMap<CurrencySymbolDto, CurrencySymbol>();

        }
    }
}
