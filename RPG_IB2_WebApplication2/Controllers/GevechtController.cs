using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RPG_IB2;
using RPG_IB2.Datalayer.MSSQLContexts;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Converters;
using RPG_IB2_WebApplication2.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace RPG_IB2_WebApplication2.Controllers
{
    public class GevechtController : Controller
    {
        private int superAanval = 0;
        private int spelerHP;
        private int cpuHP;
        private bool spelerLevend = true;
        private bool cpuLevend = true;
        private bool potionSpelerGebruikt = false;
        private bool potionCPUGebruikt = false;
        private bool gevechtBeëindigd = false;
        EquipDomein equipDomein;
        GevechtRepository gevechtrepo = new GevechtRepository(new GevechtMSSQLContext());
        SpelerRepository spelerrepo = new SpelerRepository(new SpelerMSSQLContext());
        CPURepository cpurepo = new CPURepository(new CPUMSSQLContext());
        GevechtViewModelConverter gevechtcvt = new GevechtViewModelConverter();
        private GevechtDetailViewModel vm;
        public GevechtController()
        {
            equipDomein = new EquipDomein();

            //int userId = (int)HttpContext.Session.GetInt32("CurrentUserID");
            Speler speler = spelerrepo.GetSpeler(1);
            CPU cpu = cpurepo.GetCPUById(1);
            Gevecht gevecht = equipDomein.VulGevecht(speler, cpu);
            vm = gevechtcvt.ViewModelFromGevecht(gevecht);
            vm.SpelerAanZet = true;

            spelerHP = vm.Speler.HP;
            cpuHP = vm.CPU.HP;
        }
        public IActionResult Gevechtwereld(int id)
        {
            if (HttpContext.Session.GetInt32("spelerHP") == null && HttpContext.Session.GetInt32("HP-CPU") == null && HttpContext.Session.GetString("SpelerAanZet") == null
                && HttpContext.Session.GetString("potionSpelerGebruikt") == null && HttpContext.Session.GetString("potionCPUGebruikt") == null && HttpContext.Session.GetString("spelerLevend") == null
                && HttpContext.Session.GetString("cpuLevend") == null && HttpContext.Session.GetString("cpuId") == null)
            {
                HttpContext.Session.SetInt32("spelerHP", spelerHP);
                HttpContext.Session.SetInt32("cpuHP", cpuHP);
                HttpContext.Session.SetString("SpelerAanZet", Convert.ToString(vm.SpelerAanZet));
                HttpContext.Session.SetString("potionSpelerGebruikt", Convert.ToString(potionSpelerGebruikt));
                HttpContext.Session.SetString("potionCPUGebruikt", Convert.ToString(potionCPUGebruikt));
                HttpContext.Session.SetString("spelerLevend", Convert.ToString(spelerLevend));
                HttpContext.Session.SetString("cpuLevend", Convert.ToString(cpuLevend));
                HttpContext.Session.SetInt32("cpuId", id);
            }
            //vm.CPU = cpurepo.GetCPUById(Convert.ToInt32(HttpContext.Session.GetInt32("cpuId")));
            vm.Speler.HP = Convert.ToInt32(HttpContext.Session.GetInt32("spelerHP"));
            vm.CPU.HP = Convert.ToInt32(HttpContext.Session.GetInt32("cpuHP"));
            vm.SpelerAanZet = Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet"));

            if (HttpContext.Session.GetInt32("superAanval") == 1)
            {
                ViewBag.SuperAanval = "Superaanval is geslaagd!";
            }
            else if (HttpContext.Session.GetInt32("superAanval") == 2)
            {
                ViewBag.SuperAanval = "Superaanval is mislukt!";
            }

            if (HttpContext.Session.GetInt32("superAanval") == 1 || HttpContext.Session.GetInt32("superAanval") == 2)
            {
                HttpContext.Session.SetInt32("superAanval", 0);
            }
            return View(vm);
        }
        public IActionResult VolgendeBeurt()
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet")))
            {
                vm.SpelerAanZet = false;
                HttpContext.Session.SetString("SpelerAanZet", Convert.ToString(vm.SpelerAanZet));
                return RedirectToAction("GevechtKeuzeCPU");
            }
            else
            {
                vm.SpelerAanZet = true;
                HttpContext.Session.SetString("SpelerAanZet", Convert.ToString(vm.SpelerAanZet));
                return RedirectToAction("ControlerenGevecht");
            }
        }
        public IActionResult Aanval()
        {
            spelerHP = Convert.ToInt32(HttpContext.Session.GetInt32("spelerHP"));
            cpuHP = Convert.ToInt32(HttpContext.Session.GetInt32("cpuHP"));
            if (Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet")))
            {
                cpuHP -= vm.Speler.Wapen.HP;
                HttpContext.Session.SetInt32("cpuHP", cpuHP);
                return RedirectToAction("ControlerenGevecht");
            }
            else
            {
                spelerHP -= vm.CPU.Wapen.HP;
                HttpContext.Session.SetInt32("spelerHP", spelerHP);
                return RedirectToAction("VolgendeBeurt");
            }
        }
        public IActionResult Superaanval()
        {
            Random rnd = new Random();
            int rndsuperaanval = rnd.Next(1, 5);
            spelerHP = Convert.ToInt32(HttpContext.Session.GetInt32("spelerHP"));
            cpuHP = Convert.ToInt32(HttpContext.Session.GetInt32("cpuHP"));
            vm.SpelerAanZet = Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet"));
            if (vm.SpelerAanZet)
            {
                if (rndsuperaanval == 1)
                {
                    cpuHP -= vm.Speler.Wapen.HP * 2;
                    HttpContext.Session.SetInt32("superAanval", 1);
                }
                else
                {
                    HttpContext.Session.SetInt32("superAanval", 2);
                }
                HttpContext.Session.SetInt32("cpuHP", cpuHP);
                return RedirectToAction("ControlerenGevecht");
            }
            else
            {
                if (rndsuperaanval == 1)
                {
                    spelerHP -= vm.CPU.Wapen.HP * 2;
                    HttpContext.Session.SetInt32("superAanval", 1);
                }
                else
                {
                    HttpContext.Session.SetInt32("superAanval", 2);
                }
                HttpContext.Session.SetInt32("spelerHP", spelerHP);
                return RedirectToAction("VolgendeBeurt");
            }
        }
        public IActionResult Verdediging()
        {
            spelerHP = Convert.ToInt32(HttpContext.Session.GetInt32("spelerHP"));
            cpuHP = Convert.ToInt32(HttpContext.Session.GetInt32("cpuHP"));
            potionSpelerGebruikt = Convert.ToBoolean(HttpContext.Session.GetString("potionSpelerGebruikt"));
            potionCPUGebruikt = Convert.ToBoolean(HttpContext.Session.GetString("potionCPUGebruikt"));
            vm.SpelerAanZet = Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet"));
            if (vm.SpelerAanZet && potionSpelerGebruikt == false)
            {
                spelerHP += vm.Speler.Potion.HP;
                potionSpelerGebruikt = true;
                HttpContext.Session.SetInt32("spelerHP", spelerHP);
                HttpContext.Session.SetString("potionSpelerGebruikt", Convert.ToString(potionSpelerGebruikt));
                return RedirectToAction("ControlerenGevecht");
            }
            else if (vm.SpelerAanZet == false && potionCPUGebruikt == false)
            {
                cpuHP += vm.CPU.Potion.HP;
                potionCPUGebruikt = true;
                HttpContext.Session.SetInt32("cpuHP", cpuHP);
                HttpContext.Session.SetString("potionCPUGebruikt", Convert.ToString(potionCPUGebruikt));
                return RedirectToAction("VolgendeBeurt");
            }
            return RedirectToAction("ControlerenGevecht");
        }
        public IActionResult ControlerenGevecht()
        {
            spelerHP = Convert.ToInt32(HttpContext.Session.GetInt32("spelerHP"));
            cpuHP = Convert.ToInt32(HttpContext.Session.GetInt32("cpuHP"));
            if (spelerHP <= 0)
            {
                spelerLevend = false;
                HttpContext.Session.SetString("spelerLevend", Convert.ToString(spelerLevend));
                return RedirectToAction("GevechtBeëindigd");
            }
            else if (cpuHP <= 0)
            {
                cpuLevend = false;
                HttpContext.Session.SetString("cpuLevend", Convert.ToString(cpuLevend));
                return RedirectToAction("GevechtBeëindigd");
            }
            else
            {
                return RedirectToAction("Gevechtwereld");
            }
            
        }
        public IActionResult GevechtBeëindigd()
        {
            gevechtBeëindigd = true;
            HttpContext.Session.SetString("gevechtBeëindigd", Convert.ToString(gevechtBeëindigd));
            return RedirectToAction("Beloningen");

        }
        public IActionResult Beloningen()
        {
            spelerLevend = Convert.ToBoolean(HttpContext.Session.GetString("spelerLevend"));
            cpuLevend = Convert.ToBoolean(HttpContext.Session.GetString("cpuLevend"));
            if (spelerLevend == true && cpuLevend == false)
            {
                vm.Speler.Geld += 100;
                vm.Speler.XP += 50;
                gevechtrepo.GevechtBeëindigd(vm.Speler.XP, vm.Speler.Geld);
                ViewBag.Beloningen = "GEWONNEN! Je hebt " + 100 + " geld en " + 50 + " XP verdiend!";
            }
            else if (spelerLevend == false && cpuLevend == true)
            {
                vm.Speler.Geld += 50;
                vm.Speler.XP += 25;
                gevechtrepo.GevechtBeëindigd(vm.Speler.XP, vm.Speler.Geld);
                ViewBag.Beloningen = "VERLOREN! Je hebt " + 50 + " geld en " + 25 + " XP verdiend!";
            }
            return RedirectToAction("Gamewereld", "Game");
        }
        public IActionResult GevechtKeuzeCPU()
        {
            cpuHP = Convert.ToInt32(HttpContext.Session.GetInt32("cpuHP"));
            potionCPUGebruikt = Convert.ToBoolean(HttpContext.Session.GetString("potionCPUGebruikt"));
            if (cpuHP <= 5 && potionCPUGebruikt == false)
            {
                return RedirectToAction("Verdediging");
            }
            else
            {
                return RedirectToAction("AanvalKeuzeCPU");
            }
        }
        public IActionResult AanvalKeuzeCPU()
        {
            cpuHP = Convert.ToInt32(HttpContext.Session.GetInt32("cpuHP"));
            if (cpuHP >= 10)
            {
                return RedirectToAction("Superaanval");
            }
            else
            {
                return RedirectToAction("Aanval");
            }
        }
        public IActionResult UpdateGevecht()
        {
            return View(vm);
        }
    }
}