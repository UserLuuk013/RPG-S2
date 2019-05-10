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
using Newtonsoft.Json;
using RPG_IB2_WebApplication2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Datalayer.MSSQLContexts;

namespace RPG_IB2_WebApplication2.Controllers
{
    public class GevechtController : Controller
    {
        private bool spelerLevend = true;
        private bool cpuLevend = true;
        private bool potionSpelerGebruikt = false;
        private bool potionCPUGebruikt = false;
        private bool gevechtBeëindigd = false;
        EquipDomein equipDomein;
        GevechtRepository gevechtrepo = new GevechtRepository(new GevechtMSSQLContext());
        SpelerRepository spelerrepo = new SpelerRepository(new SpelerMSSQLContext());
        CPURepository cpurepo = new CPURepository(new CPUMSSQLContext());
        PersonageRepository personagerepo = new PersonageRepository(new PersonageMSSQLContext());
        GevechtViewModelConverter gevechtcvt = new GevechtViewModelConverter();
        private GevechtDetailViewModel vm;
        public GevechtController()
        {
            equipDomein = new EquipDomein();
        }
        public IActionResult Gevechtwereld(int id)
        {
            //User CurrentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("CurrentUser"));
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("CurrentUserID"));

            if (HttpContext.Session.GetInt32("StartGame") == null || HttpContext.Session.GetInt32("StartGame") == 0)
            {
                HttpContext.Session.SetString("Speler", JsonConvert.SerializeObject(spelerrepo.GetSpeler(userId)));
                HttpContext.Session.SetString("CPU", JsonConvert.SerializeObject(cpurepo.GetCPUById(id)));
                HttpContext.Session.SetString("Personage", JsonConvert.SerializeObject(personagerepo.GetPersonageBySpelerId(userId)));
                HttpContext.Session.SetString("SpelerAanZet", Convert.ToString(true));
                HttpContext.Session.SetString("potionSpelerGebruikt", Convert.ToString(false));
                HttpContext.Session.SetString("potionCPUGebruikt", Convert.ToString(false));
                HttpContext.Session.SetString("spelerLevend", Convert.ToString(true));
                HttpContext.Session.SetString("cpuLevend", Convert.ToString(true));
                HttpContext.Session.SetInt32("StartGame", 1);
            }
            
