using SQLite;

namespace TheApp.Services;

public class MySqliteConnection : SQLiteAsyncConnection
{
    public MySqliteConnection(
        IPlatform platform,
        ILogger<MySqliteConnection> logger
    ) : base(Path.Combine(platform.AppData.FullName, "app.db"))
    {
        var conn = this.GetConnection();
        // conn.CreateTable<YourModel>();

        conn.EnableWriteAheadLogging();
#if DEBUG
        conn.Trace = true;
        conn.Tracer = sql => logger.LogDebug("SQLite Query: " + sql);
#endif
    }


    // public AsyncTableQuery<YourModel> Logs => this.Table<YourModel>();
}