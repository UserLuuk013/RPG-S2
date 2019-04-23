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
    public class KarakterController : Controller
    {
        IKarakterContext karaktercontext;
        KarakterRepository karakterrepo;
        IItemContext itemcontext;
        ItemRepository itemrepo;
        ISpelerContext spelercontext;
        SpelerRepository spelerrepo;
        public KarakterController()
        {
            karaktercontext = new KarakterMSSQLContext();
            karakterrepo = new KarakterRepository(karaktercontext);
            itemcontext = new ItemMSSQLContext();
            itemrepo = new ItemRepository(itemcontext);
            spelercontext = new SpelerMSSQLContext();
            spelerrepo = new SpelerRepository(spelercontext);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UpgradeKarakter(int id)
        {
            Speler speler = spelerrepo.GetSpeler(1);
            Karakter karakter = karakterrepo.GetKarakterById(id);
            speler.XP -= karakter.Prijs;
            karakterrepo.UpgradeKarakter(id, speler.XP, karakter.HP);
            return RedirectToAction("KarakterUpgrades", "Game");
        }
    }
}