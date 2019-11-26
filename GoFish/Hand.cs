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
        public string Name { get; set; }


        public Hand(int numOfCards, string name, Deck deck)
        {
            Name = name;
            for(int i = 0; i < numOfCards; i++)
            {
                Cards.Add(deck.Cards[i]);
                deck.Cards.Remove(deck.Cards[i]);
            }
        }
        public bool MatchAndRemoveDuplicates()
        {
            bool matched = false;
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
                        matched = true;
                        break;
                    }
                }
            }
            return matched;
        }

        //Draw a card and remove it from the deck
        public Card DrawCardFromDeck(Random rand, Deck deck)
        {
            //int min = 2;
            //int max = 15;

            //var randSuit = deck.Suits[rand.Next(0, 4)];
            //var randVal = deck.Values[rand.Next(min, max)];
            //var nextCard = deck.Cards.Single(card => card.Suit == randSuit && card.Value == randVal);

            var nextCard = deck.Cards[0];
            deck.Cards.Remove(nextCard);

            //foreach (Card card in deck.Cards)
            //    Console.WriteLine(card.Value + " ** " + card.Suit);

            return nextCard;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }

        public void DisplayHand()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                Console.Write(Cards[i].Suit + " " + Cards[i].Value + "\t");
            }
            Console.WriteLine();
        }
    }

    
}
