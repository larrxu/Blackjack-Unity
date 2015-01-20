using UnityEngine;
using System.Collections;

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
    Spades = 1,
    Hearts = 2,
    Diamonds = 3,
    Clubs = 4
}
