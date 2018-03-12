﻿using SnapCardGameLib.Card;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SnapCardGameLib.Player_
{
    public class Player : IPlayer , IDisposable
    {
        public string Name { get; set; }
        public ICardBase PreviousCard { get; set; }
        public ICardBase TopPileCard { get; set; }

        private static readonly object _lockerPile = new object();

        public static EventHandler<EventArgs> Snap;

        Thread worker;
        EventWaitHandle wh;

        private PlayerStack<CardBase> Stack;

        public Player(EventWaitHandle wh)
        {
            Stack = new PlayerStack<CardBase>();
            worker = new Thread(CheckCard);
            worker.Start();
            this.wh = wh;

        }

        public void AddCardPile(Array a)
        {

        }


        public void AddCard(ICardBase card)
        {
            var _card = card as CardBase;
            if (_card != null)
                Stack.addCard(_card);
        }

        public ICardBase PopCard()
        {

            lock (_lockerPile)
                return Stack.pop();

        }

      public bool HasCards() => Stack.HasItem();

      public void CheckCard()
      {
            while (true)
            {
                if (PreviousCard?.CompareTo(TopPileCard) == 0)
                {
                    lock (_lockerPile)
                        Console.WriteLine("Snap for user" + Name);
                        Snap(this, new EventArgs());
                }

                wh.WaitOne();
            }
        }

      public void PileChange(object o, CardArgs carArgs)
      {
            PreviousCard = TopPileCard;
            TopPileCard = carArgs.cardBase;    
      }

        public void Dispose()
        {
            worker.Join();             
        }

    }
}