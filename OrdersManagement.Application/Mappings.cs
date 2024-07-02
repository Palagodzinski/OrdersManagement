using AutoMapper;
using OrdersManagement.Application.Models.Dto;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Application
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<BillingEntryDto, BillingEntry>()
            .ForMember(x => x.Id, opts => opts.Ignore())
            .ForMember(x => x.BillingEntryId, opts => opts.MapFrom(x => x.Id))
            .ForMember(x => x.OccurredAt, opts => opts.MapFrom(x => x.OccurredAt))
            .ForMember(x => x.BillingTypeId, opts => opts.MapFrom(x => x.Type.Id))
            .ForMember(x => x.BillingTypeName, opts => opts.MapFrom(x => x.Type.Name))
            .ForMember(x => x.OfferId, opts => opts.Ignore())
            .ForMember(x => x.ValueAmount, opts => opts.MapFrom(x => x.Value.Amount))
            .ForMember(x => x.ValueCurrency, opts => opts.MapFrom(x => x.Value.Currency))
            .ForMember(x => x.TaxPercentage, opts => opts.MapFrom(x => x.Tax.Percentage))
            .ForMember(x => x.TaxAnnotation, opts => opts.MapFrom(x => x.Tax.Annotation))
            .ForMember(x => x.BalanceAmount, opts => opts.MapFrom(x => x.Balance.Amount))
            .ForMember(x => x.BalanceCurrency, opts => opts.MapFrom(x => x.Balance.Currency))
            .ForMember(x => x.OrderId, opts => opts.Ignore())
            .ForMember(x => x.Offer, opts => opts.Ignore())
            .ForMember(x => x.Order, opts => opts.Ignore());

            //CreateMap<OfferDto, Offer>()
            //    .ForMember(x => x.Id, opts => opts.Ignore())
            //    .ForMember(x => x.OfferId, opts => opts.MapFrom(x => x.Id))
            //    .ForMember(x => x.Name, opts => opts.MapFrom(x => x.Name));

            //CreateMap<OrderInfoDto, Order>()
            //    .ForMember(x => x.Id, opts => opts.Ignore())
            //    .ForMember(x => x.OrderId, opts => opts.MapFrom(x => x.Id))
            //    .ForMember(x => x.ErpOrderId, opts => opts.Ignore())
            //    .ForMember(x => x.InvoiceId, opts => opts.Ignore())
            //    .ForMember(x => x.StoreId, opts => opts.Ignore());
        }
    }
}
