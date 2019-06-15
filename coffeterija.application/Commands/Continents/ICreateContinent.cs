using coffeterija.application.Commands;
using coffeterija.application.Requests;

namespace coffeterija.application
{
    public interface ICreateContinent : ICommand<NewContinentDTO> {}
}
