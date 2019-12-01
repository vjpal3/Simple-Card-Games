using System;
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
        public Hand Hand1 { get; set; }
        public Hand Hand2 { get; set; }
        public void StartGame()
        {
            Random rand = new Random();
            SetupEntities(rand);
            DeclareWinner();
        }

        private void DeclareWinner()
        {
            Hand1.CalculatePoints();
            Hand2.CalculatePoints();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Hand1.Name} Points: {Hand1.Points}");
            Console.WriteLine($"{Hand2.Name} Points: {Hand2.Points}");

            Console.WriteLine();
            if (Hand1.Points == Hand2.Points)
                Console.WriteLine("Tie!!");
            else
                Console.WriteLine("Winner is " + ((Hand1.Points > Hand2.Points) ? Hand1.Name : Hand2.Name));
        }

        private void SetupEntities(Random rand)
        {
            Deck = new Deck();
            Deck.Shuffle(rand);
            
            Hand1 = new Hand(8, "Player1", ConsoleColor.Yellow, Deck);
            Hand1.DisplayHand();
            Hand2 = new Hand(8, "Player2", ConsoleColor.Green, Deck);
            Hand2.DisplayHand();
        }
    }
}
