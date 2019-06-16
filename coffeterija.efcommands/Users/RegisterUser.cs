using System;
using System.Linq;
using coffeterija.application.Commands.Users;
using coffeterija.application.Exceptions;
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
            var sameEmail = CoffeeContext.Users.FirstOrDefault(usr => usr.Email.Equals(request.Email));

            if(sameEmail != null)
            {
                throw new UniqueFieldException(request.Email);
            }

            User user = Mapper.Map<User>(request);

            CoffeeContext.Users.Add(user);
            CoffeeContext.SaveChanges();
        }
    }
}
