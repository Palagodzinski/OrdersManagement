using Newtonsoft.Json;

namespace OrdersManagement.Application.Models.Dto
{
    public class BillingEntryDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("occurredAt")]
        public DateTime OccurredAt { get; set; }

        [JsonProperty("type")]
        public BillingTypeDto Type { get; set; }

        [JsonProperty("offer")]
        public OfferDto Offer { get; set; }

        [JsonProperty("value")]
        public ValueDto Value { get; set; }

        [JsonProperty("tax")]
        public TaxDto Tax { get; set; }

        [JsonProperty("balance")]
        public BalanceDto Balance { get; set; }

        [JsonProperty("order")]
        public OrderInfoDto Order { get; set; }
    }
}
