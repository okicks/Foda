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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemCreate model)
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

        public ActionResult Details(int id)
        {
            var svc = CreateService();
            var model = svc.GetItemById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ItemEdit model)
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

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateService();
            var model = svc.GetItemById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateService();

            service.DeleteItem(id);

            TempData["SaveResult"] = "Your Item was deleted";

            return RedirectToAction("Index");
        }

        private ItemService CreateService()
        {
            return new ItemService(Guid.Parse(User.Identity.GetUserId()));
        }
    }
}