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
                    PlayTurn(Hand1, Hand2, rand);
                }
                else
                {
                    PlayTurn(Hand2, Hand1, rand);
                }
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

        public void PlayTurn(Hand currentHand, Hand otherHand, Random rand)
        {
            Console.WriteLine($"******* {currentHand.Name} Now: *******");
            currentHand.DisplayHand();
            Console.WriteLine("Ask Match for a Card: Specify the index: ");
            int index = Convert.ToInt32(Console.ReadLine());

            bool matchFound = false;
            for(int i = 0; i < otherHand.Cards.Count; i++)
            {
                if(currentHand.Cards[index].Value == otherHand.Cards[i].Value)
                {
                    Console.WriteLine();
                    Console.WriteLine("Match Found!");
                    otherHand.Cards.Remove(otherHand.Cards[i]);
                    currentHand.Cards.Remove(currentHand.Cards[index]);
                    currentHand.Points++;
                    Console.WriteLine($"Points gained by {currentHand.Name}: {currentHand.Points}");
                    matchFound = true;
                    break;
                }
            }
            if (!matchFound)
            {
                Console.WriteLine("No Match Found. GoFish! Drawing a card from Deck...!");
                Card card = currentHand.DrawCardFromDeck(rand, Deck);
                Console.WriteLine("Card Drawn: " + card.Suit + " " + card.Value);
                for (int i = 0; i < currentHand.Cards.Count; i++)
                {
                    if (card.Value == currentHand.Cards[i].Value)
                    {
                        Console.WriteLine();
                        currentHand.Points++;
                        Console.WriteLine($"Points gained by {currentHand.Name}: {currentHand.Points}");
                        currentHand.Cards.Remove(currentHand.Cards[i]);
                        matchFound = true;
                        break;
                    }
                }
                if (!matchFound)
                    currentHand.Cards.Add(card);

                Turn = !Turn;
            }

            Console.WriteLine($"******* {currentHand.Name} Now: *******");

            currentHand.DisplayHand();
            Console.WriteLine();
        }
    }
}
