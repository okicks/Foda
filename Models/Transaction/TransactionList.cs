using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Transaction
{
    public class TransactionList
    {
        public int TransactionId { get; set; }
        public int StoreId { get; set; }
        public virtual Data.Store Store { get; set; }
        public DateTime TransactionDate { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryZip { get; set; }
    }
}
