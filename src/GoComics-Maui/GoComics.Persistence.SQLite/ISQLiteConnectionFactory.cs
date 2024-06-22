using SQLite;

namespace GoComics.Persistence.SQLite;

public interface ISQLiteConnectionFactory
{
    SQLiteAsyncConnection GetConnection();
}

public sealed class SQLiteAsyncConnectionFactory
    : ISQLiteConnectionFactory
{
    public SQLiteAsyncConnection GetConnection()
        => new(Constants.DatabasePath, Constants.Flags);
}