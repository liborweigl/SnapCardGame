﻿using SnapCardGameLib.Card;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SnapCardGameLib.Player_
{
    class PlayerStack<T>  : IEnumerable<T> where T: class, ICardBase
    {
        private Queue<T> CardPile { get; set; }
        #region Methods
        public PlayerStack()
        {
            CardPile = new Queue<T>();
        }

        public T pop()
        {
           T item;       
           return CardPile.TryDequeue(out item) ? item : null;
        }

        public void addPile(Stack<T> pile)
        {
            T item;
            while (pile.TryPop(out item))
                CardPile.Enqueue(item);
        }

        public void addCard(T card)
        {
            CardPile.Enqueue(card);
        }

        public bool HasItem() => CardPile.Count > 0 ? true : false;

        public IEnumerator<T> GetEnumerator()
        {
            return CardPile.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
