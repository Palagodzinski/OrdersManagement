using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OrdersManagement.Application.Interfaces;
using OrdersManagement.Application.Models.Dto;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Infrastructure.Interfaces;
using System.Net.Http.Headers;

namespace OrdersManagement.Application.Services
{
    public class BillingService : IBillingService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBillingRepository _billingRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public BillingService(
            IOrderRepository orderRepository,            
            IBillingRepository billingRepository,
            IOfferRepository offerRepository,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            _orderRepository = orderRepository;
            _billingRepository = billingRepository;
            _offerRepository = offerRepository;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        //public async Task AddBillingEntriesByOrderIdAsync()
        //{
        //    var orders = await _orderRepository.GetOrdersIdsAsync();

        //    foreach (var order in orders)
        //    {
        //        var response = await GetBillingEntriesByOrderIdAsync(order.Value);

        //        if (response is null || response.BillingEntries.IsNullOrEmpty())
        //        {
        //            //continue;
        //            response = BillingEntryFactory();
        //        }

        //        var filteredBillingEntries = FilterOutExistingBillingEntries(response.BillingEntries);

        //        if (!filteredBillingEntries.Any())
        //        {
        //            continue;
        //        }

        //        var billingEntries = _mapper.Map<List<BillingEntry>>(filteredBillingEntries);
        //        billingEntries.ForEach(x => x.OrderId = order.Key);

        //        await _billingRepository.SaveBillingEntriesAsync(billingEntries);
        //    }
        //}

        //public async Task AddBillingEntriesByOfferIdAsync()
        //{
        //    var offers = await _offerRepository.GetOfferIdsAsync();

        //    foreach (var offer in offers)
        //    {
        //        var response = await GetBillingEntriesByOfferIdAsync(offer.Value);

        //        if (response is null || response.BillingEntries.IsNullOrEmpty())
        //        {
        //            continue;
        //        }

        //        var filteredBillingEntries = FilterOutExistingBillingEntries(response.BillingEntries);

        //        if (!filteredBillingEntries.Any())
        //        {
        //            continue;
        //        }

        //        var billingEntries = _mapper.Map<List<BillingEntry>>(filteredBillingEntries);
        //        billingEntries.ForEach(x => x.OfferId = offer.Key);

        //        await _billingRepository.SaveBillingEntriesAsync(billingEntries);
        //    }
        //}
        public async Task AddBillingEntriesByOrderIdAsync()
        {
            var orders = await _orderRepository.GetOrdersIdsAsync();
            await AddBillingEntriesAsync(orders, GetBillingEntriesByOrderIdAsync, (entry, id) => entry.OrderId = id);
        }

        public async Task AddBillingEntriesByOfferIdAsync()
        {
            var offers = await _offerRepository.GetOfferIdsAsync();
            await AddBillingEntriesAsync(offers, GetBillingEntriesByOfferIdAsync, (entry, id) => entry.OfferId = id);
        }

        private async Task AddBillingEntriesAsync(
            IDictionary<int, string> ids,
            Func<string, Task<BillingEntriesResponse?>> getBillingEntriesFunc,
            Action<BillingEntry, int> setIdAction)
        {
            foreach (var id in ids)
            {
                var response = await getBillingEntriesFunc(id.Value);

                if (response is null || response.BillingEntries.IsNullOrEmpty())
                {
                    response = BillingEntryFactory();
                }

                var filteredBillingEntriesList = new List<BillingEntryDto>();
                foreach (var entry in FilterOutExistingBillingEntries(response.BillingEntries))
                {
                    filteredBillingEntriesList.Add(entry);
                }

                if (!filteredBillingEntriesList.Any())
                {
                    continue;
                }

                var billingEntries = _mapper.Map<List<BillingEntry>>(filteredBillingEntriesList);
                billingEntries.ForEach(x => setIdAction(x, id.Key));

                await _billingRepository.SaveBillingEntriesAsync(billingEntries);
            }
        }

        private async Task<BillingEntriesResponse?> GetBillingEntriesByOrderIdAsync(string orderId)
        {
            return await GetBillingEntriesAsync(orderId, "order.id");
        }

        private async Task<BillingEntriesResponse?> GetBillingEntriesByOfferIdAsync(string offerId)
        {
            return await GetBillingEntriesAsync(offerId, "offer.id");
        }

        private BillingEntriesResponse BillingEntryFactory() =>
                    new BillingEntriesResponse
                    {
                        BillingEntries = new List<BillingEntryDto>()
                        {
                            new BillingEntryDto
                            {
                                Id = Guid.NewGuid().ToString(),
                                OccurredAt = DateTime.Now,
                                Type = new BillingTypeDto { Id = "fakeBillingTypeId", Name = "TypeName" },
                                Offer = new OfferDto { Id = "fakeOffer", Name="lolName" },
                                Value = new ValueDto { Amount = 2, Currency="PLN" },
                                Tax = new TaxDto( 2.0m, "someAnnotation"),
                                Balance = new BalanceDto { Amount = 2.0m, Currency = "PLN" },
                                Order = new OrderInfoDto("b8bc0d4f-4ad5-4c04-acca-fc92f0b276d6")
                            }
                        }
                    };

        private async Task<BillingEntriesResponse?> GetBillingEntriesAsync(string id, string idType)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiConfiguration.token);

                var response = await httpClient.GetAsync($"{ApiConfiguration.baseUrl}/billing/billing-entries?{idType}={id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error occured while requesting for billing entries for {idType}={id} - StatusCode={response.StatusCode} Reason={response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var billingEntriesResponse = JsonConvert.DeserializeObject<BillingEntriesResponse>(content);

                return billingEntriesResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //private async Task<BillingEntriesResponse?> GetBillingEntriesByOrderIdAsync(string orderId)
        //{
        //    try
        //    {
        //        var httpClient = _httpClientFactory.CreateClient();
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiConfiguration.token);

        //        var response = await httpClient.GetAsync($"{ApiConfiguration.baseUrl}/billing/billing-entries?order.id={orderId}");

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            throw new Exception($"Error occured while requesting for billing entries for orderId={orderId} - StatusCode={response.StatusCode} Reason={response.ReasonPhrase}");
        //        }

        //        var content = await response.Content.ReadAsStringAsync();
        //        var billingEntriesResponse = JsonConvert.DeserializeObject<BillingEntriesResponse>(content);

        //        return billingEntriesResponse;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private async Task<BillingEntriesResponse?> GetBillingEntriesByOfferIdAsync(string offerId)
        //{
        //    try
        //    {
        //        var httpClient = _httpClientFactory.CreateClient();

        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiConfiguration.token);

        //        var response = await httpClient.GetAsync($"{ApiConfiguration.baseUrl}/billing/billing-entries?offer.id={offerId}");

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            throw new Exception($"Error occured while requesting for billing entries for offerId={offerId} - StatusCode={response.StatusCode} Reason={response.ReasonPhrase}");
        //        }

        //        var content = await response.Content.ReadAsStringAsync();
        //        var billingEntriesResponse = JsonConvert.DeserializeObject<BillingEntriesResponse>(content);

        //        return billingEntriesResponse;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        private IEnumerable<BillingEntryDto> FilterOutExistingBillingEntries(ICollection<BillingEntryDto> billingEntries)
        {
            foreach (var entry in billingEntries)
            {
                var entryExists = _billingRepository.BillingEntryExists(entry.Id);

                if (!entryExists)
                {
                    yield return entry;
                }
            }
        }
    }
}
