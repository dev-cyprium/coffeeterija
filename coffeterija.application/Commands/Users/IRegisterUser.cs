using System;
using coffeterija.application.Requests;

namespace coffeterija.application.Commands.Users
{
    public interface IRegisterUser : ICommand<UserRegisterDTO> {}
}
