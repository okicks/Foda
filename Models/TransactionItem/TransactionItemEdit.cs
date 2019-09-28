using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.TransactionItem
{
    public class TransactionItemEdit
    {
        [Key]
        public int TransactionItemId { get; set; }

        [Required]
        [ForeignKey(nameof(Transaction))]
        public int TransactionId { get; set; }

        [Required]
        public virtual Data.Transaction Transaction { get; set; }

        [Required]
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }

        [Required]
        public virtual Data.Item Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
    }
}
