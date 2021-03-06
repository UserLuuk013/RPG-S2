﻿using RPG_IB2;
using RPG_IB2.Datalayer.MSSQLContexts;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2.Models;
using RPG_IB2_WebApplication2.Datalayer.MSSQLContexts;
using RPG_IB2_WebApplication2.Datalayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class EquipDomein
    {
        private readonly ItemRepository itemrepo;
        private readonly PersonageRepository personagerepo;
        public EquipDomein()
        {
            itemrepo = new ItemRepository(new ItemMssqlContext());
            personagerepo = new PersonageRepository(new PersonageMssqlContext());
        }
        public Speler VulSpeler(Speler speler)
        {
            speler.Wapen = itemrepo.GetPlayerItemsById(speler.ID)[0];
            speler.Potion = itemrepo.GetPlayerItemsById(speler.ID)[1];
            speler.Personage = personagerepo.GetPersonageBySpelerId(speler.ID);
            return speler;
        }
        public Cpu VulCPU(Cpu cpu)
        {
            cpu.Wapen = itemrepo.GetCPUItemsById(cpu.ID)[0];
            cpu.Potion = itemrepo.GetCPUItemsById(cpu.ID)[1];
            return cpu;
        }
        public List<Cpu> VulCPUs(List<Cpu> cpus)
        {
            foreach (Cpu cpu in cpus)
            {
                cpu.Wapen = itemrepo.GetCPUItemsById(cpu.ID)[0];
                cpu.Potion = itemrepo.GetCPUItemsById(cpu.ID)[1];
            }
            return cpus;
        }
        public Game VulGame(Speler speler, List<Cpu> cpus)
        {
            Game game = new Game();
            game.Speler = VulSpeler(speler);
            game.CPUs = VulCPUs(cpus);
            return game;
        }
        public Gevecht VulGevecht(Speler speler, Cpu cpu)
        {
            Gevecht gevecht = new Gevecht();
            gevecht.Speler = VulSpeler(speler);
            gevecht.CPU = VulCPU(cpu);
            gevecht.SpelerAanZet = true;
            gevecht.PotionSpelerGebruikt = false;
            gevecht.PotionCPUGebruikt = false;
            gevecht.SpelerLevend = true;
            gevecht.CPULevend = true;
            gevecht.GameGestart = true;

            return gevecht;
        }
        public Shop VulShop(List<Item> playeritems, List<Item> shopitems, Speler speler)
        {
            Shop shop = new Shop();
            shop.PlayerItems = playeritems;
            shop.ShopItems = shopitems;
            shop.Geld = speler.Geld;
            return shop;
        }
        public PersonageShop VulPersonageShop(Personage spelerpersonage, Personage volgendeupgrade, Speler speler)
        {
            PersonageShop personageshop = new PersonageShop();
            personageshop.XP = speler.XP;
            personageshop.SpelerPersonage = spelerpersonage;
            personageshop.VolgendeUpgrade = volgendeupgrade;
            return personageshop;
        }
    }
}
