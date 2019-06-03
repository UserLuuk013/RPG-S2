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
        private readonly EquipDomein equipDomein;
        private readonly GevechtRepository gevechtrepo = new GevechtRepository(new GevechtMSSQLContext());
        private readonly SpelerRepository spelerrepo = new SpelerRepository(new SpelerMSSQLContext());
        private readonly CPURepository cpurepo = new CPURepository(new CPUMSSQLContext());
        private readonly PersonageRepository personagerepo = new PersonageRepository(new PersonageMSSQLContext());
        private readonly GevechtViewModelConverter gevechtcvt = new GevechtViewModelConverter();
        public GevechtController()
        {
            equipDomein = new EquipDomein();
        }

        //In de methode Gevechtwereld worden Sessions geset indien deze nog gelijk zijn aan null. Vervolgens worden de Sessions geget en deze worden omgezet naar Models.
        //Ook worden de ViewBags gevuld indien nodig en worden deze meegegeven aan de View.
        public IActionResult Gevechtwereld(int id)
        {
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
            GevechtDetailViewModel vm = gevechtcvt.ViewModelFromGevecht(gevecht);
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

        //In deze Methode wordt de bepaald of de Speler of CPU aanzet is. Als de speler aanzet is, wordt de boolean SpelerAanZet false.
        //Als de CPU aanzet is, wordt de boolean SpelerAanZet true.
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

        //In deze methode wordt de aanval uitgevoerd. De aanval wordt uitgevoerd op basis van de boolean SpelerAanZet. Indien de boolean true is (aanval Speler)
        //wordt de damage die de Speler aanricht in HP afgehaald van het totaal van de CPU. Indien de boolean false is (aanval CPU) wordt de damage die de
        //CPU aanricht in HP afgehaald van het totaal van de Speler.
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

        //In deze methode wordt de superaanval uitgevoerd. De aanval wordt uitgevoerd op basis van de boolean SpelerAanZet en een randomgetal van 1 tot 4. Indien
        //de boolean true is (aanval Speler) en het randomgetal gelijk is aan 1 (1/4 kans) wordt de damage die de Speler aanricht (wapen damage maal 2) in HP
        //afgehaald van het totaal van de CPU. Indien de boolean false is (aanval CPU) en het randomgetal gelijk is aan 1 (1/4 kans) wordt de damage die de CPU
        //aanricht (wapen damage maal 2) in HP afgehaald van het totaal van de Speler.
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

        //In deze methode wordt de verdediging uitgevoerd. De verdediging wordt uitgevoerd op basis van de boolean SpelerAanZet. Indien de boolean true is
        //(verdediging Speler) wordt de HP van de healthpot opgeteld bij het totaal HP van de Speler. Indien de boolean false is (verdediging CPU) wordt de
        //HP van de healthpot opgeteld bij het totaal HP van de CPU.
        public IActionResult Verdediging()
        {
            Speler speler = JsonConvert.DeserializeObject<Speler>(HttpContext.Session.GetString("Speler"));
            CPU cpu = JsonConvert.DeserializeObject<CPU>(HttpContext.Session.GetString("CPU"));
            bool potionSpelerGebruikt = Convert.ToBoolean(HttpContext.Session.GetString("potionSpelerGebruikt"));
            bool potionCPUGebruikt = Convert.ToBoolean(HttpContext.Session.GetString("potionCPUGebruikt"));
            if (Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet")) && !potionSpelerGebruikt)
            {
                speler.HP += speler.Potion.HP;
                HttpContext.Session.SetString("Speler", JsonConvert.SerializeObject(speler));
                HttpContext.Session.SetString("potionSpelerGebruikt", Convert.ToString(true));
                return RedirectToAction("ControlerenGevecht");
            }
            else if (!Convert.ToBoolean(HttpContext.Session.GetString("SpelerAanZet")) && !potionCPUGebruikt)
            {
                cpu.HP += cpu.Potion.HP;
                HttpContext.Session.SetString("CPU", JsonConvert.SerializeObject(cpu));
                HttpContext.Session.SetString("potionCPUGebruikt", Convert.ToString(true));
                return RedirectToAction("ControlerenGevecht");
            }
            return RedirectToAction("ControlerenGevecht");
        }

        //In deze methode wordt het gevecht gecontroleerd. Het controleren van het gevecht wordt gedaan op basis van de HP's van zowel de Speler, als de CPU. Indien
        //het totaal HP van de Speler gelijk aan of kleiner dan 0 is, is het gevecht voorbij en heeft de CPU het gevecht gewonnen. Indien het totaal HP van de CPU
        //gelijk aan of kleiner dan 0 is, is het gevecht voorbij en heeft de Speler het gevecht gewonnen. Zolang beide partijen meer dan 0 HP hebben gaat het
        //gevecht gewoon door en gaat het systeem naar VolgendeBeurt.
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

        //In deze methode komt het systeem zodra het totaal HP van de Speler of CPU kleiner of gelijk is aan 0. Deze methode set gevechtBeëindigd naar true
        //en stuurt het systeem naar de methode Beloningen.
        public IActionResult GevechtBeëindigd()
        {
            HttpContext.Session.SetString("gevechtBeëindigd", Convert.ToString(true));
            return RedirectToAction("Beloningen");
        }

        //In deze methode worden de beloningen aan de Speler toegekend op basis van het resultaat van het gevecht. Indien de Speler levend is en de CPU dood
        //(Speler heeft gewonnen) verdient de Speler een maximaal aantal Geld en XP. Indien de Speler dood is en de CPU levend (CPU heeft gewonnen) verdient
        //de Speler een minimaal aantal Geld en XP. Verder zorgt de methode er nog voor dat de gebruiker terug wordt gestuurd naar de Gamewereld en krijgt
        //de Speler in een MessageBox zijn beloningen te zien.
        public IActionResult Beloningen()
        {
            bool spelerLevend = Convert.ToBoolean(HttpContext.Session.GetString("spelerLevend"));
            bool cpuLevend = Convert.ToBoolean(HttpContext.Session.GetString("cpuLevend"));
            Speler speler = JsonConvert.DeserializeObject<Speler>(HttpContext.Session.GetString("Speler"));
            if (spelerLevend && !cpuLevend)
            {
                speler.Geld += 100;
                speler.XP += 50;
                gevechtrepo.GevechtBeëindigd(speler.XP, speler.Geld, speler.ID);
                HttpContext.Session.SetString("Beloningen", "GEWONNEN! Je hebt " + 100 + " geld en " + 50 + " XP verdiend!");
            }
            else if (!spelerLevend && cpuLevend)
            {
                speler.Geld += 50;
                speler.XP += 25;
                gevechtrepo.GevechtBeëindigd(speler.XP, speler.Geld, speler.ID);
                HttpContext.Session.SetString("Beloningen", "Verloren! Je hebt " + 50 + " geld en " + 25 + " XP verdiend!");
            }
            HttpContext.Session.SetInt32("StartGame", 0);
            HttpContext.Session.SetString("gevechtBeëindigd", Convert.ToString(false));
            return RedirectToAction("Gamewereld", "Game");
        }

        //In deze methode vindt de gevechtkeuze van de CPU plaats. Dit gebeurt op basis van het HP van de CPU en de boolean PotionCPUGebruikt.
        //Indien het HP van de CPU gelijk of groter dan 5 is en de boolean PotionCPUGebruikt false is gaat de CPU in de verdediging. Als dit
        //niet het geval is gaat de CPU in de aanval.
        public IActionResult GevechtKeuzeCPU()
        {
            CPU cpu = JsonConvert.DeserializeObject<CPU>(HttpContext.Session.GetString("CPU"));
            bool potionCPUGebruikt = Convert.ToBoolean(HttpContext.Session.GetString("potionCPUGebruikt"));
            if (cpu.HP <= 5 && !potionCPUGebruikt)
            {
                return RedirectToAction("Verdediging");
            }
            else
            {
                return RedirectToAction("AanvalKeuzeCPU");
            }
        }

        //In deze methode vindt de aanvalkeuze van de CPU plaats. Dit gebeurt op basis van de HP van de CPU. Indien de HP van de CPU gelijk of
        //groter dan 10 is voert de CPU de superaanval uit. Als dit niet het geval is voert de CPU de normale aanval uit.
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