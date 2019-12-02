using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeeController : Controller
    {
        ApplicationDbContext db;
        public EmployeeController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Employee
        public ActionResult Index()
        { 
            Day day = db.Days.FirstOrDefault(d => DbFunctions.TruncateTime(d.Date) == DbFunctions.TruncateTime(DateTime.Today));
            if (day == null || !day.HasBeenPopulated)
            {
                PopulateDay();
                if(day == null)
                    db.Days.Add(new Day { Date = DateTime.Today, HasBeenPopulated = true});
                db.SaveChanges();
            }
            var userId = User.Identity.GetUserId();
            Employee employeeFromDB = db.Employees.FirstOrDefault(e => e.ApplicationId == userId);
            List<PickUp> pickUps = db.PickUps.Include("Customer").Where(p => p.Customer.ZipCode == employeeFromDB.ZipCode && p.TimeOfRequest == DateTime.Today).ToList();
            return View(pickUps);
        }
        private void PopulateDay()
        {
            DateTime today = DateTime.Today;
            List<Customer> todaysCustomers = db.Customers.Where(c => c.PickUpDay == today.DayOfWeek.ToString()).ToList();
            foreach(Customer customer in todaysCustomers)
            {
                bool isSuspended = false;
                List<ServiceSuspension> suspensions = db.Suspensions.Where(s => s.CustomerId == customer.Id).ToList();
                foreach(ServiceSuspension suspension in suspensions)
                {
                    if (today >= suspension.StartOfSuspension && today < suspension.EndOfSuspension)
                        isSuspended = true;
                }
                if(!isSuspended)
                    db.PickUps.Add(new PickUp { CustomerId = customer.Id, IsSpecial = false, TimeOfRequest = today });
            }
            db.SaveChanges();
        }
        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                employee.ApplicationId = User.Identity.GetUserId();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Collect(int id)
        {
            var userId = User.Identity.GetUserId();
            Employee employeeFromDB = db.Employees.SingleOrDefault(e => e.ApplicationId == userId);
            PickUp pickUpFromDB = db.PickUps.SingleOrDefault(p => p.Id == id);
            if (pickUpFromDB.EmployeeId == null)
            {
                pickUpFromDB.EmployeeId = employeeFromDB.Id;
                pickUpFromDB.TimeOfPickUp = DateTime.Now;
                if (pickUpFromDB.IsSpecial)
                    db.Customers.Find(pickUpFromDB.CustomerId).Balance += 2.25;
                else
                    db.Customers.Find(pickUpFromDB.CustomerId).Balance += 1.99;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DailyPickUps()
        {
            var userId = User.Identity.GetUserId();
            var zip = db.Employees.SingleOrDefault(e => e.ApplicationId == userId).ZipCode;
            List<Customer> customers = db.Customers.Where(c => c.ZipCode == zip).ToList();
            customers = customers.FindAll(c => c.PickUpDay == DateTime.Today.DayOfWeek.ToString());
            return View(customers);
        }
        [HttpPost]
        public ActionResult DailyPickUps(string Day)
        {
            var userId = User.Identity.GetUserId();
            var zip = db.Employees.SingleOrDefault(e => e.ApplicationId == userId).ZipCode;
            List<Customer> customers = db.Customers.Where(c => c.ZipCode == zip).ToList();
            customers = customers.FindAll(c => c.PickUpDay == Day);
            return View(customers);
        }
    }
}
