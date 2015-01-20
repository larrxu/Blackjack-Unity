using UnityEngine;
using System.Collections;

public class Deck
{

    private Card[] cards;
    private int dealIndex;

    public Deck()
    {
        foreach (Suit suit in (Suit[]) System.Enum.GetValues(typeof(Suit)))
        {
            for (int rank = 1; rank <= 13; rank++)
            {
                cards [(int)suit * rank - 1] = new Card(suit, rank);
            }
        }
        Shuffle();
    }

    void Shuffle()
    {
        dealIndex = 0;
        for (int currIndex = 0; currIndex < 51; currIndex++)
        {
            int targetIndex = Random.Range(currIndex, 52);
            Card temp = cards [currIndex];
            cards [currIndex] = cards [targetIndex];
            cards [targetIndex] = temp;
        }
    }

    Card Deal()
    {
        return cards [dealIndex++];
    }
}
