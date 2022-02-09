using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetMicroservice.Models;
using TweetMicroservice.Repositories;

namespace TweetMicroservice.Services
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;

        public TweetService(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }
        public void AddTweet(Tweet tweet)
        {
            _tweetRepository.AddTweet(tweet);
        }

        public Tweet DeleteTweet(string id)
        {
            return _tweetRepository.DeleteTweet(id);
        }

        public List<Tweet> GetAllTweets()
        {
            return _tweetRepository.GetAllTweets();
        }
        public List<Tweet> GetTweetsByUserId(string id)
        {
            return _tweetRepository.GetTweetsByUserId(id);
        }
        public Tweet GetTweetById(string id)
        {
            return _tweetRepository.GetTweetById(id);
        }

        public Tweet UpdateTweet(string id, Tweet tweet)
        {
            return _tweetRepository.UpdateTweet(id, tweet);
        }
        public Tweet AddReplyTweet(string id, Tweet reply)
        {
            return _tweetRepository.AddReplyTweet(id, reply);
        }
        public bool LikeTweet(string id, string userId)
        {
           return  _tweetRepository.LikeTweet(id, userId);
        }
    }
}
