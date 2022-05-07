using DAL.Base.EF;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF;

public class AppDbContext : BaseDbContext<User, Role, UserRole, IdentityUserClaim<Guid>,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Transport> Transports { get; set; } = default!;
    public DbSet<TransportNeed> TransportNeed { get; set; } = default!;
    public DbSet<TransportOffer> TransportOffer { get; set; } = default!;
    public DbSet<Location> Location { get; set; } = default!;
    public DbSet<Vehicle> Vehicle { get; set; } = default!;
    public DbSet<Schedule> Schedule { get; set; } = default!;
    public DbSet<QuestionAnswer> QuestionAnswer { get; set; } = default!;
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Transport

        builder.Entity<Transport>()
            .HasOne(t => t.TransportNeed)
            .WithMany(l => l.Transports)
            .HasForeignKey(t => t.TransportNeedId);

        builder.Entity<Transport>()
            .HasOne(t => t.TransportOffer)
            .WithMany(l => l.Transports)
            .HasForeignKey(t => t.TransportOfferId);

        //TransportNeed
        builder.Entity<TransportNeed>()
            .HasOne(t => t.StartLocation)
            .WithMany(l => l.TransportNeedStartLocations)
            .HasForeignKey(t => t.StartLocationId);

        builder.Entity<TransportNeed>()
            .HasOne(t => t.DestinationLocation)
            .WithMany(l => l.TransportNeedDestinationLocations)
            .HasForeignKey(t => t.DestinationLocationId);

        builder.Entity<TransportNeed>()
            .Property(n => n.Price)
            .HasPrecision(9, 2);

        builder.Entity<TransportNeed>()
            .Property(n => n.Description)
            .HasMaxLength(512);

        //TransportOffer
        builder.Entity<TransportOffer>()
            .HasOne(t => t.StartLocation)
            .WithMany(l => l.TransportOfferStartLocations)
            .HasForeignKey(t => t.StartLocationId);

        builder.Entity<TransportOffer>()
            .HasOne(t => t.DestinationLocation)
            .WithMany(l => l.TransportOfferDestinationLocations)
            .HasForeignKey(t => t.DestinationLocationId);

        builder.Entity<TransportOffer>()
            .HasOne(t => t.Vehicle)
            .WithMany(l => l.TransportOffers)
            .HasForeignKey(t => t.VehicleId);

        builder.Entity<TransportOffer>()
            .Property(o => o.Price)
            .HasPrecision(9, 2);

        builder.Entity<TransportOffer>()
            .Property(o => o.Description)
            .HasMaxLength(512);

        //Schedule
        builder.Entity<Schedule>()
            .HasOne(s => s.TransportNeed)
            .WithMany(t => t.Schedules)
            .HasForeignKey(s => s.TransportNeedId);

        builder.Entity<Schedule>()
            .HasOne(s => s.TransportOffer)
            .WithMany(t => t.Schedules)
            .HasForeignKey(s => s.TransportOfferId);

        builder.Entity<Schedule>()
            .HasOne(s => s.Transport)
            .WithMany(t => t.Schedules)
            .HasForeignKey(s => s.TransportId);

        //Vehicle
        builder.Entity<Vehicle>()
            .HasOne(v => v.User)
            .WithMany(u => u.Vehicles)
            .HasForeignKey(v => v.UserId);

        builder.Entity<Vehicle>()
            .Property(v => v.Make)
            .HasMaxLength(128);

        builder.Entity<Vehicle>()
            .Property(v => v.Model)
            .HasMaxLength(128);

        builder.Entity<Vehicle>()
            .Property(v => v.Number)
            .HasMaxLength(32);

        //Location
        builder.Entity<Location>()
            .HasOne(l => l.User)
            .WithMany(u => u.Locations)
            .HasForeignKey(l => l.UserId);

        builder.Entity<Location>()
            .Property(l => l.Country)
            .HasMaxLength(128);

        builder.Entity<Location>()
            .Property(l => l.Province)
            .HasMaxLength(128);

        builder.Entity<Location>()
            .Property(l => l.City)
            .HasMaxLength(128);

        builder.Entity<Location>()
            .Property(l => l.Address)
            .HasMaxLength(256);

        builder.Entity<Location>()
            .Property(l => l.Description)
            .HasMaxLength(512);


        //QuestionAnswer
        builder.Entity<QuestionAnswer>()
            .HasOne(q => q.User)
            .WithMany(u => u.QuestionAnswers)
            .HasForeignKey(q => q.UserId);

        builder.Entity<QuestionAnswer>()
            .Property(l => l.Question)
            .HasMaxLength(512);

        builder.Entity<QuestionAnswer>()
            .Property(l => l.Question)
            .HasMaxLength(2048);
    }
}