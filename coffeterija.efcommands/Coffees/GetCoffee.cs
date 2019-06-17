using System;
using System.Linq;
using System.Linq.Expressions;
using coffeterija.application.Commands;
using coffeterija.application.Commands.Coffees;
using coffeterija.application.Pagination;
using coffeterija.application.Requests.Coffees;
using coffeterija.application.Responses;
using coffeterija.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace coffeterija.efcommands.Coffees
{
    public class GetCoffee : CofeterijaBase, IGetCoffee
    {
        public GetCoffee(CoffeeContext context) : base(context)
        {
        }

        public PaginateResponse<CoffeeResponse> Execute(CoffeeFilterDTO request)
        {
            var query = CoffeeContext.Coffees.AsQueryable();

            query = query
                .Include(c => c.Prices)
                .Include(c => c.Images)
                .Include(c => c.Country)
                .ThenInclude(cc => cc.Continent);

            if(request.Name != null)
            {
                query = query.Where(c => TE(c.Name, request.Name));
            }

            if(request.LessThen != null)
            {
                query = query.Where(c =>
                    c.Prices
                    .OrderByDescending(p => p.CreatedAt)
                    .First()
                    .Price
                    <= request.LessThen);
            }

            if(request.MoreThen != null)
            {
                query = query.Where(c =>
                    c.Prices
                    .OrderByDescending(p => p.CreatedAt)
                    .First()
                    .Price
                    >= request.MoreThen);
            }

            if(request.ContinentName != null)
            {
                query = query.Where(c => TE(c.Country.Continent.Name, request.ContinentName));
            }

            if(request.CountryName != null)
            {
                query = query.Where(c => TE(c.Country.Name, request.CountryName));
            }

            return query.Select(c => new CoffeeResponse()
            {
               Id = c.Id,
               Name = c.Name,
               ContinentName = c.Country.Continent.Name,
               CountryName = c.Country.Name,
               ImageURL = c.Images.First().ImagePath,
               Price = c.Prices
                    .OrderByDescending(p => p.CreatedAt)
                    .First()
                    .Price
            }).Paginate(request.PerPage, request.Page);
        }

        // Trim Equal
        private bool TE(string str1, string str2)
        {
            return str1.ToLower().Equals(str2.Trim().ToLower());
        }
    }

    
}
