using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExamOne.Entity
{
    public class ExamOneMongoDBContext
    {
        private readonly IMongoDatabase _database;
        public ExamOneMongoDBContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<ExamHistory> ExamHistories => _database.GetCollection<ExamHistory>(typeof(ExamHistory).Name);
        public IMongoCollection<Estimate> Estimates => _database.GetCollection<Estimate>(typeof(Estimate).Name);
    }

    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
