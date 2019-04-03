using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;

namespace RPG_IB2
{
    public class CPU
    {
        //Fields
        private string naam;
        private int hp;
        private Item wapen;
        private Item potion;

        //Constructor
        public CPU(string naam, int hp, Item wapen, Item potion)
        {
            this.naam = naam;
            this.hp = hp;
            this.wapen = wapen;
            this.potion = potion;
        }

        //Methods
        public void HPErbij(int hpwinst)
        {
            //WEG
            hp += hpwinst;
        }
        public void HPEraf(int hpverlies)
        {
            //WEG
            hp -= hpverlies;
        }
        public void Aanval()
        {
            //WEG
        }
        public void Superaanval()
        {
            //WEG
        }
        public void Verdediging()
        {
            //WEG
        }

        //Properties
        public string Naam
        {
            get
            {
                return this.naam;
            }
            set
            {
                this.naam = value;
            }
        }
        public int HP
        {
            get
            {
                return this.hp;
            }
            set
            {
                this.hp = value;
            }
        }
        public Item Wapen
        {
            get
            {
                return this.wapen;
            }
            set
            {
                this.wapen = value;
            }
        }
        public Item Potion
        {
            get
            {
                return this.potion;
            }
            set
            {
                this.potion = value;
            }
        }
    }
}
