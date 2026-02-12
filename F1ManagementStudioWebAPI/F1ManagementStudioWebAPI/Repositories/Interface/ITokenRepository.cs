using Microsoft.AspNetCore.Identity;

namespace F1ManagementStudioWebAPI.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
