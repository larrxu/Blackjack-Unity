using UnityEngine;
using System.Collections;

public class Dealer : MonoBehaviour
{
    public Hand Hand { get; set; }

    public Dealer()
    {
        Hand = new Hand();
    }
}

