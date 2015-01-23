using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Model;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        public PlayerController PlayerController;
        public DealerController DealerController;
        public GameObject GameResultText;

        public GameState GameState { get; set; }

        private Deck deck;
        private int fixedBet = 100;

        void Awake()
        {
            DealerController.Dealer = new Dealer();
            PlayerController.Player = new Player("lex", 9999999);
            deck = new Deck();
        }

        void Start()
        {
            GameState = new GameState(State.Betting);
            StartCoroutine(gameProcess());
        }

        IEnumerator gameProcess()
        {
            while (true)
            {
                Debug.Log("State:" + Enum.GetName(typeof(State), GameState.State));
                switch (GameState.State)
                {
                    case State.Betting:
                        {
                            break;
                        }
                    case State.DealFirstCardToPlayer:
                        {
                            yield return StartCoroutine(PlayerController.AddCard(deck.Deal()));
                            GameState.State = State.DealSecondCardToPlayer;
                            break;
                        }
                    case State.DealSecondCardToPlayer:
                        {
                            yield return StartCoroutine(PlayerController.AddCard(deck.Deal()));
                            GameState.State = State.CheckBlackjack;
                            break;
                        }
                    case State.CheckBlackjack:
                        {
                            if (isBlackjack(PlayerController.Player.Hand))
                            {
                                // blackjack, player wins!
                                GameState = new GameState(State.ProcessResult, GameResult.DealerLoseToBlackjack);
                            } else
                            {
                                GameState.State = State.DealTwoCardsToDealer;
                            }
                            break;
                        }
                    case State.DealTwoCardsToDealer:
                        {
                            yield return StartCoroutine(DealerController.AddCard(deck.Deal()));
                            yield return StartCoroutine(DealerController.AddCard(deck.Deal()));
                            GameState.State = State.WaitForPlayerToAct;
                            break;
                        }
                    case State.WaitForPlayerToAct:
                        {
                            break;
                        }
                    case State.WaitForDealerToAct:
                        {
                            GameAction action = DealerController.DecideAction(PlayerController.Player.Hand);
                            if (action == GameAction.Hit)
                            {
                                yield return StartCoroutine(DealerController.AddCard(deck.Deal()));
                                if (isBreaking(DealerController.Dealer.Hand))
                                {
                                    GameState = new GameState(State.ProcessResult, GameResult.DealerLose);
                                }
                            } else if (action == GameAction.Stand)
                            {
                                GameState = new GameState(
                                    State.ProcessResult, 
                                    compareHands(DealerController.Dealer.Hand, PlayerController.Player.Hand));
                            }
                            break;
                        }
                    case State.ProcessResult:
                        {
                            GameResult gameResult = (GameResult)GameState.Data;
                            if (gameResult == GameResult.DealerLoseToBlackjack)
                            {
                                GameResultText.GetComponent<Text>().text = "Dealer Lose To Blackjack";
                            } else
                            {
                                DealerController.showSecondCard();
                            }
                            if (gameResult == GameResult.DealerLose)
                            {
                                GameResultText.GetComponent<Text>().text = "Dealer Lose";
                            } else if (gameResult == GameResult.PlayerLose)
                            {
                                GameResultText.GetComponent<Text>().text = "Player Lose";
                            } else if (gameResult == GameResult.Tie)
                            {
                                GameResultText.GetComponent<Text>().text = "Tie";
                            }
                            
                            // Clean up...
                            PlayerController.Player.ResetBet();
                            PlayerController.Player.Hand.Reset();
                            DealerController.Dealer.Hand.Reset();
                            deck.Shuffle();

                            GameState.State = State.WaitForRestart;
                            break;
                        }
                    case State.WaitForRestart:
                        {
                            break;
                        }
                }
                yield return null;
            }
        }

        public void OnClickBetButton()
        {
            if (GameState.State == State.Betting)
            {
                PlayerController.BetCash(fixedBet);
                GameState.State = State.DealFirstCardToPlayer;
            }
        }

        public void OnClickHitButton()
        {
            if (GameState.State == State.WaitForPlayerToAct)
            {
                StartCoroutine(PlayerController.AddCard(deck.Deal()));
                if (isBreaking(PlayerController.Player.Hand))
                {
                    // Check for breaking...
                    GameState = new GameState(State.ProcessResult, GameResult.PlayerLose);
                } else
                {
                    GameState.State = State.WaitForPlayerToAct;
                }
            }
        }

        public void OnClickStandButton()
        {
            if (GameState.State == State.WaitForPlayerToAct)
            {
                GameState.State = State.WaitForDealerToAct;
            }
        }

        public void OnClickDoubleDownButton()
        {
            if (GameState.State == State.WaitForPlayerToAct)
            {
                // Double bet
                PlayerController.BetCash(fixedBet);
                // Hit
                StartCoroutine(PlayerController.AddCard(deck.Deal()));
                if (isBreaking(PlayerController.Player.Hand))
                {
                    // Check for breaking...
                    GameState = new GameState(State.ProcessResult, GameResult.PlayerLose);
                } else
                {
                    // Stand
                    GameState.State = State.WaitForDealerToAct;
                }
            }
        }

        public void OnClickRestartButton()
        {
            if (GameState.State == State.WaitForRestart)
            {
                Application.LoadLevel(0);
            }
        }

        public static int CalculateHand(Hand hand)
        {
            int sum = 0;
            bool haventSeenA = true;
            foreach (Card card in hand.Cards)
            {
                if (haventSeenA && card.Rank == 1)
                {
                    sum += 11;
                    haventSeenA = false;
                } else if (card.Rank > 10)
                {
                    sum += 10;
                } else
                {
                    sum += card.Rank;
                }
            }
            if (sum > 21 && !haventSeenA)
            {
                sum -= 10;
            }
            return sum;
        }

        private bool isBreaking(Hand hand)
        {
            return CalculateHand(hand) > 21;
        }

        private bool isBlackjack(Hand hand)
        {
            return CalculateHand(hand) == 21 && hand.Size == 2;
        }

        private GameResult compareHands(Hand dealerHand, Hand playerHand)
        {
            int dealerHandValue = CalculateHand(dealerHand);
            int playerHandValue = CalculateHand(playerHand);
            if (dealerHandValue > playerHandValue)
            {
                return GameResult.PlayerLose;
            } else if (dealerHandValue == playerHandValue)
            {
                return GameResult.Tie;
            } else
            {
                return GameResult.DealerLose;
            }
        }
    }
}