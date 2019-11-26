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
        Hand hand1, hand2;
        Deck deck;

        public void StartGame()
        {
            var rand = new Random();
            SetUpEntities(rand);
            PlayTurns(rand);
            
            
        }

        public void SetUpEntities(Random rand)
        {
            
            deck = new Deck();
            deck.Shuffle(rand);
            hand1 = new Hand(5, deck);
            Console.WriteLine("******* Hand 1 Before: *******");
            hand1.DisplayHand();
            Console.WriteLine("******* Hand 1 After: *******");
            hand1.MatchAndRemoveDuplicates();
           
            hand1.DisplayHand();
            Console.WriteLine("******* Hand 2 Before: *******");
            hand2 = new Hand(5, deck);
            hand2.DisplayHand();
            Console.WriteLine("******* Hand 2 After: *******");
            hand2.MatchAndRemoveDuplicates();
            hand2.DisplayHand();
            Console.WriteLine();
            Console.WriteLine();
        }

        public void PlayTurns(Random rand)
        {
            bool p1Turn = true;
            while (hand1.Cards.Count > 0 || hand2.Cards.Count > 0)
            {
                if(p1Turn)
                {
                    Console.WriteLine("******* Hand 1 Now: *******");
                    hand1.DisplayHand();
                    Console.WriteLine("Ask Match for a Card: Specify the index: ");
                    int index = Convert.ToInt32(Console.ReadLine());
                    bool matchFound = false;
                    for(int i = 0; i < hand2.Cards.Count; i++)
                    {
                        if(hand1.Cards[index].Value == hand2.Cards[i].Value)
                        {
                            Console.WriteLine("Match Found");
                            hand2.Cards.Remove(hand2.Cards[i]);
                            hand1.Cards.Remove(hand1.Cards[index]);
                            hand1.Points++;
                            matchFound = true;
                            break;
                        }
                    }
                    if (!matchFound)
                    {
                        Card card = hand1.DrawCardFromDeck(rand, deck);
                        for (int i = 0; i < hand1.Cards.Count; i++)
                        {
                            if (card.Value == hand1.Cards[i].Value)
                            {
                                hand1.Points++;
                                hand1.Cards.Remove(hand1.Cards[i]);
                            }
                        }
                        p1Turn = false;
                    }

                }
                Console.WriteLine("******* Hand 1 Now: *******");
                hand1.DisplayHand();
                break;
            }
        }
    }
}
