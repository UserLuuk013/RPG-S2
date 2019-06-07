using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPG_IB2;
using RPG_IB2.Datalayer.MSSQLContexts;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Converters;
using RPG_IB2_WebApplication2.Datalayer.MSSQLContexts;
using RPG_IB2_WebApplication2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Models;

namespace RPG_IB2_WebApplication2.Controllers
{
    public class PersonageController : Controller
    {
        private readonly SpelerRepository spelerrepo = new SpelerRepository(new SpelerMssqlContext());
        private readonly PersonageRepository personagerepo = new PersonageRepository(new PersonageMssqlContext());
        private readonly EquipDomein equipDomein;
        private readonly PersonageShopViewModelConverter personageshopcvt = new PersonageShopViewModelConverter();
        private readonly PersonageViewModelConverter personagecvt = new PersonageViewModelConverter();
        public PersonageController()
        {
            equipDomein = new EquipDomein();
        }
        public IActionResult Personage()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            Speler speler = spelerrepo.GetSpelerByID(userId);
            Personage spelerpersonage = personagerepo.GetPersonageBySpelerId(speler.ID);
            List<Personage> shoppersonages = personagerepo.GetPersonagesBySpelerId(speler.ID);
            PersonageShop personageshop = equipDomein.VulPersonageShop(spelerpersonage, shoppersonages, speler);
            PersonageShopDetailViewModel vm = personageshopcvt.ViewModelFromPersonageShop(personageshop);

            if (HttpContext.Session.GetInt32("XP") == 1)
            {
                ViewBag.XP = "Je hebt niet genoeg XP om het personage te kunnen upgraden.";
                HttpContext.Session.SetInt32("XP", 0);
            }
            return View(vm);
        }
        public IActionResult UpgradePersonage(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            Speler speler = spelerrepo.GetSpelerByID(userId);
            Personage personage = personagerepo.GetPersonageById(id);
            if (speler.XP < personage.Prijs)
            {
                HttpContext.Session.SetInt32("XP", 1);
            }
            else
            {
                speler.XP -= personage.Prijs;
                personagerepo.UpgradePersonage(personage.ID, speler.XP, userId);
            }
            return RedirectToAction("Personage");
        }
        public IActionResult PersonageDetail(int id)
        {
            PersonageDetailViewModel vm = personagecvt.ViewModelFromPersonage(personagerepo.GetPersonageById(id));
            return View(vm);
        }

        [HttpGet]
        public IActionResult GetPersonageByID(int personageID)
        {
            Personage personage = personagerepo.GetPersonageById(personageID);
            PersonageDetailViewModel vm = personagecvt.ViewModelFromPersonage(personage);
            return View("PersonagePartial", vm);
        }
    }
}