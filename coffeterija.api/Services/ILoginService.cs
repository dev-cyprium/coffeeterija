using System;
namespace coffeterija.api.Services
{
    public interface ILoginService
    {
        User MaybeGetUser();
        void LoginWithId(int id);
    }
}
