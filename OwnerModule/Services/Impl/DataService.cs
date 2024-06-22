using System.Linq.Expressions;

namespace OwnerModule.Services.Impl;


// sqlite doesn't care about cancellationtokens so they are blanked out - you should have them in your abstraction though
public class DataService(OwnerSqliteConnection conn) : IDataService
{
    public async Task Add(int vehicleId, int personId, CancellationToken _)
    {
        var owner = await conn
            .Owners
            .FirstOrDefaultAsync(x => x.VehicleId == vehicleId && x.PersonId == personId);
        
        if (owner == null)
        {
            await conn.InsertAsync(new OwnerModel
            {
                VehicleId = vehicleId,
                PersonId = personId,
                DateCreated = DateTimeOffset.UtcNow
            });
        }
    }

    public Task Remove(int vehicleId, int personId, CancellationToken _)
        => conn.Owners.DeleteAsync(x => x.VehicleId == vehicleId && x.PersonId == personId);

    public Task<int[]> GetPeopleIdsByVehicleId(int vehicleId, CancellationToken _)
        => this.ToIds(x => x.VehicleId == vehicleId, x => x.PersonId);

    public Task<int[]> GetVehicleIdsByPersonId(int personId, CancellationToken _)
        => this.ToIds(x => x.PersonId == personId, x => x.VehicleId);

    async Task<int[]> ToIds(Expression<Func<OwnerModel, bool>> filter, Func<OwnerModel, int> select)
    {
        var ownerships = await conn
            .Owners
            .Where(filter)
            .ToListAsync();

        return ownerships
            .Select(select)
            .ToArray();
    }

    public Task DeleteByVehicle(int vehicleId, CancellationToken _)
        => conn.Owners.DeleteAsync(x => x.VehicleId == vehicleId);

    public Task DeleteByPerson(int personId, CancellationToken _)
        => conn.Owners.DeleteAsync(x => x.PersonId == personId);
}