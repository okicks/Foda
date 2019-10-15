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
            var service = CreateService();

            if (service == null)
                return RedirectToAction("Login", "Account");

            return View(service.GetStores());
        }

        public ActionResult Create()
        {
            if (IsAdmin())
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreCreate model)
        {
            if (IsAdmin())
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var service = CreateService();

                if (service == null)
                    return RedirectToAction("Login", "Account");

                if (service.CreateStore(model))
                {
                    TempData["SaveResult"] = "Your Store was created.";
                    return RedirectToAction("Index");
                };

                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return RedirectToAction("Index", "Item", new { id });
        }

        public ActionResult Edit(int id)
        {
            if (IsAdmin())
            {
                var service = CreateService();

                if (service == null)
                    return RedirectToAction("Login", "Account");

                var detail = service.GetStoreByIdDetail(id);
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

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StoreEdit model)
        {
            if (IsAdmin())
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

                if (service == null)
                    return RedirectToAction("Login", "Account");

                if (service.UpdateStore(model))
                {
                    TempData["SaveResult"] = "Your Store was updated.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Your Store could not be updated.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (IsAdmin())
            {
                var service = CreateService();

                if (service == null)
                    return RedirectToAction("Login", "Account");

                var model = service.GetStoreByIdDelete(id);

                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (IsAdmin())
            {
                var service = CreateService();

                if (service == null)
                    return RedirectToAction("Login", "Account");

                service.DeleteStore(id);

                TempData["SaveResult"] = "Your Store was deleted";

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        private StoreService CreateService()
        {
            try
            {
                return new StoreService(Guid.Parse(User.Identity.GetUserId()));
            }
            catch (System.ArgumentNullException)
            {
                return null;
            }
        }
        private bool IsAdmin()
        {
            return User.IsInRole("Admin");
        }
    }
}