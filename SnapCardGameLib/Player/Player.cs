using SnapCardGameLib.Card;
using SnapCardGameLib.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnapCardGameLib.Player
{
    public class Player : IPlayer
    {
      public string Name { get; set; }
      public ICardBase PreviousCard { get; set; }
      private PlayerStack<CardBase> Stack;

        public Player()
        {
            Stack = new PlayerStack<CardBase>();
        }

      public void AddCard(ICardBase card)
      {
            var _card = card as CardBase;
            if (_card != null)
                Stack.addCard(_card);         
      }

      public ICardBase PopCard() => Stack.pop();




    }
}
