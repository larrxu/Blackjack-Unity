using UnityEngine;
using System.Collections;

namespace Model
{
    public class Dealer : MonoBehaviour
    {
        public Hand Hand { get; set; }

        public Dealer()
        {
            Hand = new Hand();
        }
    }
}
