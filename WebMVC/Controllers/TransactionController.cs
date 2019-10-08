using Microsoft.AspNet.Identity;
using Models.Transaction;
using Services;
using System;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class TransactionController : Controller
    {
        public ActionResult Index()
        {
            var service = CreateService();

            if (service == null)
                return RedirectToAction("Login", "Account");

            return View(service.GetTransactions());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateService();

            if (service == null)
                return RedirectToAction("Login", "Account");

            if (service.CreateTransaction(model))
            {
                TempData["SaveResult"] = "Your Transaction was created.";
                return RedirectToAction("Index");
            };

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateService();

            if (service == null)
                return RedirectToAction("Login", "Account");

            var model = service.GetTransactionById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateService();

            if (service == null)
                return RedirectToAction("Login", "Account");

            var detail = service.GetTransactionById(id);
            var model =
                new TransactionEdit
                {
                    TransactionId = detail.TransactionId,
                    StoreId = detail.StoreId,
                    Store = detail.Store,
                    TransactionDate = detail.TransactionDate,
                    DeliveryStreet = detail.DeliveryStreet,
                    DeliveryCity = detail.DeliveryCity,
                    DeliveryState = detail.DeliveryState,
                    DeliveryZip = detail.DeliveryZip,
                    OwnerId = detail.OwnerId
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransactionEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.TransactionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateService();

            if (service == null)
                return RedirectToAction("Login", "Account");

            if (service.UpdateTransaction(model))
            {
                TempData["SaveResult"] = "Your Transaction was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Transaction could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateService();

            if (service == null)
                return RedirectToAction("Login", "Account");

            var model = service.GetTransactionById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateService();

            if (service == null)
                return RedirectToAction("Login", "Account");

            service.DeleteTransaction(id);

            TempData["SaveResult"] = "Your Transaction was deleted";

            return RedirectToAction("Index");
        }

        private TransactionService CreateService()
        {
            try
            {
                return new TransactionService(Guid.Parse(User.Identity.GetUserId()));
            }
            catch (System.ArgumentNullException)
            {
                return null;
            }
        }
    }
}