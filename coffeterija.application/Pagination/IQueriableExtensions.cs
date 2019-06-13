using System.Linq;

namespace coffeterija.application.Pagination
{
    public static class IQueriableExtensions
    {
        public static PaginateResponse<T> Paginate<T>(this IQueryable<T> query,
            int perPage, int page)
        {
            var result =
               query
                   .Skip((page - 1) * perPage)
                   .Take(perPage);

            return new PaginateResponse<T>()
            {
                Count = query.Count(),
                PerPage = perPage,
                Page = page,
                Results = result.ToList()
            };
        }
    }
}
