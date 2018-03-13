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
       #region Properties
        public event EventHandler<CardArgs> CardChange {
            add { _cardChange += value; }
            remove { _cardChange -= value; }
        }

        private EventHandler<CardArgs> _cardChange;


        Stack<T> CentralCardPile { get; set; }
        #endregion

        #region Methods
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
            _cardChange?.Invoke(this, new CardArgs());
            CentralCardPile.Clear();
        }

        public Queue<T> TurnOverPile()
        {
            var array = CentralCardPile.ToArray();
            Array.Reverse<T>(array);
            return new Queue<T>(array);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return CentralCardPile.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion 
    }
}
