using DAL.App.EF;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace Tests.IntegrationTests.Data;

public class ApplicationData
{
    private readonly AppDbContext _ctx;

    public ApplicationData(AppDbContext ctx)
    {
        this._ctx = ctx;
    }

    public void InitializeData()
    {
        var userId = InitializeUser();
        InitializeLocations(userId);
        InitializeVehicles(userId);
    } 

    private Guid InitializeUser()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "test@user2.ee",
            Email = "test@user2.ee",
            PhoneNumber = "123",
            FirstName = "user",
            LastName = "user",
            IsDeleted = false,
            CreatedAt = DateTime.Now

        };
        _ctx.Users.Add(user);
        var hasher = new PasswordHasher<User>();
        var normalizer = new UpperInvariantLookupNormalizer();
        var hashedPassword = hasher.HashPassword(user, "Test123");
        user.SecurityStamp = Guid.NewGuid().ToString();
        user.PasswordHash = hashedPassword;
        user.NormalizedEmail = normalizer.NormalizeEmail(user.Email);
        user.NormalizedUserName = normalizer.NormalizeName(user.Email);
        _ctx.SaveChanges();
        return user.Id;
    }

    private void InitializeLocations(Guid userId)
    {
        var location1 = new Location
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            CreatedAt = DateTime.Now,
            UserId = userId,
            User = null,
            Country = "Eesti",
            City = "Tallinn",
            Address = "xxx"
        };
        _ctx.Add(location1);

        var location2 = new Location
        {
            Id = Guid.NewGuid(),
            IsDeleted = true,
            CreatedAt = DateTime.Now,
            UserId = userId,
            User = null,
            Country = "Eesti",
            City = "Tallinn",
            Address = "xxx"
        };
        _ctx.Add(location2);

        var location3 = new Location
        {
            Id = TestConstants.TransportNeedStartLocationId,
            IsDeleted = false,
            CreatedAt = DateTime.Now,
            UserId = userId,
            User = null,
            Country = "Eesti",
            City = "need start",
            Address = "xxx"
        };
        _ctx.Add(location3);

        var location4 = new Location
        {
            Id = TestConstants.TransportNeedDestinationLocationId,
            IsDeleted = false,
            CreatedAt = DateTime.Now,
            UserId = userId,
            User = null,
            Country = "Eesti",
            City = "need end",
            Address = "xxx"
        };
        _ctx.Add(location4);

        var location5 = new Location
        {
            Id = TestConstants.TransportOfferStartLocationId,
            IsDeleted = false,
            CreatedAt = DateTime.Now,
            UserId = userId,
            User = null,
            Country = "Eesti",
            City = "Offer start",
            Address = "xxx"
        };
        _ctx.Add(location5);


        var location6 = new Location
        {
            Id = TestConstants.TransportOfferDestinationLocationId,
            IsDeleted = false,
            CreatedAt = DateTime.Now,
            UserId = userId,
            User = null,
            Country = "Eesti",
            City = "Offer end",
            Address = "xxx"
        };
        _ctx.Add(location6);
        _ctx.SaveChanges();
    }

    private void InitializeVehicles(Guid userId)
    {
        _ctx.Vehicle.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            UserId = userId,
            Make = "Audi",
            Model = "A6",
            Number = "777XXX"
        });
        _ctx.Vehicle.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            UserId = userId,
            Make = "Volvo",
            Model = "XC60",
            Number = "777JJJ"
        });

        _ctx.Vehicle.Add(new Vehicle
        {
            Id = TestConstants.TransportOfferVehicleId,
            CreatedAt = DateTime.Now,
            UserId = userId,
            Make = "Volvo",
            Model = "Offer vehicle",
            Number = "777JJJ"
        });

        _ctx.SaveChanges();
    }
}