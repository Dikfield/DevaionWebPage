using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class TwitterAPISettings
    {
        public required string OAuthConsumerKey { get; set; }
        public required string OAuthConsumerSecret { get; set; }
        public required string AccessToken { get; set; }
        public required string AccessTokenSecret { get; set; }
    }
}