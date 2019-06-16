using System;
using coffeterija.application.Commands.Users;
using coffeterija.application.Requests;
using coffeterija.dataaccess;

namespace coffeterija.efcommands.Users
{
    public class RegisterUser : CofeterijaBase, IRegisterUser
    {
        public RegisterUser(CoffeeContext context) : base(context)
        {
        }

        public void Execute(UserRegisterDTO request)
        {
            User user = Mapper.Map<User>(request);

            CoffeeContext.Users.Add(user);
            CoffeeContext.SaveChanges();
        }
    }
}
