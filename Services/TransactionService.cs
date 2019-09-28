using Data;
using Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService
    {
        private readonly Guid _userId;

        public TransactionService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateTransaction(TransactionCreate model)
        {
            var entity = new Transaction()
            {
                TransactionId = model.TransactionId,
                StoreId = model.StoreId,
                Store = model.Store,
                TransactionDate = model.TransactionDate,
                DeliveryStreet = model.DeliveryStreet,
                DeliveryCity = model.DeliveryCity,
                DeliveryState = model.DeliveryState,
                DeliveryZip = model.DeliveryZip,
                OwnerId = _userId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Transactions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TransactionListItem> GetTransactions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Transactions.Where(e => e.OwnerId == _userId).Select(
                            model =>
                                new TransactionListItem
                                {
                                    TransactionId = model.TransactionId,
                                    StoreId = model.StoreId,
                                    Store = model.Store,
                                    TransactionDate = model.TransactionDate,
                                    DeliveryStreet = model.DeliveryStreet,
                                    DeliveryCity = model.DeliveryCity,
                                    DeliveryState = model.DeliveryState,
                                    DeliveryZip = model.DeliveryZip
                                }
                        );

                return query.ToArray();
            }
        }

        public TransactionDetail GetTransactionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var model =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == id && e.OwnerId == _userId);
                return
                    new TransactionDetail
                    {
                        TransactionId = model.TransactionId,
                        StoreId = model.StoreId,
                        Store = model.Store,
                        TransactionDate = model.TransactionDate,
                        DeliveryStreet = model.DeliveryStreet,
                        DeliveryCity = model.DeliveryCity,
                        DeliveryState = model.DeliveryState,
                        DeliveryZip = model.DeliveryZip
                    };
            }
        }

        public bool UpdateTransaction(TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == model.TransactionId && e.OwnerId == _userId);

                entity.TransactionId = model.TransactionId;
                entity.StoreId = model.StoreId;
                entity.Store = model.Store;
                entity.TransactionDate = model.TransactionDate;
                entity.DeliveryStreet = model.DeliveryStreet;
                entity.DeliveryCity = model.DeliveryCity;
                entity.DeliveryState = model.DeliveryState;
                entity.DeliveryZip = model.DeliveryZip;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTransaction(int TransactionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == TransactionId && e.OwnerId == _userId);

                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public ApplicationDbContext GetDb()
        {
            return new ApplicationDbContext();
        }
    }
}
