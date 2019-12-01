using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonEntities
{
    public class Card
    {
        private string suit;
        private string value;

        public string Suit { get => suit; set => suit = value; }
        public string Value { get => value; set => this.value = value; }

        public Card(string suit, string value)
        {
            Suit = suit;
            Value = value;
        }
    }
}
