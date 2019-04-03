using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2
{
    public class Gevecht
    {
        private Speler speler;
        private CPU cpu;
        private int level;
        private string datum;
        private string tijd;
        private string winnaar;
        private bool voltooid;
        public Gevecht(Speler speler, CPU cpu, int level)
        {
            this.speler = speler;
            this.cpu = cpu;
            this.level = level;
            this.datum = DateTime.Now.ToString("dd/MM/yyyy");
            this.tijd = DateTime.Now.ToString("h:mm:ss tt");
        }
        public Speler Speler
        {
            get
            {
                return this.speler;
            }
            set
            {
                this.speler = value;
            }
        }
        public CPU CPU
        {
            get
            {
                return this.cpu;
            }
            set
            {
                this.cpu = value;
            }
        }
        public int Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }
        public string Datum
        {
            get
            {
                return this.datum;
            }
            set
            {
                this.datum = value;
            }
        }
        public string Tijd
        {
            get
            {
                return this.tijd;
            }
            set
            {
                this.tijd = value;
            }
        }
        public string Winnaar
        {
            get
            {
                return this.winnaar;
            }
            set
            {
                this.winnaar = value;
            }
        }
        public bool Voltooid
        {
            get
            {
                return this.voltooid;
            }
            set
            {
                this.voltooid = value;
            }
        }
    }
}
