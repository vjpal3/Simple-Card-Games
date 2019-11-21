using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestHand
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            var deck = new Deck();
            deck.Shuffle(rand);
            deck.ShowDeck();
        }
    }
}
