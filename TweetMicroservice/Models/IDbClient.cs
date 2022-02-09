using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetMicroservice.Models
{
    public interface IDbClient
    {
        IMongoCollection<Tweet> GetTweetCollection();
    }
}
