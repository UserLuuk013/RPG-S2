using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        IPersonageContext personagecontext;
        ISpelerContext spelercontext;
        ICPUContext cpucontext;
        PersonageRepository personagerepo;
        SpelerRepository spelerrepo;
        CPURepository cpurepo;
        PersonageViewModelConverter personagecvt = new PersonageViewModelConverter();
        SpelerViewModelConverter spelercvt = new SpelerViewModelConverter();
        GameViewModelConverter gamecvt = new GameViewModelConverter();
        GevechtViewModelConverter gevechtcvt = new GevechtViewModelConverter();
        EquipDomein equipDomein;
        public GameController()
        {
            personagecontext = new PersonageMSSQLContext();
            spelercontext = new SpelerMSSQLContext();
            cpucontext = new CPUMSSQLContext();
            personagerepo = new PersonageRepository(personagecontext);
            spelerrepo = new SpelerRepository(spelercontext);
            cpurepo = new CPURepository(cpucontext);
            equipDomein = new EquipDomein();
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
                vm.Personages.Add(personagecvt.ViewModelFromPersonage(p));
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
            Speler speler = spelerrepo.GetSpeler(1);
            List<CPU> cpus = cpurepo.GetAllCPUs();
            Game game = equipDomein.VulGame(speler, cpus);
            GameDetailViewModel vm = gamecvt.ViewModelFromGame(game);
            return View(vm);
        }
        public IActionResult Gevechtwereld(int id)
        {
            Speler speler = spelerrepo.GetSpeler(1);
            CPU cpu = cpurepo.GetCPUById(id);
            Gevecht gevecht = equipDomein.VulGevecht(speler, cpu);
            gevecht.SpelerAanZet = true;
            GevechtDetailViewModel vm = gevechtcvt.ViewModelFromGevecht(gevecht);
            return View(vm);
        }
        public IActionResult Shop()
        {
            return View();
        }
        public IActionResult KarakterUpgrades()
        {
            return View();
        }
    }
}