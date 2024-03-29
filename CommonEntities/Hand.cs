﻿using System;
using System.Collections.Generic;
using System.Linq;

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

        public void RemoveRoyals(List<int> royalFamily)
        {
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                int val = 0;
                switch (Cards[i].Value)
                {
                    case "J":
                        val = 11;
                        break;
                    case "Q":
                        val = 12;
                        break;
                    case "K":
                        val = 13;
                        break;
                }
                if (val > 10 && !royalFamily.Contains(val))
                    Cards.RemoveAt(i);
                else
                    royalFamily.Remove(val);
            }
            //DisplayHand();
        }

        public void ExchangeCards(Random rand, Deck deck, List<int> fibonacciTrade)
        {
            DisplayHand();
            var tradeNumber = EnforceFibTradeRule(fibonacciTrade);
            string[] input = new string[tradeNumber];
            input = GetCards(tradeNumber, input);

            while (IsMatchingTrade(input))
            {
                PrintErrorMsg("You cannot trade same cards!");
                input = GetCards(tradeNumber, input);
            }

            if(tradeNumber > 2 && HandHasFaceCard())
            {
                while (!TradeHasFaceCard(input))
                {
                    PrintErrorMsg("You must trade a Face Card!");
                    input = GetCards(tradeNumber, input);
                }
            }

            Console.WriteLine("You will be recieving following Cards: ");

            foreach (var item in input)
            {
                var tradedInput = item.Split(' ');
                var tradedCard = Cards.Find(card => card.Value == tradedInput[1] && card.Suit == tradedInput[0]);
                Cards.Remove(tradedCard);
                var newCard = DrawCardFromDeck(rand, deck);
                Console.Write($"{newCard.Suit} {newCard.Value} ");
                Cards.Add(newCard);
            }
            Console.WriteLine("\n");

        }

        private bool HandHasFaceCard()
        {
            string[] faceValues = new string[] { "J", "Q", "K" };
            return Cards.Any(card => faceValues.Contains(card.Value));
        }

        private bool TradeHasFaceCard(string[] input)
        {
            string[] faceValues = new string[] { "J", "Q", "K" };
            return input.Any(item => faceValues.Contains(item.Split()[1]));
        }

        private void PrintErrorMsg(string errorMsg)
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(errorMsg);
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = Color;
        }

        private static string[] GetCards(int tradeNumber, string[] input)
        {
            for (var i = 0; i < tradeNumber; i++)
            {
                Console.Write("Type Card Value and Suit separated by a space (like - heart 3):  ");
                input[i] = Console.ReadLine();
            }
            return input;
        }

        private bool IsMatchingTrade(string[] input)
        {
            return input.GroupBy(item => item).Any(group => group.Count() > 1);
        }

        private int EnforceFibTradeRule(List<int> fibonacciTrade)
        {
            Console.Write($"{Name}, How many cards you want to trade? { String.Join(" ", fibonacciTrade) }: ");
            int result;
            var tradeNumber = int.TryParse(Console.ReadLine(), out result) ? result : 0;

            while (!fibonacciTrade.Contains(tradeNumber))
            {
                Console.Write($"Enter a Number from this list: { String.Join(" ", fibonacciTrade) }: ");
                tradeNumber = int.TryParse(Console.ReadLine(), out result) ? result : 0;
            }
            return tradeNumber;
        }

        public void AddMatchingCardsBonus()
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
                        Points += (int)Scores.MatchesBonus;
                        break;
                    }
                }
            }
        }

        public void CalculateFinalScore()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                switch(Cards[i].Value)
                {
                    case "J":
                    case "Q":
                    case "K":
                        Points += (int)Scores.RoyalFamilyValue;
                        break;
                    case "A":
                        Points += (int)Scores.AceValue;
                        break;
                    default:
                        Points += Convert.ToInt32(Cards[i].Value);
                        break;
                }
            }
        }

        public void AddSequencesBonus()
        {
            List<List<int>> groups = GroupSequences();

            foreach (var group in groups)
            {
                if(group.Count >= 3)
                {
                    Points += (group.Count - 2) * (int)Scores.SequencesBonus;
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
                if (group.Count == 0 || val - group[group.Count - 1] == 1)
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
            Console.ForegroundColor = Color;
            DisplayGroups(groups, "");
            return groups;
        }

        public static void DisplayGroups(List<List<int>> groups, string msg)
        {
            Console.WriteLine(msg);
            foreach (var group in groups)
            {
                Console.Write("Group: ");
                foreach (var num in group)
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
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
            return cardsIntValues;
        }

        public void AddRoyalFamilyBonus()
        {
            List<int> royalFamily = FilterRoyalFamily();
            if (royalFamily.Count >= 2)
            {
                Points += (royalFamily.Count - 1) * (int)Scores.RoyalFamilyBonus;
            }
        }

        public List<int> FilterRoyalFamily()
        {
            List<int> sortedValues = SortCardsValues();
            List<int> royalFamily = new List<int>();
            foreach (var val in sortedValues)
            {
                if (val > 10 && val < 14)
                    royalFamily.Add(val);
            }
            return royalFamily;
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
                        Console.WriteLine("Match found with newly drawn card");
                        Console.WriteLine($"Points gained by {Name}: {Points}");
                        Cards.Remove(Cards[i]);
                        matchFoundForDrawn = true;
                        break;
                    }
                }
                if (!matchFoundForDrawn)
                    Cards.Add(card);
            }

            if(!matchFoundForTraded)
            {
                Console.WriteLine($"******* {Name} Now: *******");
                DisplayHand();
                Console.WriteLine();
            }
            
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

        public void DisplayScore()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine($"{Name} Points: {Points}");
        }
    }
}
