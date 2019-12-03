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

            DiscardCommonRoyalFamilyCards();
            //CalculateScoresOfMatchingCards();
            //CalculateScoresOfSequences();
            //CalculateScoresofRoyalFamily();
        }

        private void DiscardCommonRoyalFamilyCards()
        {
            List<List<int>> royalFamilies = new List<List<int>>();
            foreach (var hand in Hands)
            {
                royalFamilies.Add(hand.FilterRoyalFamily());
            }

            foreach (var family in royalFamilies)
            {
                Console.Write("FaceCard Group: ");
                foreach (var faceCard in family)
                {
                    Console.Write(faceCard + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("****After removing common royal family cards*****");

            for (int i = royalFamilies.Count - 1; i >= 0; i--)
            {
                if (royalFamilies[i].Count > 0 )
                {
                    for (int m = royalFamilies[i].Count - 1; m >= 0; m--)
                    {
                        var itemToRemove = 0;
                        var markForDeletion = false;
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (royalFamilies[j].Count > 0)
                            {
                                var deleted = false;
                                for (int n = royalFamilies[j].Count - 1; n >= 0; n--)
                                {
                                    if (royalFamilies[i][m] == royalFamilies[j][n] && !deleted)
                                    {
                                        markForDeletion = true;
                                        itemToRemove = royalFamilies[i][m];
                                        royalFamilies[j].RemoveAt(n);
                                        deleted = true;
                                    }
                                }
                            }
                        }
                        if (markForDeletion)
                        {
                            royalFamilies[i].Remove(itemToRemove);
                        }
                    }
                }
            }

            foreach (var family in royalFamilies)
            {
                Console.Write("FaceCard Group: ");
                foreach (var faceCard in family)
                {
                    Console.Write(faceCard + " ");
                }
                Console.WriteLine();
            }

        }

        private void CalculateScoresofRoyalFamily()
        {
            foreach (var hand in Hands)
            {
                hand.AddScoresOfRoyalFamily();
                Console.ResetColor();
                Console.WriteLine($"{hand.Name} Points: {hand.Points}");
            }
        }

        private void CalculateScoresOfSequences()
        {
            foreach (var hand in Hands)
            {
                hand.AddScoresOfSequences();
                Console.ResetColor();
                Console.WriteLine($"{hand.Name} Points: {hand.Points}");
            }
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
