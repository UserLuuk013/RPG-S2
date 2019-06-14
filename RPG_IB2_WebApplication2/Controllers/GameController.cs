using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RPG_IB2;
using RPG_IB2.Datalayer.Interfaces;
using RPG_IB2.Datalayer.MSSQLContexts;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2.Models;
using RPG_IB2_WebApplication2.Converters;
using RPG_IB2_WebApplication2.Datalayer.Interfaces;
using RPG_IB2_WebApplication2.Datalayer.MSSQLContexts;
using RPG_IB2_WebApplication2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Models;

namespace RPG_IB2_WebApplication2.Controllers
{
    public class GameController : Controller
    {
        private readonly PersonageRepository personagerepo = new PersonageRepository(new PersonageMssqlContext());
        private readonly SpelerRepository spelerrepo = new SpelerRepository(new SpelerMssqlContext());
        private readonly CPURepository cpurepo = new CPURepository(new CpuMssqlContext());
        private readonly PersonageViewModelConverter personagecvt = new PersonageViewModelConverter();
        private readonly GameViewModelConverter gamecvt = new GameViewModelConverter();
        private readonly EquipDomein equipDomein;
        public GameController()
        {
            equipDomein = new EquipDomein();
        }
        public IActionResult GameMenu()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            Gevecht gevecht = new Gevecht();
            gevecht.GameGestart = false;
            HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
            Personage personage = personagerepo.GetPersonageBySpelerId(userId);
            PersonageDetailViewModel vm = personagecvt.ViewModelFromPersonage(personage);
            return View(vm);
        }
        public IActionResult NewGame()
        {
            List<Personage> personages = personagerepo.GetStartPersonages();
            PersonageViewModel vm = new PersonageViewModel()
            {
                Personages = new List<PersonageDetailViewModel>()
            };

            foreach (Personage p in personages)
            {
                vm.Personages.Add(personagecvt.ViewModelFromPersonage(p));
            }

            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            spelerrepo.NieuwSpel(userId);
            return View(vm);
        }
        public IActionResult StartGame(int id)
        {
            if (id != 0 && id <= 10)
            {                
                int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
                personagerepo.SelecteerPersonage(id, userId);
                return RedirectToAction("Gamewereld");
            }
            else
            {
                TempData["Error"] = "Ongeldig personage geselecteerd.";
                return RedirectToAction("NewGame");
            }
            
        }
        public IActionResult Gamewereld()
        {
            if (HttpContext.Session.GetString("Gevecht") == null)
            {
                Gevecht gevecht = new Gevecht();
                gevecht.Beloningen = "";
                HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
            }
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            Speler speler = spelerrepo.GetSpelerByID(userId);
            List<Cpu> cpus = cpurepo.GetAllCPUs();
            Game game = equipDomein.VulGame(speler, cpus);
            GameDetailViewModel vm = gamecvt.ViewModelFromGame(game);

            if (JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht")).Beloningen != "")
            {
                Gevecht gevecht = JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht"));
                ViewBag.Beloningen = gevecht.Beloningen;
                gevecht.Beloningen = "";
                HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
            } 
            return View(vm);
        }
    }
}