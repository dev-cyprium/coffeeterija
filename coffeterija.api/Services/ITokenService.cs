using System;
namespace coffeterija.api.Services
{
    public interface ITokenService<ResourceFromToken, ResourceToToken>
    {
        ResourceFromToken GetFromToken(string token);
        string GenerateToken(ResourceToToken data);
    }
}
