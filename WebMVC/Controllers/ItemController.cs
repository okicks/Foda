using Microsoft.AspNet.Identity;
using Models.Item;
using Services;
using System;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class ItemController : Controller
    {
        public ActionResult Index()
        {
            return View(CreateService().GetItems());
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
        public ActionResult Create(ItemCreate model)
        {
            if (IsAdmin())
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var service = CreateService();

                if (service.CreateItem(model))
                {
                    TempData["SaveResult"] = "Your Item was created.";
                    return RedirectToAction("Index");
                };

                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var svc = CreateService();
            var model = svc.GetItemById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (IsAdmin())
            {
                var service = CreateService();
                var detail = service.GetItemById(id);
                var model =
                    new ItemEdit
                    {
                        ItemId = detail.ItemId,
                        StoreId = detail.StoreId,
                        Store = detail.Store,
                        ItemName = detail.ItemName,
                        Description = detail.Description,
                        OwnerId = detail.OwnerId
                    };
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ItemEdit model)
        {
            if (IsAdmin())
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.ItemId != id)
                {
                    ModelState.AddModelError("", "Id Mismatch");
                    return View(model);
                }

                var service = CreateService();

                if (service.UpdateItem(model))
                {
                    TempData["SaveResult"] = "Your Item was updated.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Your Item could not be updated.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (IsAdmin())
            {
                var svc = CreateService();
                var model = svc.GetItemById(id);

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

                service.DeleteItem(id);

                TempData["SaveResult"] = "Your Item was deleted";

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        private ItemService CreateService()
        {
            return new ItemService(Guid.Parse(User.Identity.GetUserId()));
        }

        private bool IsAdmin()
        {
            return User.IsInRole("Admin");
        }
    }
}