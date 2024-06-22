using GoComics.Persistence.SQLite.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Persistence.SQLite;

public class ComicStore(ISQLiteConnectionFactory connectionFactory)
    : ComicStoreBase(connectionFactory)
{
    public async Task CreateTableAsync()
    {
        await _connection.CreateTableAsync<Comic>();
    }

    public async ValueTask<int> CreateAsync(Comic comic, ComicAvatar avatar)
    {
        return await _connection.InsertAsync(comic);
    }

    public async ValueTask<Comic?> GetComicAsync(int id)
    {
        return await _connection.Table<Comic>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
