using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetMicroservice.Models;

namespace TweetMicroservice.Repositories
{
    public class TweetRepository:ITweetRepository
    {
        private readonly IMongoCollection<Tweet> _tweets;

        public TweetRepository(IDbClient dbClient)
        {
            _tweets = dbClient.GetTweetCollection();
        }

        public Tweet GetTweetById(string id)
        {
            var filter = Builders<Tweet>.Filter.Eq("Id", id);

            Tweet tweet = _tweets.Find(filter).FirstOrDefault();

            if (tweet == null)
            {
                return null;
            }

            return tweet;

        }
        public List<Tweet> GetAllTweets()
        {

            List<Tweet> tweets = _tweets.Find(tweet=>true).ToList();

            if(tweets==null)
            {
                return null;
            }

            return tweets;
        }
        public List<Tweet> GetTweetsByUserId(string id )
        {
            var filter = Builders<Tweet>.Filter.Eq("User.Id", id);

            List<Tweet> tweets = _tweets.Find(filter).ToList();

            if (tweets == null)
            {
                return null;
            }
            return tweets;
        }
        public void AddTweet(Tweet tweet)
        {
            _tweets.InsertOne(tweet);
        }
        public Tweet DeleteTweet(string id)
        {
            Tweet tweet = GetTweetById(id);

            var filter = Builders<Tweet>.Filter.Eq("Id", id);

            if (tweet != null)
            {
                _tweets.DeleteOne(filter);
                
                return tweet;
            }
            return null;
            
        }
        public Tweet UpdateTweet(string id, Tweet tweet)
        {
            Tweet oldTweet = GetTweetById(id);

            if (oldTweet == null)
            {
                return null;
            }
            _tweets.ReplaceOne(t => t.Id == id, tweet);

            return tweet;
        }
        public Tweet AddReplyTweet(string id, Tweet reply)
        {
            Tweet tweet = GetTweetById(id);
            if (tweet == null)
            {
                return null;
            }
            tweet.Replies.Add(reply);
            _tweets.ReplaceOne(t => t.Id == id, tweet);
            return tweet;
        }
        public bool LikeTweet(string id, string userId)
        {
            Tweet tweet = GetTweetById(id);
            var filter = Builders<Tweet>.Filter.Eq("Id", id);

            if (tweet != null)
            {
                if(tweet.Likes.Count == 0)
                {
                    tweet.Likes.Add(userId);
                }

                else
                {
                    string isExist = tweet.Likes.FirstOrDefault(uId => uId == userId);

                    if (isExist != null)
                    {
                        tweet.Likes.Remove(userId);
                    }
                    else
                    {
                        tweet.Likes.Add(userId);
                    }
                }
                 
                _tweets.ReplaceOne(filter, tweet);
                return true;
            }
            return false;
        }
    }
}
