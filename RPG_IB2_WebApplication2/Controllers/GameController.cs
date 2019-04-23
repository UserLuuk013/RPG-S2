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
        IShopContext shopcontext;
        IItemContext itemcontext;
        IKarakterContext karaktercontext;
        PersonageRepository personagerepo;
        SpelerRepository spelerrepo;
        CPURepository cpurepo;
        ShopRepository shoprepo;
        ItemRepository itemrepo;
        KarakterRepository karakterrepo;
        PersonageViewModelConverter personagecvt = new PersonageViewModelConverter();
        SpelerViewModelConverter spelercvt = new SpelerViewModelConverter();
        GameViewModelConverter gamecvt = new GameViewModelConverter();
        GevechtViewModelConverter gevechtcvt = new GevechtViewModelConverter();
        ShopViewModelConverter shopcvt = new ShopViewModelConverter();
        KarakterUpgradeViewModelConverter karakterupgradecvt = new KarakterUpgradeViewModelConverter();
        EquipDomein equipDomein;
        public GameController()
        {
            personagecontext = new PersonageMSSQLContext();
            spelercontext = new SpelerMSSQLContext();
            cpucontext = new CPUMSSQLContext();
            shopcontext = new ShopMSSQLContext();
            itemcontext = new ItemMSSQLContext();
            karaktercontext = new KarakterMSSQLContext();
            personagerepo = new PersonageRepository(personagecontext);
            spelerrepo = new SpelerRepository(spelercontext);
            cpurepo = new CPURepository(cpucontext);
            shoprepo = new ShopRepository(shopcontext);
            itemrepo = new ItemRepository(itemcontext);
            karakterrepo = new KarakterRepository(karaktercontext);
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
            return RedirectToAction("Gevechtwereld", "Gevecht", id);
        }
        public IActionResult Shop()
        {
            Speler speler = spelerrepo.GetSpeler(1);
            List<Item> playeritems = itemrepo.GetPlayerItemsById(speler.ID);
            List<Item> shopitems = shoprepo.GetShopItems();
            Shop shop = equipDomein.VulShop(playeritems, shopitems, speler);
            ShopDetailViewModel vm = shopcvt.ViewModelFromShop(shop);
            return View(vm);
        }
        public IActionResult KarakterUpgrades()
        {
            Speler speler = spelerrepo.GetSpeler(1);
            Karakter spelerkarakter = karakterrepo.GetSpelerKarakter(speler.ID);
            List<Karakter> shopkarakters = karakterrepo.GetAllKarakters(speler.ID);
            KarakterUpgrade karakterupgrade = equipDomein.VulKarakterUpgrade(spelerkarakter, shopkarakters, speler);
            KarakterUpgradeDetailViewModel vm = karakterupgradecvt.ViewModelFromKarakterUpgrade(karakterupgrade);
            return View(vm);
        }
    }
}