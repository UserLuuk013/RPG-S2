using RPG_IB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Interfaces
{
    interface IKarakterContext
    {
        bool VoegKarakterToe(Karakter karakter);
        bool VerwijderKarakter(Karakter karakter);
        Karakter GetSpelerKarakter(int idSpeler);
        List<Karakter> GetAllKarakters(int idSpeler);
        bool UpgradeKarakter(int idKarakter, int xp, int hp);
        Karakter GetKarakterById(int idKarakter);
    }
}
