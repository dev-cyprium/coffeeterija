using AutoMapper;
using coffeterija.application.Requests;
using coffeterija.dataaccess;

namespace coffeterija.efcommands
{
    public abstract class CofeterijaBase
    {
        protected CoffeeContext CoffeeContext { get; private set; }
        protected IMapper Mapper { get; private set; }
    
        protected CofeterijaBase (CoffeeContext context)
        {
            CoffeeContext = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateContinentDTO, Continent>()
                    .ForMember(c => c.Id, cf => cf.Ignore())
                    .ForSourceMember(dto => dto.Id, cf => cf.DoNotValidate());

                cfg.CreateMap<UpdateOriginCountryDTO, OriginCountry>()
                    .ForMember(c => c.Id, cf => cf.Ignore())
                    .ForSourceMember(dto => dto.Id, cf => cf.DoNotValidate());

                cfg.CreateMap<NewContinentDTO, Continent>();
                cfg.CreateMap<NewOriginCountryDTO, OriginCountry>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
