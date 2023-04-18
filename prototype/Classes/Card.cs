using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prototype.Classes
{
    public class Card
    {
        public string Id { get; private set; }
        public int Value { get; private set; }
        public Ranks Rank { get; private set; }
        public string ImageSource { get; private set; }
        public Suits Suit { get; private set; }
        public enum Suits
        {
            Clubs, Diamonds, Hearts, Spades
        }

        public enum Ranks
        {
            Ace = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13
        }

        public Card(Ranks rank, Suits suit)
        {
            Rank = rank;
            Suit = suit;
            Id = rank.ToString().ToLower() + "Of" + suit.ToString().ToLower();
            ImageSource = Id + ".png";

            if (Rank >= Ranks.Two && Rank <= Ranks.Ten)
            {
                Value = (int)Rank;
            }
            else if (Rank >= Ranks.Jack)
            {
                Value = 10;
            }
            else if (Rank == Ranks.Ace)
            {
                Value = 11;
            }
        }
    }
}
