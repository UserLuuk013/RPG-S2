﻿using RPG_IB2;
using RPG_IB2.Datalayer.MSSQLContexts;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Datalayer.MSSQLContexts;
using RPG_IB2_WebApplication2.Datalayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class EquipDomein
    {
        ItemRepository itemrepo = new ItemRepository(new ItemMSSQLContext());
        PersonageRepository personagerepo = new PersonageRepository(new PersonageMSSQLContext());
        CPURepository cpurepo = new CPURepository(new CPUMSSQLContext());
        public Speler VulSpeler(Speler speler)
        {
            speler.Wapen = itemrepo.GetPlayerItemsById(speler.ID)[0];
            speler.Potion = itemrepo.GetPlayerItemsById(speler.ID)[1];
            speler.Personage = personagerepo.GetPersonageBySpelerId(speler.ID);
            return speler;
        }
        public CPU VulCPU(CPU cpu)
        {
            cpu.Wapen = itemrepo.GetCPUItemsById(cpu.ID)[0];
            cpu.Potion = itemrepo.GetCPUItemsById(cpu.ID)[1];
            return cpu;
        }
        public List<CPU> VulCPUs(List<CPU> cpus)
        {
            foreach (CPU cpu in cpus)
            {
                cpu.Wapen = itemrepo.GetCPUItemsById(cpu.ID)[0];
                cpu.Potion = itemrepo.GetCPUItemsById(cpu.ID)[1];
            }
            return cpus;
        }
        public Game VulGame(Speler speler, List<CPU> cpus)
        {
            Game game = new Game();
            game.Speler = VulSpeler(speler);
            game.CPUs = VulCPUs(cpus);
            return game;
        }
        public Gevecht VulGevecht(Speler speler, CPU cpu)
        {
            Gevecht gevecht = new Gevecht();
            gevecht.Speler = VulSpeler(speler);
            gevecht.CPU = VulCPU(cpu);
            gevecht.SpelerAanZet = true;
            return gevecht;
        }
    }
}
