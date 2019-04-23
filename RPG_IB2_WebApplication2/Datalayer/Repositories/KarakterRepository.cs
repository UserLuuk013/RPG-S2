using RPG_IB2.Datalayer.Interfaces;
using RPG_IB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Repositories
{
    class KarakterRepository
    {
        private IKarakterContext context;
        public KarakterRepository(IKarakterContext context)
        {
            this.context = context;
        }
        public bool VoegKarakterToe(Karakter karakter)
        {
            return context.VoegKarakterToe(karakter);
        }
        public bool VerwijderKarakter(Karakter karakter)
        {
            return context.VerwijderKarakter(karakter);
        }
        public Karakter GetSpelerKarakter(int idSpeler)
        {
            return context.GetSpelerKarakter(idSpeler);
        }
        public List<Karakter> GetAllKarakters(int idSpeler)
        {
            return context.GetAllKarakters(idSpeler);
        }
        public bool UpgradeKarakter(int idKarakter, int xp, int hp)
        {
            return context.UpgradeKarakter(idKarakter, xp, hp);
        }
        public Karakter GetKarakterById(int idKarakter)
        {
            return context.GetKarakterById(idKarakter);
        }
    }
}
