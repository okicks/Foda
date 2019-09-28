using Data;
using Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class StoreService
    {
        private readonly Guid _userId;

        public StoreService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateStore(StoreCreate model)
        {
            var entity = new Store()
            {
                StoreId = model.StoreId,
                StoreName = model.StoreName,
                StoreStreet = model.StoreStreet,
                StoreCity = model.StoreCity,
                StoreState = model.StoreState,
                StoreZip = model.StoreZip,
                StorePhoneNumber = model.StorePhoneNumber,
                OwnerId = _userId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Stores.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<StoreListItem> GetStores()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Stores.Where(e => e.OwnerId == _userId).Select(
                            model =>
                                new StoreListItem
                                {
                                    StoreId = model.StoreId,
                                    StoreName = model.StoreName,
                                    StoreStreet = model.StoreStreet,
                                    StoreCity = model.StoreCity,
                                    StoreState = model.StoreState,
                                    StoreZip = model.StoreZip,
                                    StorePhoneNumber = model.StorePhoneNumber
                                }
                        );

                return query.ToArray();
            }
        }

        public StoreDetail GetStoreById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var model =
                    ctx
                        .Stores
                        .Single(e => e.StoreId == id && e.OwnerId == _userId);
                return
                    new StoreDetail
                    {
                        StoreId = model.StoreId,
                        StoreName = model.StoreName,
                        StoreStreet = model.StoreStreet,
                        StoreCity = model.StoreCity,
                        StoreState = model.StoreState,
                        StoreZip = model.StoreZip,
                        StorePhoneNumber = model.StorePhoneNumber
                    };
            }
        }

        public bool UpdateStore(StoreEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Stores
                        .Single(e => e.StoreId == model.StoreId && e.OwnerId == _userId);

                entity.StoreId = model.StoreId;
                entity.StoreName = model.StoreName;
                entity.StoreStreet = model.StoreStreet;
                entity.StoreCity = model.StoreCity;
                entity.StoreState = model.StoreState;
                entity.StoreZip = model.StoreZip;
                entity.StorePhoneNumber = model.StorePhoneNumber;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteStore(int StoreId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Stores
                        .Single(e => e.StoreId == StoreId && e.OwnerId == _userId);

                ctx.Stores.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public ApplicationDbContext GetDb()
        {
            return new ApplicationDbContext();
        }
    }
}
