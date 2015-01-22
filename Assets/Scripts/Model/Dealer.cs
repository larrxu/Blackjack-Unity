using UnityEngine;
using System.Collections;

namespace Model
{
    public class Dealer
    {
        public Hand Hand { get; set; }

        public Dealer()
        {
            Hand = new Hand();
        }
    }
}
