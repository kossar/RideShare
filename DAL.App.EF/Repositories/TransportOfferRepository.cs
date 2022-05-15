using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories;

public class TransportOfferRepository : BaseRepository<TransportOfferDto, TransportOffer, AppDbContext>, ITransportOfferRepository
{
    public TransportOfferRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new TransportOfferMapper(mapper))
    {
    }

    public async Task<IEnumerable<TransportOfferDto>> GetAllWithIncludingsAsync(Guid userId = default, bool noTracking = true)
    {
        var query = CreateQuery(userId, noTracking);

        var resQuery = query.Include(to => to.StartLocation)
            .Include(to => to.DestinationLocation)
            .Include(to => to.Vehicle)
            .Where(t => t.StartAt > DateTime.UtcNow && t.IsAd)
            .OrderByDescending(to => to.CreatedAt)
            .Select(x => Mapper.Map(x));

        var res = await resQuery.ToListAsync();
        return res!;
    }

    public async Task<IEnumerable<TransportOfferDto>> GetByCountAsync(int count)
    {
        var query = CreateQuery();

        var resQuery = query.Include(to => to.StartLocation)
            .Include(to => to.DestinationLocation)
            .Include(to => to.Vehicle)
            .Where(to => to.StartAt > DateTime.UtcNow && to.IsAd)
            .OrderByDescending(to => to.CreatedAt)
            .Take(count)
            .Select(x => Mapper.Map(x));

        var res = await resQuery.ToListAsync();
        return res!;
    }
}