using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(45)]
        public string OrderId { get; set; }

        public int? ErpOrderId { get; set; }

        public int? InvoiceId { get; set; }

        public int? StoreId { get; set; }

        public virtual IEnumerable<BillingEntry> BillingEntries { get; set; }
    }
}