            Speler speler = JsonConvert.DeserializeObject<Speler>(HttpContext.Session.GetString("Speler"));
            CPU cpu = JsonConvert.DeserializeObject<CPU>(HttpContext.Session.GetString("CPU"));
            Gevecht gevecht = equipDomein.VulGevecht(speler, cpu);
            HttpContext.Session.SetString("Speler", JsonConvert.SerializeObject(gevecht.Speler));
            HttpContext.Session.SetString("CPU", JsonConvert.SerializeObject(gevecht.CPU));
            vm = gevechtcvt.ViewModelFromGevecht(gevecht);
            vm.SpelerAanZet = Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet"));
            vm.PotionSpelerGebruikt = Convert.ToBoolean(HttpContext.Session.GetString("potionSpelerGebruikt"));

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
            if (HttpContext.Session.GetString("Beloningen") != null)
            {
                ViewBag.Beloningen = HttpContext.Session.GetString("Beloningen");
            }
            return View(vm);
        }
        public IActionResult VolgendeBeurt()
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet")))
            {
                HttpContext.Session.SetString("SpelerAanZet", Convert.ToString(false));
                return RedirectToAction("Gevechtwereld");
            }
            else
            {
                HttpContext.Session.SetString("SpelerAanZet", Convert.ToString(true));
                return RedirectToAction("Gevechtwereld");
            }
        }
        public IActionResult Aanval()
        {
            Speler speler = JsonConvert.DeserializeObject<Speler>(HttpContext.Session.GetString("Speler"));
            CPU cpu = JsonConvert.DeserializeObject<CPU>(HttpContext.Session.GetString("CPU"));
            Personage personage = JsonConvert.DeserializeObject<Personage>(HttpContext.Session.GetString("Personage"));
            if (Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet")))
            {
                cpu.HP -= speler.Wapen.HP + personage.Damage;
                HttpContext.Session.SetString("CPU", JsonConvert.SerializeObject(cpu));
                return RedirectToAction("ControlerenGevecht");
            }
            else
            {
                speler.HP -= cpu.Wapen.HP;
                HttpContext.Session.SetString("Speler", JsonConvert.SerializeObject(speler));
                return RedirectToAction("ControlerenGevecht");
            }
        }
        public IActionResult Superaanval()
        {
            Random rnd = new Random();
            int rndsuperaanval = rnd.Next(1, 5);
            Speler speler = JsonConvert.DeserializeObject<Speler>(HttpContext.Session.GetString("Speler"));
            CPU cpu = JsonConvert.DeserializeObject<CPU>(HttpContext.Session.GetString("CPU"));
            Personage personage = JsonConvert.DeserializeObject<Personage>(HttpContext.Session.GetString("Personage"));
            if (Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet")))
            {
                if (rndsuperaanval == 1)
                {
                    cpu.HP -= speler.Wapen.HP * 2 + personage.Damage;
                    HttpContext.Session.SetInt32("superAanval", 1);
                }
                else
                {
                    HttpContext.Session.SetInt32("superAanval", 2);
                }
                HttpContext.Session.SetString("CPU", JsonConvert.SerializeObject(cpu));
                return RedirectToAction("ControlerenGevecht");
            }
            else
            {
                if (rndsuperaanval == 1)
                {
                    speler.HP -= cpu.Wapen.HP * 2;
                    HttpContext.Session.SetInt32("superAanval", 1);
                }
                else
                {
                    HttpContext.Session.SetInt32("superAanval", 2);
                }
                HttpContext.Session.SetString("Speler", JsonConvert.SerializeObject(speler));
                return RedirectToAction("ControlerenGevecht");
            }
        }
        public IActionResult Verdediging()
        {
            Speler speler = JsonConvert.DeserializeObject<Speler>(HttpContext.Session.GetString("Speler"));
            CPU cpu = JsonConvert.DeserializeObject<CPU>(HttpContext.Session.GetString("CPU"));
            potionSpelerGebruikt = Convert.ToBoolean(HttpContext.Session.GetString("potionSpelerGebruikt"));
            potionCPUGebruikt = Convert.ToBoolean(HttpContext.Session.GetString("potionCPUGebruikt"));
            if (Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet")) && potionSpelerGebruikt == false)
            {
                speler.HP += speler.Potion.HP;
                HttpContext.Session.SetString("Speler", JsonConvert.SerializeObject(speler));
                HttpContext.Session.SetString("potionSpelerGebruikt", Convert.ToString(true));
                return RedirectToAction("ControlerenGevecht");
            }
            else if (Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet")) == false && potionCPUGebruikt == false)
            {
                cpu.HP += cpu.Potion.HP;
                HttpContext.Session.SetString("CPU", JsonConvert.SerializeObject(cpu));
                HttpContext.Session.SetString("potionCPUGebruikt", Convert.ToString(true));
                return RedirectToAction("ControlerenGevecht");
            }
            return RedirectToAction("ControlerenGevecht");
        }
        public IActionResult ControlerenGevecht()
        {
            Speler speler = JsonConvert.DeserializeObject<Speler>(HttpContext.Session.GetString("Speler"));
            CPU cpu = JsonConvert.DeserializeObject<CPU>(HttpContext.Session.GetString("CPU"));
            if (speler.HP <= 0)
            {
                HttpContext.Session.SetString("spelerLevend", Convert.ToString(false));
                return RedirectToAction("GevechtBeëindigd");
            }
            else if (cpu.HP <= 0)
            {
                HttpContext.Session.SetString("cpuLevend", Convert.ToString(false));
                return RedirectToAction("GevechtBeëindigd");
            }
            else
            {
                return RedirectToAction("VolgendeBeurt");
            }
            
        }
        public IActionResult GevechtBeëindigd()
        {
            HttpContext.Session.SetString("gevechtBeëindigd", Convert.ToString(true));
            return RedirectToAction("Beloningen");

        }
        public IActionResult Beloningen()
        {
            spelerLevend = Convert.ToBoolean(HttpContext.Session.GetString("spelerLevend"));
            cpuLevend = Convert.ToBoolean(HttpContext.Session.GetString("cpuLevend"));
            Speler speler = JsonConvert.DeserializeObject<Speler>(HttpContext.Session.GetString("Speler"));
            if (spelerLevend == true && cpuLevend == false)
            {
                speler.Geld += 100;
                speler.XP += 50;
                gevechtrepo.GevechtBeëindigd(speler.XP, speler.Geld, speler.ID);
                HttpContext.Session.SetString("Beloningen", "GEWONNEN! Je hebt " + 100 + " geld en " + 50 + " XP verdiend!");
            }
            else if (spelerLevend == false && cpuLevend == true)
            {
                speler.Geld += 50;
                speler.XP += 25;
                gevechtrepo.GevechtBeëindigd(speler.XP, speler.Geld, speler.ID);
                HttpContext.Session.SetString("Beloningen", "Verloren! Je hebt " + 50 + " geld en " + 25 + " XP verdiend!");
            }
            return RedirectToAction("Gevechtwereld");
        }
        public IActionResult GevechtKeuzeCPU()
        {
            CPU cpu = JsonConvert.DeserializeObject<CPU>(HttpContext.Session.GetString("CPU"));
            potionCPUGebruikt = Convert.ToBoolean(HttpContext.Session.GetString("potionCPUGebruikt"));
            if (cpu.HP <= 5 && potionCPUGebruikt == false)
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
            CPU cpu = JsonConvert.DeserializeObject<CPU>(HttpContext.Session.GetString("CPU"));
            if (cpu.HP >= 10)
            {
                return RedirectToAction("Superaanval");
            }
            else
            {
                return RedirectToAction("Aanval");
            }
        }
    }
}