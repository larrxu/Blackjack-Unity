using UnityEngine;
using System.Collections;

namespace Model
{
    public class Card
    {
        public readonly Suit suit;
        public readonly int rank;

        public Card(Suit suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
        }
    }

    public enum Suit
    {
        Spades,
        Hearts,
        Diamonds,
        Clubs
    }
}
