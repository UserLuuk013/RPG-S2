using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class GameViewModel
    {
        private List<GameDetailViewModel> games = new List<GameDetailViewModel>();
        public List<GameDetailViewModel> Games
        {
            get
            {
                return this.games;
            }
            set
            {
                this.games = value;
            }
        }
    }
}
