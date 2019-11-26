using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestHand;

namespace GoFish
{
    public class Hand
    {
        public List<Card> Cards = new List<Card>();
        public int Points { get; set; }
        public int InitialNumber { get; set; }

        public Hand(int numOfCards, Deck deck)
        {
           
            for(int i = 0; i < numOfCards; i++)
            {
                Cards.Add(deck.Cards[i]);
                deck.Cards.Remove(deck.Cards[i]);
            }
        }
        public void RemoveDuplicates()
        {
            for(int i = Cards.Count-1; i >= 0; i--)
            {
                for(int j = i-1; j >= 0; j--)
                {
                    if(Cards[i].Value == Cards[j].Value)
                    {
                        Cards.RemoveAt(i);
                        Cards.RemoveAt(j);
                        j--;
                        i--;
                        Points++;
                        break;
                    }
                }
            }            
        }

        public void DisplayHand()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                Console.WriteLine(Cards[i].Suit + " " + Cards[i].Value);
            }
        }
    }

    
}
