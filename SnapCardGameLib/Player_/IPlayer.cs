using SnapCardGameLib.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnapCardGameLib.Player_
{
    public interface IPlayer
    {
       string Name { get; set; }
        ICardBase PreviousCard { get; set; } 
        ICardBase TopPileCard { get; set; }
        int ReactionTime { get; set; }
        ICardBase PopCard();
        void AddCard(ICardBase card);
        bool HasCards();
    }
}
