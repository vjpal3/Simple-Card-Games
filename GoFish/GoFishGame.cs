using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestHand;

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
            
            while (Hand1.Cards.Count > 0 || Hand2.Cards.Count > 0 || Deck.Cards.Count > 0)
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

            if(Hand1.Points == Hand2.Points)
                Console.WriteLine("Tie!!");
            else
                Console.WriteLine("Winner is " + ((Hand1.Points > Hand2.Points) ? Hand1.Name : Hand2.Name));
        }

        public void SetUpEntities(Random rand)
        {
            
            Deck = new Deck();
            Deck.Shuffle(rand);
            Hand1 = new Hand(5, "Player1", Deck);
            ShowHand(Hand1);
            
            Hand2 = new Hand(5, "Player2", Deck);
            ShowHand(Hand2);

        }

        public void ShowHand(Hand hand)
        {
            Console.WriteLine($"******* {hand.Name} Before: *******");
            hand.DisplayHand();
            hand.MatchAndRemoveDuplicates();
            Console.WriteLine($"******* {hand.Name} After: *******");
            hand.DisplayHand();
            Console.WriteLine($"Points gained by {hand.Name}: {hand.Points}");
            Console.WriteLine();
        }

    }
}
