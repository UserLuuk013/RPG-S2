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
        IShopContext shopcontext;
        ISpelerContext spelercontext;
        IItemContext itemcontext;
        ShopRepository shoprepo;
        SpelerRepository spelerrepo;
        ItemRepository itemrepo;
        EquipDomein equipDomein;
        ShopViewModelConverter shopcvt = new ShopViewModelConverter();
        public ShopController()
        {
            shopcontext = new ShopMSSQLContext();
            spelercontext = new SpelerMSSQLContext();
            itemcontext = new ItemMSSQLContext();
            shoprepo = new ShopRepository(shopcontext);
            spelerrepo = new SpelerRepository(spelercontext);
            itemrepo = new ItemRepository(itemcontext);
            equipDomein = new EquipDomein();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Shop()
        {
            Speler speler = spelerrepo.GetSpeler(1);
            List<Item> playeritems = itemrepo.GetPlayerItemsById(speler.ID);
            List<Item> shopitems = shoprepo.GetShopItems();
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
            Speler speler = spelerrepo.GetSpeler(1);
            Item item = itemrepo.GetItemById(id);
            if (speler.Geld < item.Prijs)
            {
                HttpContext.Session.SetInt32("Geld", 1);
            }
            else
            {
                speler.Geld -= item.Prijs;
                shoprepo.KoopItem(item.ID, item.Type, speler.Geld);
            }
            return RedirectToAction("Shop");
        }
        public IActionResult VerkoopItem(int id)
        {
            Speler speler = spelerrepo.GetSpeler(1);
            Item item = itemrepo.GetItemById(id);
            speler.Geld += item.Prijs;
            shoprepo.VerkoopItem(item.ID, item.Type, speler.Geld);
            return RedirectToAction("Shop");
        }
    }
}