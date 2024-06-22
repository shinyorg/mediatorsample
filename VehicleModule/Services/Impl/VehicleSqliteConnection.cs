using SQLite;

namespace VehicleModule.Services.Impl;


public class VehicleSqliteConnection : SQLiteAsyncConnection
{
    public VehicleSqliteConnection(IPlatform platform, ILogger<VehicleSqliteConnection> logger) : base(Path.Combine(platform.AppData.FullName, "animal.db"), true)
    {
        var c = this.GetConnection();
        c.CreateTable<VehicleModel>();
        
        c.EnableWriteAheadLogging();
#if DEBUG
        c.Trace = true;
        c.Tracer = sql => logger.LogDebug("SQLite Query: " + sql);
#endif
    }

    public AsyncTableQuery<VehicleModel> Vehicles => this.Table<VehicleModel>();
}

public class VehicleModel
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    
    public string Manufacturer { get; set; }
    public string Model { get; set; }
}