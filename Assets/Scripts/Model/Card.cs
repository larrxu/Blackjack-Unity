using UnityEngine;
using System.Collections;

namespace Model
{
    public class Card
    {
        public readonly Suit suit;
        private int rank;
        private string[] suitNames = {"spades", "hearts", "diamonds", "clubs"};

        public int Rank { get { return rank; } }

        public Card(Suit suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
        }

        public override string ToString() {
            return "card_" + suitNames[(int)suit] + "_" + rank;
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
