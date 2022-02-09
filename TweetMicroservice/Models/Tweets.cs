using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetMicroservice.Models
{
    public class Tweets
    {
        public IEnumerable<Tweet> TweetList { get; set; }
    }
}
