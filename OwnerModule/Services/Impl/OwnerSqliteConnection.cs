using SQLite;

namespace OwnerModule.Services.Impl;


public class OwnerSqliteConnection : SQLiteAsyncConnection
{
    public OwnerSqliteConnection(IPlatform platform, ILogger<OwnerSqliteConnection> logger) : base(Path.Combine(platform.AppData.FullName, "owners.db"), true)
    {
        var c = this.GetConnection();
        c.CreateTable<OwnerModel>();
        
        c.EnableWriteAheadLogging();
#if DEBUG
        c.Trace = true;
        c.Tracer = sql => logger.LogDebug("SQLite Query: " + sql);
#endif
    }

    public AsyncTableQuery<OwnerModel> Owners => this.Table<OwnerModel>();
}

public class OwnerModel
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    
    [Indexed(Name = "UK_Owner", Unique = true)]   
    public int VehicleId { get; set; }
    
    [Indexed(Name = "UK_Owner", Unique = true)]
    public int PersonId { get; set; }
    
    public DateTimeOffset DateCreated { get; set; }
}