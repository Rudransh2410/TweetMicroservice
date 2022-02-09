using Confluent.Kafka;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetMicroservice.kafka;
using TweetMicroservice.Models;
using TweetMicroservice.Services;

// For more LogInformationrmation on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TweetMicroservice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {

        private readonly ITweetService _tweetService;
        private readonly ILogger<TweetController> _logger;
        private readonly ProducerConfig _producerConfig;
        public TweetController(ITweetService tweetService, ILogger<TweetController> logger, ProducerConfig producerConfig)
        {
            _tweetService = tweetService;
            _logger = logger;
            _producerConfig = producerConfig;
        }
        [HttpGet]
        public IActionResult GetAllTweets()
        {
            try
            {
                _logger.LogInformation("Request to access all tweets");
                List<Tweet> tweets = _tweetService.GetAllTweets();
                if (tweets == null)
                {
                    _logger.LogWarning("No Tweets to show!!");
                    return NotFound("No Tweets Available");
                }
                _logger.LogInformation("Tweets returned successfully");
                return Ok(tweets);
            }
            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(TweetController.GetAllTweets) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(TweetController.GetAllTweets) + " Error Message " + e.Message);
            }
        }
        [Authorize]
        [HttpGet("user")]
        public IActionResult GetTweetsByUserId(string id)
        {
            try
            {
                List<Tweet> tweets = _tweetService.GetTweetsByUserId(id);
                if (tweets == null)
                {
                    _logger.LogWarning("No Tweets for the User " + id);
                    return NotFound("No Tweets Available");
                }
                _logger.LogInformation("Tweets returned successfully for user " + id);
                return Ok(tweets);
            }
            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(TweetController.GetTweetsByUserId) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(TweetController.GetTweetsByUserId) + " Error Message " + e.Message);
            }
        }
        // GET api/<TweetController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetTweetById(string id)
        {
            try
            {
                Tweet tweet = _tweetService.GetTweetById(id);
                if (tweet == null)
                {
                    _logger.LogWarning("No Tweet with id " + id);
                    return NotFound("No Tweet Available");
                }
                _logger.LogInformation("Tweets returned successfully of id " + id);

                return Ok(tweet);
            }
            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(TweetController.GetTweetById) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(TweetController.GetTweetById) + " Error Message " + e.Message);
            }
        }

        // POST api/<TweetController>
        [Authorize]
        [HttpPost]
        public IActionResult AddTweet([FromBody] Tweet tweet)
        {
            try
            {
                if (tweet != null)
                {
                    _tweetService.AddTweet(tweet);
                    _logger.LogInformation("Tweet added successfully.");
                    return Ok("Tweet added successfully");
                }
                else
                {
                    _logger.LogWarning("Can't add undefined tweet. ");
                    return NotFound("Can't add undefined tweet");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(TweetController.AddTweet) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(TweetController.AddTweet) + " Error Message " + e.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateTweet(string id ,[FromBody] Tweet tweet)
        {
            try
            {

                Tweet newtweet = _tweetService.UpdateTweet(id, tweet);
                if (newtweet != null)
                {
                    _logger.LogInformation("Tweet with id "+ id+ " updated successfully.");
                    return Ok("Tweet updated successfully");
                }
                else
                {
                    _logger.LogWarning("Cannot Update , tweet with id " + id + " does not exist.");
                    return NotFound("Cannot Update Unavailable tweet");
                }
            }

            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(TweetController.AddReplyTweet) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(TweetController.AddReplyTweet) + " Error Message " + e.Message);
            }
        }
        // PUT api/<TweetController>/5
        [Authorize]
        [HttpPut("reply/{id}")]
        public IActionResult AddReplyTweet(string id, [FromBody] Tweet reply)
        {
            try
            {
                Tweet newtweet = _tweetService.AddReplyTweet(id, reply);
                if (newtweet != null)
                {
                    _logger.LogInformation("Reply added to tweet with id " + id);
                    return Ok("Reply added successfully");
                }
                else
                {
                    _logger.LogWarning("Cannot Reply, tweet with id " + id + " does not exist.");
                    return NotFound("Cannot Reply  Unavailable tweet");
                }
            }

            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(TweetController.AddReplyTweet) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(TweetController.AddReplyTweet) + " Error Message " + e.Message);
            }
     }

     
        // DELETE api/<TweetController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteTweet(string id)
        {

             try
            {
                 Tweet tweet = _tweetService.DeleteTweet(id);
                if (tweet != null)
                {
                    _logger.LogInformation("Deleted tweet with id " + id);
                    return Ok("Deleted tweet successfully");
                }
                else
                {
                    _logger.LogWarning("Cannot delete, tweet with id " + id + " does not exist.");
                    return NotFound("Cannot delete Unavailable tweet");
                }
            }

            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(TweetController.DeleteTweet) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(TweetController.DeleteTweet) + " Error Message " + e.Message);
            }
        }
        [HttpPut("like")]
        public IActionResult LikeTweet(string userId,string tweetId)
        {
            try
            {
                 bool isLiked = _tweetService.LikeTweet(tweetId, userId);

                if (isLiked)
                {
                    _logger.LogInformation("Liked tweet with id " + tweetId);
                    return Ok("Liked tweet successfully");
                }
                _logger.LogWarning("Like tweet failed");
                return BadRequest("Like tweet failed");

            }
            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(TweetController.LikeTweet) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(TweetController.LikeTweet) + " Error Message " + e.Message);
            }

        }
    }
}
