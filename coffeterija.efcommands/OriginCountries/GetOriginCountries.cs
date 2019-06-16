using System;
using System.Linq;
using coffeterija.application.Commands.OriginCountries;
using coffeterija.application.Pagination;
using coffeterija.application.Requests;
using coffeterija.application.Responses;
using coffeterija.dataaccess;
using coffeterija.efcommands.Continents;
using Microsoft.EntityFrameworkCore;

namespace coffeterija.efcommands.OriginCountries
{
    public class GetOriginCountries : CofeterijaBase, IGetOriginCountries
    {
        public GetOriginCountries(CoffeeContext context) : base(context)
        {}

        public PaginateResponse<OriginCountryResponse> Execute(OriginCountryFilterDTO request)
        {
            var query = CoffeeContext.OriginCountries.AsQueryable();

            query = query.Include(oc => oc.Continent);

            if(request.Continent != null)
            {
                query = query.Where(oc => oc.Continent.Name.Contains(request.Continent.Trim()));
            }

            if(request.BiggerThen != null)
            {
                query = query.Where(oc => oc.Area >= request.BiggerThen);
            }

            if(request.SmallerThen != null)
            {
                query = query.Where(oc => oc.Area <= request.SmallerThen);
            }

            if(request.Name != null)
            {
                query = query.Where(oc => oc.Name.ToLower().Contains(request.Name.Trim().ToLower()));
            }

            return query
                .Select(oc => new OriginCountryResponse()
                {
                    Continent = oc.Continent.Name,
                    Name = oc.Name,
                    Id = oc.Id,
                    Area = oc.Area
                })
                .Paginate(request.PerPage, request.Page);
        }
    }
}
