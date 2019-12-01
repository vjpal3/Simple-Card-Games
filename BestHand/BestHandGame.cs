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
            
        }

        private void SetupEntities(Random rand)
        {
            var deck = new Deck();
            deck.Shuffle(rand);
            deck.ShowDeck();



        }
    }
}
