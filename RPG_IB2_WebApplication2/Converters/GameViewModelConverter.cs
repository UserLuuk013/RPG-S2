using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Converters
{
    public class GameViewModelConverter
    {
        public Game ViewModelToGame(GameDetailViewModel vm)
        {
            Game g = new Game()
            {
                Speler = vm.Speler,
                CPUs = vm.CPUs
            };
            return g;
        }
        public GameDetailViewModel ViewModelFromGame(Game g)
        {
            GameDetailViewModel vm = new GameDetailViewModel()
            {
                Speler = g.Speler,
                CPUs = g.CPUs
            };
            return vm;
        }
    }
}
