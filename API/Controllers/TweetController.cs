using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class TweetController(IPostsIntegrationService postsService) : BaseApiController
    {
        [HttpPost("message")]
        public async Task<ActionResult<string>> SingleTweet(string message)
        {
            if (string.IsNullOrEmpty(message))
                return BadRequest("Need to insert a message");

            return await postsService.StartSingleTweet(message);
        }
    }
}