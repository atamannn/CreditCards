using Newtonsoft.Json;
namespace ReqRes.Domain.ListUsers
{
    public class ListData
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("data")]
        public List<ListItems> Data { get; set; }

        [JsonProperty("support")]
        public CreditCards.UITests.ListSupport Support { get; set; }

    }

}
