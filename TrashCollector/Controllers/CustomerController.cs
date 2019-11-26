using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext db;
        public CustomerController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                customer.ApplicationId = User.Identity.GetUserId();
                customer.ServiceSuspended = false;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: Customer/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Customer/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        [HttpGet]
        public ActionResult PickUpChange()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Customers.SingleOrDefault(c => c.ApplicationId == userId));
        }
        [HttpPost]
        public ActionResult PickUpChange(Customer customer)
        {
            db.Customers.Single(m => m.Id == customer.Id).PickUpDay = customer.PickUpDay;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult OneTimeRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OneTimeRequest(PickUp pickUp)
        {
            var userId = User.Identity.GetUserId();
            db.PickUps.Add(new PickUp { CustomerId = db.Customers.SingleOrDefault(c => c.ApplicationId == userId).Id, IsSpecial = true, TimeOfRequest = pickUp.TimeOfRequest});
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
