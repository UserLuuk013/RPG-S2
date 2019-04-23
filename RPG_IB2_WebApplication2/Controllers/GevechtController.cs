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

namespace RPG_IB2_WebApplication2.Controllers
{
    public class GevechtController : Controller
    {
        private int superAanval = 0;
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
            Speler speler = spelerrepo.GetSpeler(1);
            CPU cpu = cpurepo.GetCPUById(1);
            Gevecht gevecht = equipDomein.VulGevecht(speler, cpu);
            vm = gevechtcvt.ViewModelFromGevecht(gevecht);
            vm.SpelerAanZet = true;
        }
        public IActionResult Gevechtwereld(int id)
        {
            return View(vm);
        }
        public IActionResult VolgendeBeurt()
        {
            superAanval = 0;
            if (vm.SpelerAanZet)
            {
                vm.SpelerAanZet = false;
                return RedirectToAction("GevechtKeuzeCPU", "Gevecht");
            }
            else
            {
                vm.SpelerAanZet = true;
                return RedirectToAction("UpdateGevecht", "Gevecht");
            }
        }
        public IActionResult Aanval()
        {
            if (vm.SpelerAanZet == true)
            {
                vm.CPU.HP -= vm.Speler.Wapen.HP;
            }
            else
            {
                vm.Speler.HP -= vm.CPU.Wapen.HP;
            }
            return RedirectToAction("ControlerenGevecht", "Gevecht");
        }
        public IActionResult Superaanval()
        {
            Random rnd = new Random();
            int rndsuperaanval = rnd.Next(1, 5);
            if (vm.SpelerAanZet)
            {
                if (rndsuperaanval == 1)
                {
                    vm.CPU.HP -= vm.Speler.Wapen.HP * 2;
                    superAanval = 1;
                }
                else
                {
                    superAanval = 2;
                }
            }
            else
            {
                if (rndsuperaanval == 1)
                {
                    vm.Speler.HP -= vm.CPU.Wapen.HP * 2;
                    superAanval = 1;
                }
                else
                {
                    superAanval = 2;
                }
            }
            return RedirectToAction("ControlerenGevecht", "Gevecht");
        }
        public IActionResult Verdediging()
        {
            if (vm.SpelerAanZet == true && potionSpelerGebruikt == false)
            {
                vm.Speler.HP += vm.Speler.Potion.HP;
                potionSpelerGebruikt = true;
            }
            else if (vm.SpelerAanZet == false)
            {
                vm.CPU.HP += vm.CPU.Potion.HP;
                potionCPUGebruikt = true;
            }
            return RedirectToAction("ControlerenGevecht", "Gevecht");
        }
        public IActionResult ControlerenGevecht()
        {
            if (vm.Speler.HP <= 0)
            {
                spelerLevend = false;
                return RedirectToAction("GevechtBeëindigd", "Gevecht");
            }
            else if (vm.CPU.HP <= 0)
            {
                cpuLevend = false;
                return RedirectToAction("GevechtBeëindigd", "Gevecht");
            }
            else
            {
                return RedirectToAction("UpdateGevecht", "Gevecht");
            }
            
        }
        public IActionResult BeëindigGevecht()
        {
            gevechtBeëindigd = true;
            return RedirectToAction("Beloningen", "Gevecht");

        }
        public IActionResult Beloningen()
        {
            if (spelerLevend == true && cpuLevend == false)
            {
                vm.Speler.Geld += 100;
                vm.Speler.XP += 50;
                gevechtrepo.GevechtBeëindigd(vm.Speler.XP, vm.Speler.Geld);
            }
            else if (spelerLevend == false && cpuLevend == true)
            {
                vm.Speler.Geld += 50;
                vm.Speler.XP += 25;
                gevechtrepo.GevechtBeëindigd(vm.Speler.XP, vm.Speler.Geld);
            }
            return RedirectToAction("Gamewereld", "Game");
        }
        public IActionResult GevechtKeuzeCPU()
        {
            if (vm.CPU.HP <= 5 && potionCPUGebruikt == false)
            {
                return RedirectToAction("Verdediging", "Gevecht");
            }
            else
            {
                return RedirectToAction("AanvalKeuzeCPU", "Gevecht");
            }
        }
        public IActionResult AanvalKeuzeCPU()
        {
            if (vm.CPU.HP >= 10)
            {
                return RedirectToAction("Superaanval", "Gevecht");
            }
            else
            {
                return RedirectToAction("Aanval", "Gevecht");
            }
        }
        public IActionResult UpdateGevecht()
        {
            return View(vm);
        }
    }
}