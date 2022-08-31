using System;

namespace RegymBot.AccountService
{
    public interface ITokenService
    {
        string Authenticate(Guid guid, string email, string role = "");
        string GetHashString(string password);
    }
}