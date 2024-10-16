
using API.Helpers;
using API.Interfaces;
using API.Entities;
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

        public async Task<string> StartSingleTweet(Tweet tweet)
        {
            try
            {
                var question = FormulateQuestion(tweet.Question, tweet.Characteres, tweet.Hashtags, tweet.Language);

                // Chama a API do ChatGPT para obter a resposta
                var answer = await chatgptApi.SingleQuestion(question);

                // Exibe a resposta
                Console.WriteLine("Resposta do ChatGPT:");

                if (string.IsNullOrEmpty(answer))
                    return string.Empty;
                twitterApi.Tweet(answer);
                return answer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o tweet: {ex.Message}");
                return $"Erro ao processar o tweet: {ex.Message}";
            }
        }

        private string FormulateQuestion(string question, int characteres, int hashs, string language)
        {
            return $"Hello Chat, please answer that question: {question}. I want in the answer the maximum quantity of {characteres} characters, this lenght of chars is very important. Add one hashtag before each of the {hashs} most important and evidence words, the answer in the language {language}.";
        }
    }
}