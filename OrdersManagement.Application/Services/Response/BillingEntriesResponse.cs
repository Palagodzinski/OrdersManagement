using Newtonsoft.Json;
using OrdersManagement.Application.Models;
using OrdersManagement.Application.Models.Dto;

namespace OrdersManagement.Application.Services
{
    public class BillingEntriesResponse
    {
        [JsonProperty("billingEntries")]
        public ICollection<BillingEntryDto> BillingEntries { get; set; }
    }
}
