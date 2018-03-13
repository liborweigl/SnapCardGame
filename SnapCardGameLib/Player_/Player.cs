using SnapCardGameLib.Card;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnapCardGameLib.Player_
{
    public class Player : IPlayer, IDisposable
    {
        #region Properties
        public string Name { get; set; }
        public ICardBase PreviousCard { get; set; }
        public ICardBase TopPileCard { get; set; }
        public int ReactionTime { get; set; }

        static readonly object _lockerPile = new object();

        public static EventHandler<EventArgs> TakeCards;
        public static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        static Barrier barrier = new Barrier(0, (a) => {
                                                        });

        private Thread worker;
        private EventWaitHandle wh;
        private PlayerStack<CardBase> Stack;
        #endregion 
        public Player(EventWaitHandle wh)
        {
            Stack = new PlayerStack<CardBase>();
            worker = new Thread(CheckCard);
            worker.Start();
            this.wh = wh;
                       
            barrier.AddParticipant();
        }

        #region Methods
        public void AddCard(ICardBase card)
        {
            var _card = card as CardBase;
            if (_card != null)
                Stack.addCard(_card);
        }

        public ICardBase PopCard()
        {
            Monitor.Enter(_lockerPile);
            try
            {
                return Stack.pop();
            }
            finally
            {
               Monitor.Exit(_lockerPile);
            }

        }

      public bool HasCards()  {  return Stack.HasItem(); }

      public void CheckCard()
      {
            while (true)
            {
                Thread.Sleep(ReactionTime);
                if (PreviousCard?.CompareTo(TopPileCard) == 0)
                {
                    ShoutSnap();
                    if (Monitor.TryEnter(_lockerPile))
                    {
                        try
                        {

                            TakeCards(this, new EventArgs());

                        }
                        finally
                        {
                            Monitor.Exit(_lockerPile);
                        }
                    }
                }
              
                barrier.SignalAndWait();
                autoResetEvent.Set();
                wh.WaitOne();

            }
        }

      public void PileChange(object o, CardArgs carArgs)
      {
                if (carArgs.cardBase != null)
                {
                    PreviousCard = TopPileCard;
                    TopPileCard = carArgs.cardBase;
                }
      }

      public void Dispose()
      {
            worker.Join();
            barrier.Dispose();
            autoResetEvent.Close();
      }

      private void ShoutSnap()
      {
            Console.WriteLine("Snap!");
      }
      #endregion
    }
}
