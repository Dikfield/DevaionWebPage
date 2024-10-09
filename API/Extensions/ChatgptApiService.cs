using System.Text;
using API.Interfaces;
using Newtonsoft.Json;

namespace API.Services
{
    public class ChatgptApiService(string apiKey)
    {
        private readonly string _apiKey = apiKey;
        private readonly string _apiUrl = "https://api.openai.com/v1/chat/completions"; // Endpoint da OpenAI

        public async Task<string> SingleQuestion(string Question)
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",  // Modelo utilizado
                    messages = new[]
                    {
                        new { role = "system", content = "You are a helpful assistant." },
                        new { role = "user", content = Question }
                    }
                };

                var jsonBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Faz a requisição POST
                var response = await client.PostAsync(_apiUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        // Captura o cabeçalho Retry-After, que informa o tempo de espera
                        if (response.Headers.TryGetValues("Retry-After", out var values))
                        {
                            string retryAfter = values.First();
                            Console.WriteLine($"Você atingiu o limite de requisições. Tente novamente após {retryAfter} segundos.");
                            return string.Empty;
                        }
                        else
                        {
                            Console.WriteLine("Você atingiu o limite de requisições. Tente novamente mais tarde.");
                            return string.Empty;
                        }
                    }
                    Console.WriteLine($"Erro: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
                    return string.Empty;
                }

                // Lê o corpo da resposta
                var responseBody = await response.Content.ReadAsStringAsync();
                var Answer = JsonConvert.DeserializeObject<ChatGPTResponse>(responseBody);

                // Retorna o conteúdo da resposta
                return Answer.choices[0].message.content.Trim();
            }
            catch (Exception ex)
            {
                // Em caso de erro, retorna a mensagem de exceção
                return $"Erro ao obter resposta: {ex.Message}";
            }
        }
    }

    // Classe auxiliar para deserializar a resposta da API
    public class ChatGPTResponse
    {
        public Choice[] choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
