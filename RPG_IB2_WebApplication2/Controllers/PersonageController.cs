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
        SpelerRepository spelerrepo;
        PersonageRepository personagerepo;
        EquipDomein equipDomein;
        PersonageShopViewModelConverter personageshopcvt;
        PersonageViewModelConverter personagecvt;
        public PersonageController()
        {
            spelerrepo = new SpelerRepository(new SpelerMSSQLContext());
            personagerepo = new PersonageRepository(new PersonageMSSQLContext());
            personageshopcvt = new PersonageShopViewModelConverter();
            personagecvt = new PersonageViewModelConverter();
            equipDomein = new EquipDomein();
        }
        public IActionResult Personage()
        {
            Speler speler = spelerrepo.GetSpeler(1);
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
            Speler speler = spelerrepo.GetSpeler(1);
            Personage personage = personagerepo.GetPersonageById(id);
            if (speler.XP < personage.Prijs)
            {
                HttpContext.Session.SetInt32("XP", 1);
            }
            else
            {
                speler.XP -= personage.Prijs;
                personagerepo.UpgradePersonage(personage.ID, speler.XP);
            }
            return RedirectToAction("Personage");
        }
        public IActionResult PersonageDetail(int id)
        {
            PersonageDetailViewModel vm = personagecvt.ViewModelFromPersonage(personagerepo.GetPersonageById(id));
            return View(vm);
        }
    }
}