using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Model;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        public PlayerController PlayerController;
        public DealerController DealerController;

        public GameState GameState { get; set; }

        private Deck deck;

        void Awake()
        {
            DealerController.Dealer = new Dealer();
            PlayerController.Player = new Player("lex", 123);
            deck = new Deck();
        }

        void Start()
        {
            StartCoroutine(AddCards());
        }

        void FixedUpdate()
        {
            //switch();
        }

        IEnumerator AddCards()
        {
            while (true)
            {
                PlayerController.AddCard(deck.Deal());
                yield return new WaitForSeconds(2);
                DealerController.AddCard(deck.Deal());
                yield return new WaitForSeconds(2);
            }
        }

        public void OnClickHitButton()
        {
            print("clicked");
            if (GameState == GameState.WaitForPlayerToAct)
            {
                // allow click, do action..
            }
        }
    }
}

namespace Model
{
    public enum GameState
    {
        WaitForPlayerToAct
    }
}