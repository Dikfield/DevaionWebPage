using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Tweet
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public required string Question { get; set; }
        public string? Answer { get; set; }
        public int Characteres { get; set; }
        public int Hashtags { get; set; }
        public required string Language { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TweetPhoto? Image { get; set; }
    }
}