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

            CardBox cardBox = new CardBox();
            cardBox.ShuffledCard();
            Console.WriteLine("Hello World!");
        }
    }
}
