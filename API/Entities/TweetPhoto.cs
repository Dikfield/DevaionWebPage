using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class TweetPhoto
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PlubicId { get; set; }
        public int TweetId { get; set; }
        public Tweet Tweet { get; set; } = null!;
    }
}