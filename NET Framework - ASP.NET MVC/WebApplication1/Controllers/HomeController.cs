using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        ObjectCache cache = MemoryCache.Default;
        List<Customer> customers;

        public HomeController() {
            customers = cache["customers"] as List<Customer>;
            if (customers == null)
            {
                customers = new List<Customer>();
            }
        }

        public void SaveCache()
        {
            cache["customers"] = customers;
        }
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Basket()
        {
            BasketViewModel model = new BasketViewModel();

            model.BasketCount = 5;
            model.BasketTotal = "R$100";

            return PartialView(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.MySuperProperty = "That is a text by Rodrigo.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Outdated
        //public ActionResult ViewCustomer(string Name, string Telephone)
        //{
        //    Customer customer = new Customer();

        //    customer.Id = Guid.NewGuid().ToString();
        //    customer.Name = Name;
        //    customer.Telephone = Telephone;

        //    return View(customer);
        //}

        public ActionResult ViewCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }

            return View(customer);
        }

        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            customer.Id = Guid.NewGuid().ToString();
            customers.Add(customer);
            SaveCache();

            return RedirectToAction("CustomerList");
        }

        public ActionResult EditCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }

        }

        [HttpPost]
        public ActionResult EditCustomer(Customer customer, string Id)
        {
            var customerToEdit = customers.FirstOrDefault(c => c.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                customerToEdit.Name = customer.Name;
                customerToEdit.Telephone = customer.Telephone;
                SaveCache();

                return RedirectToAction("CustomerList");
            }
        }

        public ActionResult CustomerList()
        {
            //List<Customer> customers = new List<Customer>();

            //customers.Add(new Customer() { Name = "Helena", Telephone = "0215155" });
            //customers.Add(new Customer() { Name = "Ellenize", Telephone = "111111" });

            return View(customers);

        }

        public ActionResult DeleteCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }

        }

        [HttpPost]
        [ActionName("DeleteCustomer")]
        public ActionResult ConfirmDeleteCustomer(string Id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                customers.Remove(customer);
                return RedirectToAction("CustomerList");
            }

        }
    }
}