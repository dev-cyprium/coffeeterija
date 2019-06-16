using System;
namespace coffeterija.api.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool Verify(string givenPassword, string actualHash);
    }
}
