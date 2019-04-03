using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Models
{
    class Karakter
    {
        private string naam;
        private int hp;
        private int damage;
        private int idKarakter;
        private int prijs;
        public Karakter()
        {

        }
        public override string ToString()
        {
            return naam;
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
        public int Damage
        {
            get
            {
                return this.damage;
            }
            set
            {
                this.damage = value;
            }
        }
        public int IDKarakter
        {
            get
            {
                return this.idKarakter;
            }
            set
            {
                this.idKarakter = value;
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
    }
}
