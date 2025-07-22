using API_RolesBase_Token.Models;

namespace API_RolesBase_Token.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, IList<string> roles);
    }
}
