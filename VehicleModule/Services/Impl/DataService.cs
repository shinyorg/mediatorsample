namespace VehicleModule.Services.Impl;


public class DataService(VehicleSqliteConnection conn) : IDataService
{
    public async Task<VehicleResult> GetById(int vehicleId)
    {
        var obj = await conn.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleId);
        return ToResult(obj);
    }

    public async Task<IReadOnlyList<VehicleResult>> GetAll()
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