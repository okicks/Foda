using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TransactionItem
    {
        [Key]
        public int TransactionItemId { get; set; }

        [Required]
        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }

        public virtual Transaction Transaction { get; set; }

        [Required]
        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
    }
}
