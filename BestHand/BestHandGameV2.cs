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

            RemoveCommonRoyalFamilyCards();
            AwardMatchingCardsBonus();
            AwardSequencesBonus();
            AwardRoyalFamilyBonus();
            GetFinalScore();
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

        private void RemoveCommonRoyalFamilyCards()
        {
            List<List<int>> royalFamilies = GetRemainingRoyals();
            for (int i = 0; i < Hands.Count; i++)
                Hands[i].RemoveRoyals(royalFamilies[i]);
        }

        private List<List<int>> GetRemainingRoyals()
        {
            List<List<int>> royalFamilies = new List<List<int>>();
            foreach (var hand in Hands)
            {
                royalFamilies.Add(hand.FilterRoyalFamily());
            }
            Console.ResetColor();
            Console.WriteLine("Royals before removal");
            Hand.DisplayGroups(royalFamilies);

            for (int i = royalFamilies.Count - 1; i >= 0; i--)
            {
                if (royalFamilies[i].Count > 0)
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
            Console.WriteLine("****Royals after removal*****");
            Hand.DisplayGroups(royalFamilies);
            return royalFamilies;
        }

        private void AwardMatchingCardsBonus()
        {
            Console.ResetColor();
            Console.WriteLine("Adding Matching cards bonus if any");
            foreach (var hand in Hands)
            {
                hand.AddMatchingCardsBonus();
                hand.DisplayScore();
            }
            Console.WriteLine();
        }

        private void AwardSequencesBonus()
        {
            Console.ResetColor();
            Console.WriteLine("Adding Sequences bonus if any");
            foreach (var hand in Hands)
            {
                hand.AddSequencesBonus();
                hand.DisplayScore();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void AwardRoyalFamilyBonus()
        {
            Console.ResetColor();
            Console.WriteLine("Adding Royal family bonus if any");
            foreach (var hand in Hands)
            {
                hand.AddRoyalFamilyBonus();
                hand.DisplayScore();
            }
            Console.WriteLine();
        }

        private void GetFinalScore()
        {
            Console.ResetColor();
            Console.WriteLine("Final Score");
            foreach (var hand in Hands)
            {
                hand.CalculateFinalScore();
                hand.DisplayScore();
            }
            Console.WriteLine();
        }
    }
}
