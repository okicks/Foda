using Microsoft.AspNet.Identity;
using Models.Store;
using Services;
using System;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class StoreController : Controller
    {
        public ActionResult Index()
        {
            return View(CreateService().GetStores());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateService();

            if (service.CreateStore(model))
            {
                TempData["SaveResult"] = "Your Store was created.";
                return RedirectToAction("Index");
            };

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateService();
            var model = svc.GetStoreById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateService();
            var detail = service.GetStoreById(id);
            var model =
                new StoreEdit
                {
                    StoreId = detail.StoreId,
                    StoreName = detail.StoreName,
                    StoreStreet = detail.StoreStreet,
                    StoreCity = detail.StoreCity,
                    StoreState = detail.StoreState,
                    StoreZip = detail.StoreZip,
                    StorePhoneNumber = detail.StorePhoneNumber,
                    OwnerId = detail.OwnerId
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StoreEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.StoreId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateService();

            if (service.UpdateStore(model))
            {
                TempData["SaveResult"] = "Your Store was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Store could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateService();
            var model = svc.GetStoreById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateService();

            service.DeleteStore(id);

            TempData["SaveResult"] = "Your Store was deleted";

            return RedirectToAction("Index");
        }

        private StoreService CreateService()
        {
            return new StoreService(Guid.Parse(User.Identity.GetUserId()));
        }
    }
}