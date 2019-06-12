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
        private readonly GevechtRepository gevechtrepo = new GevechtRepository(new GevechtMssqlContext());
        private readonly SpelerRepository spelerrepo = new SpelerRepository(new SpelerMssqlContext());
        private readonly CPURepository cpurepo = new CPURepository(new CpuMssqlContext());
        private readonly PersonageRepository personagerepo = new PersonageRepository(new PersonageMssqlContext());
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

            if (HttpContext.Session.GetString("Gevecht") == null || !JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht")).GameGestart)
            {
                Speler speler = spelerrepo.GetSpelerByID(userId);
                Cpu cpu = cpurepo.GetCPUById(id);
                HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(equipDomein.VulGevecht(speler, cpu)));
                HttpContext.Session.SetString("Personage", JsonConvert.SerializeObject(personagerepo.GetPersonageBySpelerId(speler.ID)));
            }

            Gevecht gevecht = JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht"));
            GevechtDetailViewModel vm = gevechtcvt.ViewModelFromGevecht(gevecht);
            vm.SpelerAanZet = gevecht.SpelerAanZet;
            vm.PotionSpelerGebruikt = gevecht.PotionSpelerGebruikt;

            if (gevecht.SuperAanval == Gevecht.Superaanval.Geslaagd)
            {
                ViewBag.SuperAanval = "Superaanval is geslaagd!";
                gevecht.SuperAanval = Gevecht.Superaanval.Geen;
            }
            else if (gevecht.SuperAanval == Gevecht.Superaanval.Mislukt)
            {
                ViewBag.SuperAanval = "Superaanval is mislukt!";
                gevecht.SuperAanval = Gevecht.Superaanval.Geen;
            }

            if (gevecht.Beloningen != "")
            {
                ViewBag.Beloningen = gevecht.Beloningen;
            }
            HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
            return View(vm);
        }

        //In deze Methode wordt de bepaald of de Speler of CPU aanzet is. Als de speler aanzet is, wordt de boolean SpelerAanZet false.
        //Als de CPU aanzet is, wordt de boolean SpelerAanZet true.
        public IActionResult VolgendeBeurt()
        {
            Gevecht gevecht = JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht"));
            if (gevecht.SpelerAanZet)
            {
                gevecht.SpelerAanZet = false;
                HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
                return RedirectToAction("Gevechtwereld");
            }
            else
            {
                gevecht.SpelerAanZet = true;
                HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
                return RedirectToAction("Gevechtwereld");
            }
        }

        //In deze methode wordt de aanval uitgevoerd. De aanval wordt uitgevoerd op basis van de boolean SpelerAanZet. Indien de boolean true is (aanval Speler)
        //wordt de damage die de Speler aanricht in HP afgehaald van het totaal van de CPU. Indien de boolean false is (aanval CPU) wordt de damage die de
        //CPU aanricht in HP afgehaald van het totaal van de Speler.
        public IActionResult Aanval()
        {
            Gevecht gevecht = JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht"));
            Personage personage = JsonConvert.DeserializeObject<Personage>(HttpContext.Session.GetString("Personage"));
            if (gevecht.SpelerAanZet)
            {
                gevecht.CPU.HP -= gevecht.Speler.Wapen.HP + personage.Damage;
            }
            else
            {
                gevecht.Speler.HP -= gevecht.CPU.Wapen.HP;
            }
            HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
            return RedirectToAction("ControlerenGevecht");
        }

        //In deze methode wordt de superaanval uitgevoerd. De aanval wordt uitgevoerd op basis van de boolean SpelerAanZet en een randomgetal van 1 tot 4. Indien
        //de boolean true is (aanval Speler) en het randomgetal gelijk is aan 1 (1/4 kans) wordt de damage die de Speler aanricht (wapen damage maal 2) in HP
        //afgehaald van het totaal van de CPU. Indien de boolean false is (aanval CPU) en het randomgetal gelijk is aan 1 (1/4 kans) wordt de damage die de CPU
        //aanricht (wapen damage maal 2) in HP afgehaald van het totaal van de Speler.
        public IActionResult Superaanval()
        {
            Random rnd = new Random();
            int rndsuperaanval = rnd.Next(1, 5);

            Gevecht gevecht = JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht"));
            Personage personage = JsonConvert.DeserializeObject<Personage>(HttpContext.Session.GetString("Personage"));

            if (gevecht.SpelerAanZet)
            {
                if (rndsuperaanval == 1)
                {
                    gevecht.CPU.HP -= gevecht.Speler.Wapen.HP * 2 + personage.Damage;
                    gevecht.SuperAanval = Gevecht.Superaanval.Geslaagd;
                }
                else
                {
                    gevecht.SuperAanval = Gevecht.Superaanval.Mislukt;
                }
            }
            else
            {
                if (rndsuperaanval == 1)
                {
                    gevecht.Speler.HP -= gevecht.CPU.Wapen.HP * 2;
                    gevecht.SuperAanval = Gevecht.Superaanval.Geslaagd;
                }
                else
                {
                    gevecht.SuperAanval = Gevecht.Superaanval.Mislukt;
                }
            }
            HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
            return RedirectToAction("ControlerenGevecht");
        }

        //In deze methode wordt de verdediging uitgevoerd. De verdediging wordt uitgevoerd op basis van de boolean SpelerAanZet. Indien de boolean true is
        //(verdediging Speler) wordt de HP van de healthpot opgeteld bij het totaal HP van de Speler. Indien de boolean false is (verdediging CPU) wordt de
        //HP van de healthpot opgeteld bij het totaal HP van de CPU.
        public IActionResult Verdediging()
        {
            Gevecht gevecht = JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht"));
            if (gevecht.SpelerAanZet && !gevecht.PotionSpelerGebruikt)
            {
                gevecht.Speler.HP += gevecht.Speler.Potion.HP;
                gevecht.PotionSpelerGebruikt = true;
            }
            else if (!gevecht.SpelerAanZet && !gevecht.PotionCPUGebruikt)
            {
                gevecht.CPU.HP += gevecht.CPU.Potion.HP;
                gevecht.PotionCPUGebruikt = true;
            }
            HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
            return RedirectToAction("ControlerenGevecht");
        }

        //In deze methode wordt het gevecht gecontroleerd. Het controleren van het gevecht wordt gedaan op basis van de HP's van zowel de Speler, als de CPU. Indien
        //het totaal HP van de Speler gelijk aan of kleiner dan 0 is, is het gevecht voorbij en heeft de CPU het gevecht gewonnen. Indien het totaal HP van de CPU
        //gelijk aan of kleiner dan 0 is, is het gevecht voorbij en heeft de Speler het gevecht gewonnen. Zolang beide partijen meer dan 0 HP hebben gaat het
        //gevecht gewoon door en gaat het systeem naar VolgendeBeurt.
        public IActionResult ControlerenGevecht()
        {
            Gevecht gevecht = JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht"));
            if (gevecht.Speler.HP <= 0)
            {
                gevecht.SpelerLevend = false;
                HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
                return RedirectToAction("Beloningen");
            }
            else if (gevecht.CPU.HP <= 0)
            {
                gevecht.CPULevend = false;
                HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
                return RedirectToAction("Beloningen");
            }
            else
            {
                return RedirectToAction("VolgendeBeurt");
            }
        }

        //In deze methode worden de beloningen aan de Speler toegekend op basis van het resultaat van het gevecht. Indien de Speler levend is en de CPU dood
        //(Speler heeft gewonnen) verdient de Speler een maximaal aantal Geld en XP. Indien de Speler dood is en de CPU levend (CPU heeft gewonnen) verdient
        //de Speler een minimaal aantal Geld en XP. Verder zorgt de methode er nog voor dat de gebruiker terug wordt gestuurd naar de Gamewereld en krijgt
        //de Speler in een MessageBox zijn beloningen te zien.
        public IActionResult Beloningen()
        {
            Gevecht gevecht = JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht"));
            if (gevecht.SpelerLevend && !gevecht.CPULevend)
            {
                gevecht.Speler.Geld += gevecht.CPU.Geld;
                gevecht.Speler.XP += gevecht.CPU.XP;
                gevechtrepo.GevechtBeëindigd(gevecht.Speler.XP, gevecht.Speler.Geld, gevecht.Speler.ID);
                gevecht.Beloningen = "GEWONNNEN! Je hebt " + gevecht.CPU.Geld + " geld en " + gevecht.CPU.XP + " XP verdiend!";
            }
            else if (!gevecht.SpelerLevend && gevecht.CPULevend)
            {
                gevecht.Speler.Geld += gevecht.CPU.Geld / 2;
                gevecht.Speler.XP += gevecht.CPU.XP / 2;
                gevechtrepo.GevechtBeëindigd(gevecht.Speler.XP, gevecht.Speler.Geld, gevecht.Speler.ID);
                gevecht.Beloningen = "VERLOREN! Je hebt " + (gevecht.CPU.Geld / 2) + " geld en " + (gevecht.CPU.XP / 2) + "XP verdiend!";
            }
            gevecht.GameGestart = false;
            gevecht.SuperAanval = Gevecht.Superaanval.Geen;
            HttpContext.Session.SetString("Gevecht", JsonConvert.SerializeObject(gevecht));
            return RedirectToAction("Gamewereld", "Game");
        }

        //In deze methode vindt de gevechtkeuze van de CPU plaats. Dit gebeurt op basis van het HP van de CPU en de boolean PotionCPUGebruikt.
        //Indien het HP van de CPU gelijk of groter dan 5 is en de boolean PotionCPUGebruikt false is gaat de CPU in de verdediging. Als dit
        //niet het geval is gaat de CPU in de aanval.
        public IActionResult GevechtKeuzeCPU()
        {
            Gevecht gevecht = JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht"));
            if (gevecht.CPU.HP <= 5 && !gevecht.PotionCPUGebruikt)
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
            Gevecht gevecht = JsonConvert.DeserializeObject<Gevecht>(HttpContext.Session.GetString("Gevecht"));
            if (gevecht.CPU.HP >= 10)
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