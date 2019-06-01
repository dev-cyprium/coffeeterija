using System;
using coffeterija.application.Requests;

namespace coffeterija.application.Commands
{

    public interface IGetCoffee : ICommand<PagedRequest> {} 
}
