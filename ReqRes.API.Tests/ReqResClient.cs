using Newtonsoft.Json;
using ReqRes.Domain.ListUsers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CreditCards.UITests
{
    public class ReqResClient
    {
        private const string BASE_URL = "https://reqres.in/api/";
        private HttpClient _client;
        private Uri _baseUri;

        public ReqResClient()
        {
            _client = new HttpClient();
            _baseUri = new Uri(BASE_URL);
        }

        public ListData GetListUsers()
        {
            return ListUsersGetAsync().Result;
        }

        private async Task<ListData> ListUsersGetAsync()
        {
            var fdf  = new Uri(_baseUri, "users?page=2");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_baseUri, "users?page=2"),
                Headers =
                {
                    { "user-agent", "vscode-restclient" },
                },
            };
            using (var response = await _client.SendAsync(request))
            {

                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ListData>(body);
            }
        }
    }

}
