using System;
using System.Linq;
using coffeterija.application.Commands;
using coffeterija.application.Pagination;
using coffeterija.application.Requests;
using coffeterija.application.Responses;
using coffeterija.dataaccess;

namespace coffeterija.efcommands
{
    public class GetContinents : CofeterijaBase, IGetContinents
    {
        public GetContinents(CoffeeContext context) : base(context) {}

        public PaginateResponse<ContinentResponse> Execute(PagedRequest request)
        {
            return CoffeeContext
                .Continents
                .AsQueryable()
                .Select(continent => new ContinentResponse() { Name = continent.Name })
                .Paginate(request.PerPage, request.Page);
        }
    }
}
