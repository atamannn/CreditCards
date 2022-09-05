using Newtonsoft.Json;
using ReqRes.Domain.ListUsers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CreditCards.UITests
{
    public class ReqResClient
    {
        private HttpClient _client;
        public ReqResClient()
        {
            _client = new HttpClient();
        }

        public ListData GetListUsers()
        {
            return ListUsersGetAsync().Result;
        }

        private async Task<ListData> ListUsersGetAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://reqres.in/api/users?page=2"),
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
