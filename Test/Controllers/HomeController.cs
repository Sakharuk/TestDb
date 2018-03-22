using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.DataAccess.Interfaces;
using Test.Models;
using Test.Services;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonService _personService;

        public HomeController(IPersonService personService)
        {
            _personService = personService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPerson()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult GetPeoplePage(int currentPage = 1, int itemsPerPage = 10, string predicate = "Id",
                                            bool reverse = false)
        {
            IEnumerable<PersonModel> peopleList = null;
            var peopleTotal = 0;

            var people = _personService.GetPeople(currentPage, itemsPerPage, predicate, reverse).ToList();
            peopleList = (from p in people
                         select new PersonModel
                         {
                             Id = p.Id,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             Value = p.Value
                         });

            peopleTotal = _personService.GetPeopleCount();

            return Json(new { People = peopleList, PeopleTotal = peopleTotal }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(PersonModel person)
        {
            if (!_personService.IsFirstNameUnique(person.FirstName, person.Id))
            {
                ModelState.AddModelError("First Name", "First name must be unique");
            }
            if (!_personService.IsLastNameUnique(person.LastName, person.Id))
            {
                ModelState.AddModelError("Last Name", "Last name must be unique");
            }
                        
            if (ModelState.IsValid)
            {
                _personService.CreatePerson(person.FirstName, person.LastName, person.Value);
                return Json(new { success = true }); ;
            }

            return Json(new
            {
                success = false,
                errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray()
            });
        }


    }
}