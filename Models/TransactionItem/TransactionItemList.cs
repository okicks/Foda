using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.TransactionItem
{
    public class TransactionItemList
    {
        public int TransactionItemId { get; set; }
        public int TransactionId { get; set; }
        public virtual Data.Transaction Transaction { get; set; }
        public int ItemId { get; set; }
        public virtual Data.Item Item { get; set; }
        public int Quantity { get; set; }
        public Guid OwnerId { get; set; }
    }
}
