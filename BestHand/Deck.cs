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

        
        public string[] Suits = new string[] { "heart", "spade", "club", "diamond" };
        public string[] Values = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};

        public Deck()
        {
            for (int i = 0; i < Suits.Length; i++)
            {
                for (int j = 0; j < Values.Length; j++)
                {
                    Cards.Add(new Card(Suits[i], Values[j]));
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
