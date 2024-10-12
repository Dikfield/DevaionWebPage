using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class PostTweetDto
    {
        public required string Title { get; set; }
        public required string Question { get; set; }
        public string? Answer { get; set; }
        public int Characteres { get; set; }
        public int Hashtags { get; set; }
        public required string Language { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}