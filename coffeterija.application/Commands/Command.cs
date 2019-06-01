namespace coffeterija.application.Commands
{
    public interface ICommand<TRequest>
    {
        void Execute(TRequest request);
    }

    public interface ICommand<TRequest, IResult>
    {
        IResult Execute(TRequest request);
    }
}
