namespace VehicleModule.Services.Impl;


public class DataService(VehicleSqliteConnection conn) : IDataService
{
    public Task Delete(int vehicleId, CancellationToken cancellationToken)
        => conn.Vehicles.DeleteAsync(x => x.Id == vehicleId);

    public async Task<VehicleResult?> GetById(int vehicleId, CancellationToken cancellationToken)
    {
        var obj = await conn.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleId);
        if (obj == null)
            return null;
        
        return ToResult(obj);
    }

    public async Task<IReadOnlyList<VehicleResult>> GetAll(CancellationToken cancellationToken)
    {
        var list = await conn
            .Vehicles
            .OrderBy(x => x.Manufacturer)
            .ThenBy(x => x.Model)
            .ToListAsync();
        
        return list.Select(ToResult).ToList();
    }

    static VehicleResult ToResult(VehicleModel model) => new(model.Id, model.Manufacturer, model.Model);
}