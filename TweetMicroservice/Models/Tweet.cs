using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetMicroservice.Models
{
    public class Tweet
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } 
        public string Content { get; set; } 
        public string TweetDate  { get; set; } 
        public Profile User { get; set; }
        public List<Tweet> Replies { get; set; }

        public List<string> Likes { get; set; }

    }
}
