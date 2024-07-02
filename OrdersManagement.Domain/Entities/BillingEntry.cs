
namespace OrdersManagement.Domain.Entities
{
    public class BillingEntry
    {
        public int Id { get; set; }

        public string BillingEntryId { get; set; }

        public DateTime OccurredAt { get; set; }

        public string BillingTypeId { get; set; }

        public string BillingTypeName { get; set; }

        public int? OfferId { get; set; }        

        public decimal ValueAmount { get; set; }

        public string ValueCurrency { get; set; }

        public decimal TaxPercentage { get; set; }

        public string TaxAnnotation { get; set; }

        public decimal BalanceAmount { get; set; }

        public string BalanceCurrency { get; set; }
        
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }

        public virtual Offer? Offer { get; set; }
    }
}
