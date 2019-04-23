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
        public ShopController()
        {
            shopcontext = new ShopMSSQLContext();
            spelercontext = new SpelerMSSQLContext();
            itemcontext = new ItemMSSQLContext();
            shoprepo = new ShopRepository(shopcontext);
            spelerrepo = new SpelerRepository(spelercontext);
            itemrepo = new ItemRepository(itemcontext);

        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult KoopItem(int id)
        {
            Speler speler = spelerrepo.GetSpeler(1);
            Item item = itemrepo.GetItemById(id);
            speler.Geld -= item.Prijs;
            shoprepo.KoopItem(item.ID, item.Type, speler.Geld);
            return RedirectToAction("Shop", "Game");
        }
        public IActionResult VerkoopItem(int id)
        {
            Speler speler = spelerrepo.GetSpeler(1);
            Item item = itemrepo.GetItemById(id);
            speler.Geld += item.Prijs;
            shoprepo.VerkoopItem(item.ID, item.Type, speler.Geld);
            return RedirectToAction("Shop", "Game");
        }
    }
}