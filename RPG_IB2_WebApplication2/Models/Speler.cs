using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;

namespace RPG_IB2
{
    public class Speler
    {
        //Fields
        private string naam;
        private int hp;
        private int xp;
        private int geld;
        private Item wapen;
        private Item potion;

        //Constructor
        public Speler(string naam, int hp, int xp, int geld, Item wapen, Item potion) //Gevecht
        {
            this.naam = naam;
            this.hp = hp;
            this.xp = xp;
            this.geld = geld;
            this.wapen = wapen;
            this.potion = potion;
        }
        public Speler()
        {

        }
        //public Speler(string naam, int xp, int geld) //Gamewereld
        //{
            //this.naam = naam;
            //this.xp = xp;
            //this.geld = geld;
        //}

        //Methods
        public void GeldErbij(int geldwinst)
        {
            geld += geldwinst;
        }
        public void GeldEraf(int geldverlies)
        {
            geld -= geldverlies;
        }
        public void XPErbij(int xpwinst)
        {
            xp += xpwinst;
        }
        public void XPEraf(int xpverlies)
        {
            xp -= xpverlies;
        }
        public void HPErbij(int hpwinst)
        {
            hp += hpwinst;
        }
        public void HPEraf(int hpverlies)
        {
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
        public void ResetHP()
        {
            hp = 20;
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
        public int XP
        {
            get
            {
                return this.xp;
            }
            set
            {
                this.xp = value;
            }
        }
        public int Geld
        {
            get
            {
                return this.geld;
            }
            set
            {
                this.geld = value;
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
