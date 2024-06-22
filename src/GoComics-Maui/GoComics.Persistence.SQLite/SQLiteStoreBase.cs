using SQLite;

namespace GoComics.Persistence.SQLite
{
    public class ComicStoreBase
    {
        protected readonly SQLiteAsyncConnection _connection;

        public ComicStoreBase(ISQLiteConnectionFactory connectionFactory)
        {
            ArgumentNullException.ThrowIfNull(connectionFactory);

            _connection = connectionFactory.GetConnection();
        }
    }
}