using SnapCardGameLib.Card;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SnapCardGameLib.Card
{
    public class CardBox
    {

        IList<CardBase> pile { get; set; }
        private Rank[] ranks;
        private Suit[] suits;

        public CardBox()
        {
            ranks = SetupPackage.Ranks;
            suits = SetupPackage.Suits;
            this.pile = new List<CardBase>();
            CreateBox();
        }

        public CardBox(IList<CardBase> pile, Suit[] suits, Rank[] ranks)
        {
            this.suits = suits;
            this.ranks = ranks;
            CreateBox();
        }

        private void CreateBox()
        {
            foreach (var rank in ranks)
                foreach (var suit in suits)
                    pile.Add(new CardBase() { Type = suit, Rank = rank });
        }

        public void ShuffledCard()
        {
            var random = new Random();
            int i = pile.Count;
            while (i > 1)
            {
                i--;
                var j = random.Next(i);
                if (i != j)
                {
                    pile.Swap(i, j);
                }
            }
        }

        public IEnumerable<IList<CardBase>> CreatePileForEachPlayer(int playerCount)
        {
            var discard = pile.Count % playerCount;
            var cardPerPlayer = (pile.Count - discard) / playerCount;

            int i = 0;
            int seq = new int();
          
            while (i < playerCount)
            {
                IList<CardBase> list = new List<CardBase>();
                 seq += cardPerPlayer;
                for (int j = cardPerPlayer * i; j < seq; j++)
                {
                    list.Add(pile[j]);
                }
                i++;
                yield return list;
            }
         
        }

    }





}


