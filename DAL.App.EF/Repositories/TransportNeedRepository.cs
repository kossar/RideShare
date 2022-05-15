using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories;

public class TransportNeedRepository : BaseRepository<TransportNeedDto, TransportNeed, AppDbContext>, ITransportNeedRepository
{
    public TransportNeedRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new TransportNeedMapper(mapper))
    {
    }

    public async Task<IEnumerable<TransportNeedDto>> GetAllWithIncludingsAsync(Guid userId = default, bool noTracking = true)
    {
        var query = CreateQuery(userId, noTracking);

        var resQuery = query.Include(tn => tn.StartLocation)
            .Include(tn => tn.DestinationLocation)
            .Where(tn => tn.StartAt > DateTime.UtcNow && tn.IsAd)
            .OrderByDescending(tn => tn.CreatedAt)
            .Select(x => Mapper.Map(x));

        var res = await resQuery.ToListAsync();
        return res!;
    }

    public async Task<IEnumerable<TransportNeedDto>> GetByCountAsync(int count)
    {
        var query = CreateQuery();

        var resQuery = query.Include(tn => tn.StartLocation)
            .Include(tn => tn.DestinationLocation)
            .Where(tn => tn.StartAt > DateTime.UtcNow && tn.IsAd)
            .OrderByDescending(tn => tn.CreatedAt)
            .Take(count)
            .Select(x => Mapper.Map(x));

        var res = await resQuery.ToListAsync();
        return res!;
    }
}