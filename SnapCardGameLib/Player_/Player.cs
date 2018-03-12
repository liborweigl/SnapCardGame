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
        public string Name { get; set; }
        public ICardBase PreviousCard { get; set; }
        public ICardBase TopPileCard { get; set; }
        public int ReactionTime { get; set; }

        static readonly object _lockerPile = new object();
      
        public static int RoundCount = 0;
        public int PlayerRound = 0;
 

        public static EventHandler<EventArgs> Snap;
        
        public static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        static Barrier barrier = new Barrier(0, (a) => {
            autoResetEvent.Set();
        });
        Thread worker;
        EventWaitHandle wh;
      

        private PlayerStack<CardBase> Stack;

        public Player(EventWaitHandle wh)
        {
            Stack = new PlayerStack<CardBase>();
            worker = new Thread(CheckCard);
            worker.Start();
            this.wh = wh;
                       
            barrier.AddParticipant();
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
            Monitor.Enter(_lockerPile);
            try
            {
                RoundCount++;
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
                    Thread.Sleep(ReactionTime); //simulate reaction time of player
                    Console.WriteLine( Name +"Monitor"+PreviousCard?.Rank.ToString() + "==" + TopPileCard?.Rank.ToString()+ "+PlayerRound+" + PlayerRound + "*RoundCount*" + RoundCount);
                    if (PreviousCard?.CompareTo(TopPileCard) == 0)
                        if (Monitor.TryEnter(_lockerPile))
                        {
                            try
                            {
                            Console.WriteLine("Snap " + Name);
                                    Snap(this, new EventArgs());
                            }
                            finally
                            {
                                Monitor.Exit(_lockerPile);
                            }
                        }
              
                barrier.SignalAndWait();
               
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

                PlayerRound++;
                Console.WriteLine("Pile change");
                Console.WriteLine(PreviousCard?.Rank.ToString() + "==" + TopPileCard?.Rank.ToString());
            //if(!worker.IsAlive)
            //     worker.Start();

        }

        public void Dispose()
        {
            worker.Join();
            barrier.Dispose();
            autoResetEvent.Close();
        }

    }
}
