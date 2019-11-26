using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestHand;

namespace GoFish
{
    class GoFishGame
    {
        public void StartGame()
        {
            var rand = new Random();
            Deck deck = new Deck();
            deck.Shuffle(rand);
            Hand hand1 = new Hand(5, deck);
            hand1.DisplayHand();
            Console.WriteLine("*******");
            hand1.RemoveDuplicates();
            hand1.DisplayHand();

        }
    }
}
