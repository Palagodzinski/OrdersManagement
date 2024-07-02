
using System.ComponentModel.DataAnnotations;

namespace OrdersManagement.Domain.Entities
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        public string OfferId { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<BillingEntry> BillingEntries { get; set; }
    }
}
