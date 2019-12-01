using System;
using System.Collections.Generic;

namespace CommonEntities
{
    public class Hand
    {
        public List<Card> Cards = new List<Card>();
        public int Points { get; set; }
        public string Name { get; set; }
        public ConsoleColor Color { get; set; }

        public Hand(int numOfCards, string name, ConsoleColor color, Deck deck)
        {
            Name = name;
            Color = color;
            for (int i = 0; i < numOfCards; i++)
            {
                Cards.Add(deck.Cards[i]);
                deck.Cards.Remove(deck.Cards[i]);
            }
        }
        public bool MatchAndRemoveDuplicates()
        {
            bool matched = false;
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (Cards[i].Value == Cards[j].Value)
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

        public void PlayTurn(Hand otherHand, Deck deck, Random rand)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine($"******* {Name} Now: *******");
            DisplayHand();
            Console.Write("Ask Match for a Card: Specify the index: { ");
            for (int i = 0; i < Cards.Count; i++)
            {
                Console.Write(i + "  ");
            }
            Console.WriteLine("}");
            int index = Convert.ToInt32(Console.ReadLine());

            bool matchFoundForTraded = false;
            for (int i = 0; i < otherHand.Cards.Count; i++)
            {
                if (Cards[index].Value == otherHand.Cards[i].Value)
                {
                    Console.WriteLine();
                    Console.WriteLine("Match Found!");
                    otherHand.Cards.Remove(otherHand.Cards[i]);
                    Cards.Remove(Cards[index]);
                    Points++;
                    Console.WriteLine($"Points gained by {Name}: {Points}");
                    matchFoundForTraded = true;
                    break;
                }
            }
            bool matchFoundForDrawn = false;
            if (!matchFoundForTraded)
            {
                Console.WriteLine("No Match Found. GoFish! Drawing a card from Deck...!");
                Card card = DrawCardFromDeck(rand, deck);
                Console.WriteLine("Card Drawn: " + card.Suit + " " + card.Value);
                for (int i = 0; i < Cards.Count; i++)
                {
                    if (card.Value == Cards[i].Value)
                    {
                        Console.WriteLine();
                        Points++;
                        Console.WriteLine($"Points gained by {Name}: {Points}");
                        Cards.Remove(Cards[i]);
                        matchFoundForDrawn = true;
                        break;
                    }
                }
                if (!matchFoundForDrawn)
                    Cards.Add(card);
            }

            Console.WriteLine($"******* {Name} Now: *******");
            DisplayHand();
            Console.WriteLine();

            if (Cards.Count > 0 && matchFoundForTraded)
                PlayTurn(otherHand, deck, rand);
        }

        public Card DrawCardFromDeck(Random rand, Deck deck)
        {
            var nextCard = deck.Cards[0];
            deck.Cards.Remove(nextCard);
            return nextCard;
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
