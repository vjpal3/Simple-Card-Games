﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonEntities;


namespace BestHand
{
    class BestHandGame
    {
        public Deck Deck { get; set; }
        public List<Hand> Hands = new List<Hand>();
        
        public void StartGame()
        {
            Random rand = new Random();
            SetupEntities(rand);
            DeclareWinner();
        }

        private void DeclareWinner()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var hand in Hands)
            {
                hand.CalculateFinalScore();
                Console.WriteLine($"{hand.Name} Points: {hand.Points}");
            }

            Console.WriteLine();
            if (Hands.All(hand => hand.Points == Hands.First().Points))
                Console.WriteLine("Tie!!");
            else
            {
                int maxPoints = Hands.Max(hand => hand.Points);
                Console.WriteLine("Winner is " + (Hands.First(hand => hand.Points == maxPoints)).Name);
            }
        }

        private void SetupEntities(Random rand)
        {
            Deck = new Deck();
            Deck.Shuffle(rand);
            Console.WriteLine("How many Players: Choose a number betwwen 2 to 4");
            int numOfPlayers = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numOfPlayers; i++)
            {
                var playerName = "Player" + (i + 1);
                int colorCode = i+2;
                ConsoleColor color = (ConsoleColor)colorCode;
                Hands.Add(new Hand(8, playerName, color, Deck));
            }

            foreach (var hand in Hands)
                hand.DisplayHand();
        }
    }
}
