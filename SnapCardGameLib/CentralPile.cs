using SnapCardGameLib.Card;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SnapCardGameLib
{
    public class CentralPile<T>  : IEnumerable<T>  where T : ICardBase
    {
        Stack<T> CentralCardPile { get; set; }
        

        public void AddCard(T cardBase)
        {
            CentralCardPile.Push(cardBase);
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
