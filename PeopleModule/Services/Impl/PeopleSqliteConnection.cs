using SQLite;

namespace PeopleModule.Services.Impl;


public class PeopleSqliteConnection : SQLiteAsyncConnection
{
    public PeopleSqliteConnection(ILogger<PeopleSqliteConnection> logger) : base(Path.Combine(FileSystem.AppDataDirectory, "people.db"), true)
    {
        var c = this.GetConnection();
        c.CreateTable<PersonModel>();
        
        c.EnableWriteAheadLogging();
#if DEBUG
        c.Trace = true;
        c.Tracer = sql => logger.LogDebug("SQLite Query: " + sql);
#endif
    }

    public AsyncTableQuery<PersonModel> People => this.Table<PersonModel>();
}

public class PersonModel
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
}