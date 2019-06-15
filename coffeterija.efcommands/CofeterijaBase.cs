using System;
using AutoMapper;
using coffeterija.application.Requests;
using coffeterija.dataaccess;

namespace coffeterija.efcommands.Continents
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
            });
            Mapper = config.CreateMapper();
        }
    }
}
