using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestHand
{
    class Player
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        public int Number { get; set; }

        public Player(string name, int numberOfCards)
        {
            Name = name;
            Number = numberOfCards;
        }


    }
}
