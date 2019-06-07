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
using RPG_IB2_WebApplication2.Models;

namespace RPG_IB2_WebApplication2.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopRepository shoprepo = new ShopRepository(new ShopMssqlContext());
        private readonly SpelerRepository spelerrepo = new SpelerRepository(new SpelerMssqlContext());
        private readonly ItemRepository itemrepo = new ItemRepository(new ItemMssqlContext());
        private readonly EquipDomein equipDomein;
        private readonly ShopViewModelConverter shopcvt = new ShopViewModelConverter();
        public ShopController()
        {
            equipDomein = new EquipDomein();
        }
        public IActionResult Shop()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            Speler speler = spelerrepo.GetSpelerByID(userId);
            List<Item> playeritems = itemrepo.GetPlayerItemsById(speler.ID);
            List<Item> shopitems = shoprepo.GetShopItems(speler.ID);
            Shop shop = equipDomein.VulShop(playeritems, shopitems, speler);
            ShopDetailViewModel vm = shopcvt.ViewModelFromShop(shop);

            if (HttpContext.Session.GetInt32("Geld") == 1)
            {
                ViewBag.Geld = "Je hebt niet genoeg geld om het item te kunnen kopen.";
                HttpContext.Session.SetInt32("Geld", 0);
            }
            return View(vm);
        }
        public IActionResult KoopItem(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            Speler speler = spelerrepo.GetSpelerByID(userId);
            Item item = itemrepo.GetItemById(id);
            if (speler.Geld < item.Prijs)
            {
                HttpContext.Session.SetInt32("Geld", 1);
            }
            else
            {
                speler.Geld -= item.Prijs;
                shoprepo.KoopItem(item.ID, item.Type, speler.Geld, speler.ID);
            }
            return RedirectToAction("Shop");
        }
        public IActionResult VerkoopItem(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));
            Speler speler = spelerrepo.GetSpelerByID(userId);
            Item item = itemrepo.GetItemById(id);
            speler.Geld += item.Prijs;
            shoprepo.VerkoopItem(item.ID, item.Type, speler.Geld, speler.ID);
            return RedirectToAction("Shop");
        }
    }
}