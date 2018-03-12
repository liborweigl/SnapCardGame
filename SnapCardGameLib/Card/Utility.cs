using System;
using System.Collections.Generic;
using System.Text;

namespace SnapCardGameLib.Card
{
    static class Utility
    {
        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }
    }

    static class SetupPackage
    {
        public static readonly Suit[] Suits;
        public static readonly Rank[] Ranks;

       static SetupPackage()
       {
            Suits = new Suit[] { Suit.clubs, Suit.diamonts, Suit.spades, Suit.hearts };

            Ranks = new Rank[] { Rank.two, Rank.tree, Rank.four, Rank.five, Rank.six, Rank.seven,
                                     Rank.eight,Rank.nine, Rank.ten , Rank.jack, Rank.queen, Rank.king ,
                                     Rank.ace
            };
       }
    }
}
