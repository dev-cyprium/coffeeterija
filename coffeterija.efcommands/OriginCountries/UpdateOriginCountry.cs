using System;
using System.Linq;
using coffeterija.application.Commands.OriginCountries;
using coffeterija.application.Requests;
using coffeterija.dataaccess;

namespace coffeterija.efcommands.OriginCountries
{
    public class UpdateOriginCountry : CofeterijaBase, IUpdateOriginCountry
    {
        public UpdateOriginCountry(CoffeeContext context) : base(context)
        {
        }

        public void Execute(UpdateOriginCountryDTO request)
        {
            var originCountry = CoffeeContext
                .OriginCountries
                .FirstOrDefault(oc => oc.Id == request.Id);

            if(originCountry == null)
            {
                throw new EntryPointNotFoundException();
            }

            originCountry = Mapper.Map(request, originCountry);
            CoffeeContext.SaveChanges();
        }
    }
}
