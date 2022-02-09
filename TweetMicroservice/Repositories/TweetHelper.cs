using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetMicroservice.Models;

namespace TweetMicroservice.Repositories
{
    public class TweetHelper
    {
        public static List<Tweet> tweets = new List<Tweet>
        {
           new Tweet { Id="61d16eb7a65a2b95c273e493",Content="Some Content",
               TweetDate=new DateTime(2021,03,21).ToLongDateString(),
               User=new Profile{ Id="61d16eb7a65a2b95c273e456",Image="is",Name="Alex Jacob",Email="123@gmail.com"},
               Replies = new List<Tweet>
               {
                   new Tweet { Id="61d16eb7a65a2b95c273r437",Content="Reply Content",
               TweetDate=new DateTime(2021,03,21).ToLongDateString(),
               User=new Profile{ Id="61d16eb7a65a2b95c273f879",Image="is",Name="Alex Maguire",Email="123@gmail.com"},
               Replies = new List<Tweet>() ,Likes= new List<string>()}
               },
               Likes= new List<string>()
           }
        };
    }
}
