using System;
using SnapCardGameLib.Player_;
using SnapCardGameLib.Card;
using System.Collections.Generic;
using System.Threading;

namespace SnapCardGameLib
{
    public class Game : IDisposable
    {
        private CardBox cardBox;
        private Player[] players;
        CentralPile<CardBase> centralPile = new CentralPile<CardBase>();

        EventWaitHandle wh = new AutoResetEvent(false);
        

        public Game()
        {
            this.cardBox = new CardBox();
            this.cardBox.ShuffledCard();
        }

        public void Setup()
        {
            players = new Player[] {
                                    new Player(wh) { Name = "Test1" },
                                    new Player(wh) { Name = "Test2" },
                                    new Player(wh) { Name = "Test3" },
                                    new Player(wh) { Name = "Test4" }
                                  };
            Player.Snap += Snap;

            int i = 0;
            foreach (var cardPile in cardBox.CreatePileForEachPlayer(players.Length))
            {
                foreach(var card in cardPile)
                    players[i].AddCard(card);
                i++;
            }

            foreach (var player in players)
                   centralPile.CardChange += player.PileChange;
        }

        public void Run()
        {
            int i= 0;
            IList<Player> playersInGame = new List<Player>(players);
                       
            while (playersInGame.Count > 1)
            {
                if (playersInGame[i % playersInGame.Count].HasCards())
                {
                    centralPile.AddCard((CardBase)playersInGame[i % playersInGame.Count].PopCard());
                    wh.Set();                    
                }
                else
                    playersInGame.RemoveAt(i % playersInGame.Count);

                i++;
            }

        }

        public void Snap(Object o, EventArgs e)
        {
            Player player = o as Player;

            foreach (var card in centralPile.TurnOverPile())
                player.AddCard(card);
            centralPile.Empty();
           
        }

        public void Dispose() => wh?.Close();



    }
}
