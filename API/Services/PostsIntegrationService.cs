
using API.Helpers;
using API.Interfaces;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PostsIntegrationService : IPostsIntegrationService
    {
        private readonly ChatgptApiService chatgptApi;
        private readonly TwitterApiService twitterApi;
        private readonly string endPoint = "https://api.twitter.com/2/tweets";

        public PostsIntegrationService(IOptions<TwitterAPISettings> twitterConfig, IOptions<ChatGPTApiSettings> chatGPTConfig)
        {
            chatgptApi = new ChatgptApiService(chatGPTConfig.Value.ApiKey);
            twitterApi = new(twitterConfig.Value.OAuthConsumerKey, twitterConfig.Value.OAuthConsumerSecret, twitterConfig.Value.AccessToken, twitterConfig.Value.AccessTokenSecret, endPoint);
        }

        public async Task<string> StartSingleTweet(string question)
        {

            // Chama a API do ChatGPT para obter a resposta
            var answer = await chatgptApi.SingleQuestion(question);

            // Exibe a resposta
            Console.WriteLine("Resposta do ChatGPT:");

            if (string.IsNullOrEmpty(answer))
                return string.Empty;

            Console.WriteLine(answer);

            string tweet = answer;

            try
            {
                twitterApi.Tweet(tweet);
                return answer;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
    }
}