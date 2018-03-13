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

        EventWaitHandle wh = new ManualResetEvent(false);
      
        private int gameRound;

        public Game()
        {
            this.cardBox = new CardBox();
            this.cardBox.ShuffledCard();
            Player.TakeCards += TakeCards;
        }

        public void Setup()
        {

            players = new Player[] {
                                     new Player(wh) { Name = "Test1", ReactionTime = 1 },
                                     new Player(wh) { Name = "Test2", ReactionTime = 1 },
                                     new Player(wh) { Name = "Test3", ReactionTime = 1 },
                                   };

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
            IList<Player> playersInGame = new List<Player>(players);

            while (playersInGame.Count > 1)
            {
                var index = gameRound % playersInGame.Count;
                var a = playersInGame[index].PopCard();
                    centralPile.AddCard((CardBase)a);
                    wh.Set();
                    wh.Reset();

                if (!playersInGame[index].HasCards())
                     playersInGame.RemoveAt(index);

                gameRound++;

                Player.autoResetEvent.WaitOne();
            }

        }

        
        public void TakeCards(Object o, EventArgs e)
        {
            Player player = o as Player;

            foreach (var card in centralPile.TurnOverPile())
                player.AddCard(card);
            centralPile.Empty();           
        }

        public void Dispose()
        {
            wh?.Close();
            foreach (var player in players)
                player.Dispose();
         }

    }
}
