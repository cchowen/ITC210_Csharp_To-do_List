using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using lab4a.Models;

namespace lab4a.Data.Dao
{
  public class ItemDao : IItemDao
{
    private readonly MongoDbContext _db;
    public ItemDao(IAtlasSettings settings)
    {
        _db = new MongoDbContext(settings);
    }
    public async Task Create(Item item)
    {
        try { await _db.Item.InsertOneAsync(item); }
        catch { throw; }
    }
    public async Task Delete(string id)
    {
        try
        {
            FilterDefinition<Item> data = Builders<Item>.Filter.Eq("Id", id);
            await _db.Item.DeleteOneAsync(data);
        }
        catch { throw; }
    }
    public async Task<Item> Get(string id)
    {
        try
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", id);
            return await _db.Item.Find(filter).FirstOrDefaultAsync();
        }
        catch { throw; }
    }
    public async Task<IEnumerable<Item>> Read()
    {
        try { return await _db.Item.Find(_ => true).ToListAsync(); }
        catch { throw; }
    }
    public async Task Update(Item item)
    {
        try { await _db.Item.ReplaceOneAsync(filter: g => g.Id == item.Id, replacement: item); }
        catch { throw; }
    }
}

public interface IItemDao
{
    Task Create(Item item);
    Task Delete(string id);
    Task<Item> Get(string id);
    Task<IEnumerable<Item>> Read();
    Task Update(Item item);
}
}