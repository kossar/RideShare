using Domain.Base;
using Domain.Base.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class BaseDbContext<TUser, TRole, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken> : BaseDbContext<Guid, TUser, TRole, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken>
        where TUserClaim : IdentityUserClaim<Guid>
        where TUserLogin : IdentityUserLogin<Guid>
        where TRoleClaim : IdentityRoleClaim<Guid>
        where TUserToken : IdentityUserToken<Guid>
        where TUser : BaseUser<TUserRole>
        where TRole : BaseRole<TUserRole>
        where TUserRole : BaseUserRole<TUser, TRole>
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class BaseDbContext<TKey, TUser, TRole, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        where TKey : IEquatable<TKey>
        where TUser : BaseUser<TKey, TUserRole>
        where TRole : BaseRole<TKey, TUserRole>
        where TUserRole : BaseUserRole<TKey, TUser, TRole>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");
            builder.Entity<TRole>().ToTable("Role");
            builder.Entity<TUser>().ToTable("User");
            builder.Entity<TUserRole>().ToTable("UserRole");

            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            // do not allow EF to create multiple FK-s, use existing RoleId and UserId
            builder.Entity<TUserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x!.UserRoles)
                .HasForeignKey(x => x.UserId);

            builder.Entity<TUserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x!.UserRoles)
                .HasForeignKey(x => x.RoleId);

            builder.Entity<TUser>()
                .Property(u => u.FirstName)
                .HasMaxLength(128);

            builder.Entity<TUser>()
                .Property(u => u.LastName)
                .HasMaxLength(128);

            builder.Entity<TUser>()
                .Property(u => u.Email)
                .HasMaxLength(128);

            builder.Entity<TUser>()
                .Property(u => u.PhoneNumber)
                .HasMaxLength(64);

            builder.Entity<TRole>()
                .Property(u => u.DisplayName)
                .HasMaxLength(64);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) =>
            SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var entities = ChangeTracker.Entries();

            foreach (var entity in entities.ToList())
            {
                var created = entity.State == EntityState.Added;
                var deleted = entity.State == EntityState.Deleted;
                var modified = entity.State == EntityState.Modified;

                if ((created || modified || deleted) && entity.Entity is DomainEntity domainEntity)
                {
                    switch (entity.State)
                    {
                        case EntityState.Modified:
                            domainEntity.ModifiedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Added:
                            domainEntity.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Deleted:
                            entity.State = EntityState.Modified;
                            domainEntity.DeletedAt = DateTime.UtcNow;
                            break;
                    }
                }
            }


            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}