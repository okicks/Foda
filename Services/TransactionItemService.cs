using Data;
using Models.TransactionItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionItemService
    {
        private readonly Guid _userId;

        public TransactionItemService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateTransactionItem(TransactionItemCreate model)
        {
            var entity = new TransactionItem()
            {
                TransactionItemId = model.TransactionItemId,
                TransactionId = model.TransactionId,
                Transaction = model.Transaction,
                ItemId = model.ItemId,
                Item = model.Item,
                Quantity = model.Quantity,
                OwnerId = _userId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.TransactionItems.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TransactionItemListItem> GetTransactionItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.TransactionItems.Where(e => e.OwnerId == _userId).Select(
                            model =>
                                new TransactionItemListItem
                                {
                                    TransactionItemId = model.TransactionItemId,
                                    TransactionId = model.TransactionId,
                                    Transaction = model.Transaction,
                                    ItemId = model.ItemId,
                                    Item = model.Item,
                                    Quantity = model.Quantity
                                }
                        );

                return query.ToArray();
            }
        }

        public static IEnumerable<TransactionItemListItem> GetTransactionItemsByTransaction(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.TransactionItems.Where(e => e.TransactionId == id).Select(model => new TransactionItemListItem
                {
                    TransactionItemId = model.TransactionItemId,
                    TransactionId = model.TransactionId,
                    Transaction = model.Transaction,
                    ItemId = model.ItemId,
                    Item = model.Item,
                    Quantity = model.Quantity
                });

                return query.ToArray();
            }
        }

        public TransactionItemDetail GetTransactionItemById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var model =
                    ctx
                        .TransactionItems
                        .Single(e => e.TransactionItemId == id && e.OwnerId == _userId);
                return
                    new TransactionItemDetail
                    {
                        TransactionItemId = model.TransactionItemId,
                        TransactionId = model.TransactionId,
                        Transaction = model.Transaction,
                        ItemId = model.ItemId,
                        Item = model.Item,
                        Quantity = model.Quantity,
                        OwnerId = _userId
                    };
            }
        }

        public bool UpdateTransactionItem(TransactionItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TransactionItems
                        .Single(e => e.TransactionItemId == model.TransactionItemId && e.OwnerId == _userId);

                entity.TransactionItemId = model.TransactionItemId;
                entity.TransactionId = model.TransactionId;
                entity.Transaction = model.Transaction;
                entity.ItemId = model.ItemId;
                entity.Item = model.Item;
                entity.Quantity = model.Quantity;
                entity.OwnerId = _userId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTransactionItem(int TransactionItemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TransactionItems
                        .Single(e => e.TransactionItemId == TransactionItemId && e.OwnerId == _userId);

                ctx.TransactionItems.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public ApplicationDbContext GetDb()
        {
            return new ApplicationDbContext();
        }
    }
}
