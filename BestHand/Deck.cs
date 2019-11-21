using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestHand
{
    class Deck
    {
        public List<Card> Cards = new List<Card>();

        private IEnumerable<Card> shuffledCards;
        private static string[] suits = new string[4] { "heart", "spade", "club", "diamond" };
        private static string[] values = new string[13] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};

        public Deck()
        {
            int k = 0;
            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    Cards[k] = new Card(suits[i], values[j]);
                    k++;
                }
            }
        }
        public void Shuffle(Random rand)
        {
            RandomExtensions.Shuffle(rand, this.Cards);
        }

        public void ShowDeck()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                Console.WriteLine(Cards[i].Suit + " " + Cards[i].Value);
            }
        }
    }
}
