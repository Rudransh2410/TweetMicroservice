using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace TweetMicroservice.Models
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Tweet> _tweets;
        public DbClient(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDatabase:Connectionstring"]);
            MongoClientSettings settings = MongoClientSettings.FromUrl(
             new MongoUrl(configuration["MongoDatabase:Connectionstring"])
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase(configuration["MongoDatabase:DatabaseName"]);
            _tweets = database.GetCollection<Tweet>(configuration["MongoDatabase:CollectionName"]);
        }
        public IMongoCollection<Tweet> GetTweetCollection() => _tweets;
    }
}
