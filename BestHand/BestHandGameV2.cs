using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonEntities;

namespace BestHand
{
    class BestHandGameV2
    {
        public Deck Deck { get; set; }
        public List<Hand> Hands = new List<Hand>();
        public void StartGame()
        {
            Random rand = new Random();
            SetupEntities(rand);
            CalculateScoresOfMatchingCards();
        }

        private void CalculateScoresOfMatchingCards()
        {
            Console.WriteLine();
            foreach (var hand in Hands)
            {
                hand.AddScoresOfMatchingCards();
                Console.ResetColor();
                Console.WriteLine($"{hand.Name} Points: {hand.Points}");

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
                int colorCode = i + 2;
                ConsoleColor color = (ConsoleColor)colorCode;
                Hands.Add(new Hand(8, playerName, color, Deck));
            }

            foreach (var hand in Hands)
                hand.DisplayHand();
        }
    }
}
