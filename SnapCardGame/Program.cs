
using SnapCardGameLib;
using SnapCardGameLib.Card;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnapCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Setup();
            game.Run();          
        }
    }
}
