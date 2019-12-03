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
        [HttpGet]
        // GET: Customer
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            Customer customerFromDB = db.Customers.SingleOrDefault(c => c.ApplicationId == userId);
            ViewBag.Customer = customerFromDB;
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
                customer.Balance = 0;
                customer.Country = "USA";
                customer.UserName = db.Users.SingleOrDefault(u => u.Id == customer.ApplicationId).UserName.Split('@')[0];
                if (customer.ServiceStartTime != null && DateTime.Today < customer.ServiceStartTime)
                {
                    db.Suspensions.Add(new ServiceSuspension
                    {
                        CustomerId = customer.Id,
                        StartOfSuspension = DateTime.Today,
                        EndOfSuspension = (DateTime)customer.ServiceStartTime

                    });
                    customer.ServiceSuspended = true;
                }
                else
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
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Customers.SingleOrDefault(c => c.ApplicationId == userId));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                Customer customerFromDb = db.Customers.Find(customer.Id);
                customerFromDb.FirstName = customer.FirstName;
                customerFromDb.LastName = customer.LastName;
                customerFromDb.Address = customer.Address;
                customerFromDb.State = customer.State;
                customerFromDb.ZipCode = customer.ZipCode;
                customerFromDb.PickUpDay = customer.PickUpDay;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

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
        [HttpGet]
        public ActionResult SuspendService()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Customers.SingleOrDefault(c => c.ApplicationId == userId));
        }
        [HttpPost]
        public ActionResult SuspendService(Customer customer)
        {
            db.Suspensions.Add(new ServiceSuspension 
            { 
                CustomerId = customer.Id, 
                StartOfSuspension = (DateTime)customer.ServiceEndTime, 
                EndOfSuspension = (DateTime)customer.ServiceStartTime 
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CheckBalance()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Customers.SingleOrDefault(c => c.ApplicationId == userId));
        }
    }
}
