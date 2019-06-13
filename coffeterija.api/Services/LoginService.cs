using System;
using System.Linq;
using coffeterija.dataaccess;

namespace coffeterija.api.Services
{
    public class LoginService : ILoginService
    {
        User user;
        readonly CoffeeContext context;

        public LoginService(CoffeeContext context)
        {
            this.context = context;
        }

        public void LoginWithId(int id)
        {
            var _user = context.Users.SingleOrDefault(u => u.Id == id);
            if (_user != null)
                user = _user; 
        }

        public User MaybeGetUser()
        {
            return user;
        }
    }
}
