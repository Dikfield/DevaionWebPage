using System.Runtime.CompilerServices;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class TweetController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService, IPostsIntegrationService posts) : BaseApiController
    {
        [HttpPost("postTweet")]
        public async Task<ActionResult<string>> SingleTweet(PostTweetDto postTweetDto)
        {
            var user = await userRepository.GetMemberByUsernameAsync(User.GetUsername());

            if (user == null) return BadRequest("Cannot Tweet");

            Tweet tweet = new()
            {
                Question = postTweetDto.Question,
                Language = postTweetDto.Language
            };

            if (postTweetDto.ImageFile != null)
            {
                var result = await photoService.AddPhotoAsync(postTweetDto.ImageFile);

                if (result.Error != null) return BadRequest(result.Error.Message);

                var tweetPhoto = new TweetPhoto
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    PlubicId = result.PublicId
                };

                tweet.Image = tweetPhoto;
            }

            mapper.Map(postTweetDto, tweet);

            tweet.Answer = await posts.StartSingleTweet(tweet);

            user.Tweets.Add(tweet);

            if (await userRepository.SaveAllAsync()) return "$tweeted: " + tweet.Answer;


            return BadRequest("Tweeting");
        }
    }
}