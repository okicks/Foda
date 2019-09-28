using Microsoft.AspNet.Identity;
using Models.Transaction;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class TransactionController : Controller
    {
        public ActionResult Index()
        {
            return View(CreateService().GetTransactions());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateService();

            if (service.CreateTransaction(model))
            {
                TempData["SaveResult"] = "Your Transaction was created.";
                return RedirectToAction("Index");
            };

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateService();
            var model = svc.GetTransactionById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateService();
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
            if (!ModelState.IsValid) return View(model);

            if (model.TransactionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateService();

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
            var svc = CreateService();
            var model = svc.GetTransactionById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateService();

            service.DeleteTransaction(id);

            TempData["SaveResult"] = "Your Transaction was deleted";

            return RedirectToAction("Index");
        }

        private TransactionService CreateService() => new TransactionService(Guid.Parse(User.Identity.GetUserId()));
    }
}