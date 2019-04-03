using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Models
{
    public class Item
    {
        private string naam;
        private int hp;
        private int prijs;
        private int id;
        private string type;
        public Item(string naam, int hp, int prijs)
        {
            this.naam = naam;
            this.hp = hp;
            this.prijs = prijs;
        }
        public Item()
        {

        }
        public override string ToString()
        {
            return $"{ Naam}";
        }
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
        public int Prijs
        {
            get
            {
                return this.prijs;
            }
            set
            {
                this.prijs = value;
            }
        }
        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}
