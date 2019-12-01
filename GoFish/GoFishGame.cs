using System;
using System.Linq;
using CommonEntities;

namespace GoFish
{
    class GoFishGame
    {
        public Hand Hand1 { get; set; }
        public  Hand Hand2 { get; set; }
        public Deck Deck { get; set; }
        public bool Turn { get; set; }
        
        public void StartGame()
        {
            var rand = new Random();
            SetUpEntities(rand);
            Turn = true;

            while (Hand1.Cards.Count > 0 && Hand2.Cards.Count > 0 && Deck.Cards.Count > 0)
            {
                if (Turn)
                {
                    Hand1.PlayTurn(Hand2, Deck, rand);
                }
                else
                {
                    Hand2.PlayTurn(Hand1, Deck, rand);
                }
                Turn = !Turn;
            }

            DeclareWinner();
        }

        private void DeclareWinner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Hand1.Name} Points: {Hand1.Points}");
            Console.WriteLine($"{Hand2.Name} Points: {Hand2.Points}");

            if (Hand1.Points == Hand2.Points)
                Console.WriteLine("Tie!!");
            else
                Console.WriteLine("Winner is " + ((Hand1.Points > Hand2.Points) ? Hand1.Name : Hand2.Name));
        }

        public void SetUpEntities(Random rand)
        {
            
            Deck = new Deck();
            Deck.Shuffle(rand);
            Hand1 = new Hand(5, "Player1", ConsoleColor.Yellow, Deck);
            Hand1.DisplayHand();
            ShowInitialHand(Hand1);
            Hand2 = new Hand(5, "Player2", ConsoleColor.Red, Deck);
            ShowInitialHand(Hand2);
        }

        public void ShowInitialHand(Hand hand)
        {
            Console.ForegroundColor = hand.Color;
            Console.WriteLine($"******* {hand.Name} Before: *******");
            hand.DisplayHand();
            hand.MatchAndRemoveDuplicates();
            Console.WriteLine($"After removing duplicates if any, {hand.Name}: ");
            hand.DisplayHand();
            Console.WriteLine($"Points gained by {hand.Name}: {hand.Points}");
            Console.WriteLine();
        }

    }
}
