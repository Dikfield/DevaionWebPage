using System.Net;
using API.Interfaces;
using OAuth;

namespace API.Services
{
    public class TwitterApiService(string oAuthConsumerKey, string oAuthConsumerSecret, string accessToken, string accessTokenSecret, string endPoint)
    {
        private readonly string _oAuthConsumerKey = oAuthConsumerKey;
        private readonly string _oAuthConsumerSecret = oAuthConsumerSecret;
        private readonly string _accessToken = accessToken;
        private readonly string _accessTokenSecret = accessTokenSecret;
        private readonly string _endPoint = endPoint;

        public void Tweet(string message)
        {
            try
            {
                // Configura a requisição OAuth
                OAuthRequest client = OAuthRequest.ForProtectedResource("POST", _oAuthConsumerKey, _oAuthConsumerSecret, _accessToken, _accessTokenSecret);
                client.RequestUrl = _endPoint;
                string auth = client.GetAuthorizationHeader();

                // Configura a requisição HTTP
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(client.RequestUrl);
                request.Method = "POST";
                request.ContentType = "application/json";  // Corrigido o uso do Content-Type
                request.Headers.Add("Authorization", auth);

                // Cria o conteúdo da requisição JSON
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string escapedMessage = message.Replace("\n", " ").Replace("\r", "").Replace("\"", "\\\"");
                    string jsonMessage = "{\"text\":\"" + escapedMessage + "\"}";
                    streamWriter.Write(jsonMessage);
                }

                // Obtém a resposta do servidor
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                // Exibe a resposta para fins de debug (opcional)
                Console.WriteLine("Resposta da API: " + responseString);
            }
            catch (WebException ex)
            {
                // Tratamento de erros da API (caso a API retorne 400 ou 500, por exemplo)
                using var stream = ex.Response?.GetResponseStream();
                using var reader = new StreamReader(stream);
                string errorResponse = reader.ReadToEnd();
                Console.WriteLine("Erro na API do Twitter: " + errorResponse);
            }
            catch (Exception ex)
            {
                // Tratamento genérico de exceções
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}