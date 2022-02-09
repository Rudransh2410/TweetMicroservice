using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetMicroservice.Models;

namespace TweetMicroservice.Services
{
    public interface ITweetService
    {
         List<Tweet> GetAllTweets();
         List<Tweet> GetTweetsByUserId(string id);
         Tweet GetTweetById(string id);
         void AddTweet(Tweet tweet);
         Tweet DeleteTweet(string id);
         Tweet UpdateTweet(string id, Tweet tweet);
         Tweet AddReplyTweet(string id, Tweet reply);
         bool LikeTweet(string id, string userId);
    }
}
