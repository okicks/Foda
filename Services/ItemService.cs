using Data;
using Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ItemService
    {
        private readonly Guid _userId;

        public ItemService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateItem(ItemCreate model)
        {
            var entity = new Item()
            {
                ItemId = model.ItemId,
                StoreId = model.StoreId,
                Store = model.Store,
                ItemName = model.ItemName,
                Description = model.Description,
                OwnerId = _userId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Items.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ItemListItem> GetItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Items.Select(
                            model =>
                                new ItemListItem
                                {
                                    ItemId = model.ItemId,
                                    StoreId = model.StoreId,
                                    Store = model.Store,
                                    ItemName = model.ItemName,
                                    Description = model.Description
                                }
                        );

                return query.ToArray();
            }
        }

        public ItemDetail GetItemById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var model =
                    ctx
                        .Items
                        .Single(e => e.ItemId == id);
                return
                    new ItemDetail
                    {
                        ItemId = model.ItemId,
                        StoreId = model.StoreId,
                        Store = model.Store,
                        ItemName = model.ItemName,
                        Description = model.Description
                    };
            }
        }

        public bool UpdateItem(ItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Items
                        .Single(e => e.ItemId == model.ItemId);

                entity.ItemId = model.ItemId;
                entity.StoreId = model.StoreId;
                entity.Store = model.Store;
                entity.ItemName = model.ItemName;
                entity.Description = model.Description;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItem(int ItemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Items
                        .Single(e => e.ItemId == ItemId);

                ctx.Items.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public ApplicationDbContext GetDb()
        {
            return new ApplicationDbContext();
        }
    }
}
