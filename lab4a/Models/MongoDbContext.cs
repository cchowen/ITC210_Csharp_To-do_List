using lab4a.Models;
using MongoDB.Driver;

namespace lab4a.Data
{
  public class MongoDbContext
{
    private readonly IAtlasSettings _settings;
    private readonly IMongoDatabase _db;

    public MongoDbContext(IAtlasSettings settings)
    {
        _settings = settings;
        var client = new MongoClient(_settings.ConnectionString);
        _db = client.GetDatabase(_settings.Database);
    }

    public IMongoCollection<Item> Item
    {
        get
        {
            return _db.GetCollection<Item>(_settings.Collection);
        }
    }
}
}