﻿using System;
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

        public void CalculatePoints()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                switch(Cards[i].Value)
                {
                    case "J":
                    case "Q":
                    case "K":
                        Points += 10;
                        break;
                    case "A":
                        Points += 11;
                        break;
                    default:
                        Points += Convert.ToInt32(Cards[i].Value);
                        break;
                }
            }

        }

        

        public void AddScoresOfMatchingCards()
        {
            List<Card> tempList = new List<Card>();
            tempList.AddRange(Cards);

            for (int i = tempList.Count - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (tempList[i].Value == tempList[j].Value)
                    {
                        tempList.RemoveAt(i);
                        tempList.RemoveAt(j);
                        j--;
                        i--;
                        Points += 3;
                        break;
                    }
                }
            }
        }

        public void AddScoresOfSequences()
        {
            List<List<int>> groups = GroupSequences();
            foreach (var group in groups)
            {
                if(group.Count >= 3)
                {
                    Points += (group.Count - 2) * 3;
                }
            }
        }

        public List<List<int>> GroupSequences()
        {
            List<int> sortedValues = SortCardsValues();

            var groups = new List<List<int>>();
            var group = new List<int>();

            foreach (var val in sortedValues)
            {
                if (group.Count == 0 || val - group[group.Count - 1] <= 1)
                {
                    group.Add(val);
                }
                else
                {
                    groups.Add(group);
                    group = new List<int> { val };
                }
            }
            groups.Add(group);

            foreach (var g in groups)
            {
                Console.Write("Group: ");
                foreach (var num in g)
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }
            return groups;
        }

        private List<int> SortCardsValues()
        {
            var cardsIntValues = new List<int>();
            foreach (var card in Cards)
            {
                switch (card.Value)
                {
                    case "J":
                        cardsIntValues.Add(11);
                        break;
                    case "Q":
                        cardsIntValues.Add(12);
                        break;
                    case "K":
                        cardsIntValues.Add(13);
                        break;
                    case "A":
                        cardsIntValues.Add(14);
                        break;
                    default:
                        cardsIntValues.Add(Convert.ToInt32(card.Value));
                        break;
                }
            }
            cardsIntValues.Sort();
            Console.ForegroundColor = Color;
            Console.Write("Sorted Values: ");
            
            foreach (var val in cardsIntValues)
            {
                Console.Write(val + " ");
            }
            Console.WriteLine();
            return cardsIntValues;
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
            Console.ForegroundColor = Color;
            Console.WriteLine($"{Name}: ");
            for (int i = 0; i < Cards.Count; i++)
            {
                Console.Write(Cards[i].Suit + " " + Cards[i].Value + "\t");
            }
            Console.WriteLine("\n");
        }
    }


}
