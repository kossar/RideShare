using DAL.Base.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.EF
{
    public class AppDbContext : BaseDbContext<User, Role, UserRole, IdentityUserClaim<Guid>,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {

    }
}