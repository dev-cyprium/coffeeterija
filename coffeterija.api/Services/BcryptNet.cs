using System;

namespace coffeterija.api.Services
{
    public class BcryptNet : IPasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string givenPassword, string actualHash)
        {
            return BCrypt.Net.BCrypt.Verify(givenPassword, actualHash);
        }
    }
}
