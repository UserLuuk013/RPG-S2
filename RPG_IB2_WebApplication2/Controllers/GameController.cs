using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RPG_IB2_WebApplication2.Converters;
using RPG_IB2_WebApplication2.Datalayer.Interfaces;
using RPG_IB2_WebApplication2.Datalayer.MSSQLContexts;
using RPG_IB2_WebApplication2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Models;

namespace RPG_IB2_WebApplication2.Controllers
{
    public class GameController : Controller
    {
        IPersonageContext personagecontext;
        PersonageRepository personagerepo;
        PersonageViewModelConverter cvt = new PersonageViewModelConverter();
        public GameController()
        {
            personagecontext = new PersonageMSSQLContext();
            personagerepo = new PersonageRepository(personagecontext);
        }
        public IActionResult Index()
        {
            List<Personage> personages = personagerepo.GetAllPersonages();
            PersonageViewModel vm = new PersonageViewModel()
            {
                Personages = new List<PersonageDetailViewModel>()
            };

            foreach (Personage p in personages)
            {
                vm.Personages.Add(cvt.ViewModelFromPersonage(p));
            }

            return View(vm);
        }
        public IActionResult StartGame(int id)
        {
            personagerepo.SelecteerPersonage(id);
            return RedirectToAction("Gamewereld", "Game");
        }
        public IActionResult Gamewereld()
        {
            return View();
        }
    }
}