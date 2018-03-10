using System;
using System.Collections.Generic;
using System.Text;

namespace SnapCardGameLib.Card
{
    public interface ICardBase : IComparable
    {
         Rank Rank { get; set; }
         Suit Type { get; set; }
    }

    public class CardBase : ICardBase
    {
        public virtual Rank Rank { get; set; }
        public virtual Suit Type { get; set; }

        public virtual int CompareTo(object obj)
        {
            if (this.Rank > ((ICardBase) obj).Rank) return -1;
            if (this.Rank == ((ICardBase)obj).Rank) return 0;
            return 1;
        }

        //public static bool operator ==(CardBase operand1, CardBase operand2)
        //{
        //    return operand1.CompareTo(operand2) == 0;
        //}

        //public static bool operator !=(CardBase operand1, CardBase operand2)
        //{
        //    return operand1.CompareTo(operand2) == 0;
        //}
    }


}
