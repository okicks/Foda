using Microsoft.AspNet.Identity;
using Models.Item;
using Services;
using System;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class ItemController : Controller
    {
        private int _id;

        public ActionResult Index(int id)
        {
            var service = CreateService();

            if (service == null)
                return RedirectToAction("Login", "Account");

            ViewBag.StoreId = id;
            _id = id;

            return View(service.GetItems(id));
        }

        public ActionResult Create(int id)
        {
            if (IsAdmin())
            {
                var model = new ItemCreate
                {
                    StoreId = id
                };

                return View(model);
            }

            return RedirectToAction("Index", new { id });
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

                if (service == null)
                    return RedirectToAction("Login", "Account");

                if (service.CreateItem(model))
                {
                    TempData["SaveResult"] = "Your Item was created.";
                    return RedirectToAction("Index", new { id = model.StoreId });
                };

                return View(model);
            }

            return RedirectToAction("Index", new { id = model.StoreId });
        }

        public ActionResult Details(int id)
        {
            var service = CreateService();

            if (service == null)
                return RedirectToAction("Login", "Account");

            var model = service.GetItemByIdDetail(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (IsAdmin())
            {
                var service = CreateService();

                if (service == null)
                    return RedirectToAction("Login", "Account");

                var detail = service.GetItemByIdDetail(id);
                var model =
                    new ItemEdit
                    {
                        ItemId = detail.ItemId,
                        StoreId = detail.StoreId,
                        ItemName = detail.ItemName,
                        Description = detail.Description,
                        OwnerId = detail.OwnerId
                    };
                return View(model);
            }

            return RedirectToAction("Index", new { id = _id });
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

                if (service == null)
                    return RedirectToAction("Login", "Account");

                if (service.UpdateItem(model))
                {
                    TempData["SaveResult"] = "Your Item was updated.";
                    return RedirectToAction("Index", new { id = _id });
                }

                ModelState.AddModelError("", "Your Item could not be updated.");
                return View(model);
            }
            
            return RedirectToAction("Index", new { id = _id });
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (IsAdmin())
            {
                var service = CreateService();

                if (service == null)
                    return RedirectToAction("Login", "Account");

                var model = service.GetItemByIdDelete(id);

                return View(model);
            }

            return RedirectToAction("Index", new { id = _id });
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

                service.DeleteItem(id);

                TempData["SaveResult"] = "Your Item was deleted";

                return RedirectToAction("Index", new { id = _id });
            }

            return RedirectToAction("Index", new { id = _id });
        }

        private ItemService CreateService()
        {
            try
            {
                return new ItemService(Guid.Parse(User.Identity.GetUserId()));
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