using SnapCardGameLib.Card;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SnapCardGameLib
{
   public class CardArgs : EventArgs
   {
        public ICardBase cardBase;
   }

public class CentralPile<T>  : IEnumerable<T>  where T : ICardBase
    {
       public event EventHandler<CardArgs> CardChange {
            add { _cardChange += value; }
            remove { _cardChange -= value; }
        }

        private EventHandler<CardArgs> _cardChange;


        Stack<T> CentralCardPile { get; set; }

        public int RoundNumber { get; set; }

        public CentralPile()
        {
            CentralCardPile = new Stack<T>();
        }

        public void AddCard(T cardBase)
        {
            CentralCardPile.Push(cardBase);
            _cardChange?.Invoke(this, new CardArgs() { cardBase = cardBase });

        }

        public void Empty()
        {
            //CentralList
            CentralCardPile.Clear();
        }

        public Queue<T> TurnOverPile()
        {
            return new Queue<T>(CentralCardPile.ToArray());
        }

        public IEnumerator<T> GetEnumerator()
        {
            return CentralCardPile.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
