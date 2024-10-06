using AutoMapper;
using SeriesPlus.Series;

namespace SeriesPlus;

public class SeriesPlusApplicationAutoMapperProfile : Profile
{
    public SeriesPlusApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Serie, SerieDto>();
        CreateMap<CreateUpdateSerieDto,Serie>();
    }
}
