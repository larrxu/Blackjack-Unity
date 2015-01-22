using UnityEngine;
using System.Collections;

namespace Model
{
    public enum State
    {
        Betting,
        DealFirstCardToPlayer,
        DealSecondCardToPlayer,
        CheckBlackjack,
        DealTwoCardsToDealer,
        WaitForPlayerToAct,
        WaitForDealerToAct,
        ProcessResult,
        WaitForRestart
    }
    
    public class GameState
    {
        public State State { set; get; }
        
        public object Data { set; get; }
        
        public GameState(State state, object data)
        {
            State = state;
            Data = data;
        }

        public GameState(State state) : this(state, null)
        {
        }
    }
}
