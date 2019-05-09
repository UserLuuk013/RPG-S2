using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        PersonageRepository personagerepo = new PersonageRepository(new PersonageMSSQLContext());
        SpelerRepository spelerrepo = new SpelerRepository(new SpelerMSSQLContext());
        CPURepository cpurepo = new CPURepository(new CPUMSSQLContext());
        ShopRepository shoprepo = new ShopRepository(new ShopMSSQLContext());
        ItemRepository itemrepo = new ItemRepository(new ItemMSSQLContext());
        PersonageViewModelConverter personagecvt = new PersonageViewModelConverter();
        SpelerViewModelConverter spelercvt = new SpelerViewModelConverter();
        GameViewModelConverter gamecvt = new GameViewModelConverter();
        GevechtViewModelConverter gevechtcvt = new GevechtViewModelConverter();
        ShopViewModelConverter shopcvt = new ShopViewModelConverter();
        PersonageShopViewModelConverter personageshopcvt = new PersonageShopViewModelConverter();
        EquipDomein equipDomein;
        public GameController()
        {
            equipDomein = new EquipDomein();
        }
        public IActionResult Index()
        {
            List<Personage> personages = personagerepo.GetAllStartPersonages();
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
            if (HttpContext.Session.GetInt32("personageId") == null)
            {
                HttpContext.Session.SetInt32("personageId", id);
            }
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            personagerepo.SelecteerPersonage((int)HttpContext.Session.GetInt32("personageId"), userId);
            return RedirectToAction("Gamewereld", "Game");
        }
        public IActionResult Gamewereld()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            Speler speler = spelerrepo.GetSpeler(userId);
            List<CPU> cpus = cpurepo.GetAllCPUs();
            Game game = equipDomein.VulGame(speler, cpus);
            GameDetailViewModel vm = gamecvt.ViewModelFromGame(game);
            return View(vm);
        }
        public IActionResult Gevechtwereld(int id)
        {
            return RedirectToAction("Gevechtwereld", "Gevecht", id);
        }
        public IActionResult PersonageShop()
        {
            return View();
        }
    }
}