using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.viewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _db;
        public CustomerController()
        {
            _db = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
        
        public ActionResult New()          // add new customer
        {
            var membershipTypes = _db.MembershipType.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var customer = _db.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _db.MembershipType.ToList()
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)         // post the form
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel{
                    Customer = customer,
                    MembershipTypes = _db.MembershipType.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _db.Customers.Add(customer);

            else
            {
                var customerInDb = _db.Customers.Single(m => m.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _db.SaveChanges();
            return RedirectToAction("Index","Customer");
        }

        public ViewResult Index()            // get all customers
        {
            if(MemoryCache.Default["Genres"] == null)
            {
                MemoryCache.Default["Genres"] = _db.GenreType.ToList();
            }
            var Genres = MemoryCache.Default["Genres"] as IEnumerable<GenreType>;
            return View();
        }
        
        public ActionResult Details(int id)
        {
            var customer = _db.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        
    }
}