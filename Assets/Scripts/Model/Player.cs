using UnityEngine;
using System.Collections;
using System;

namespace Model
{
    public class Player
    {
        private int cash;
        private int bet;

        public string Name { get; set; }

        public Hand Hand { get; set; }

        public int Bet { get { return bet; } }

        public int Cash
        {
            get
            {
                return cash;
            }
            set
            {
                if (value < 0)
                {
                    cash = 0;
                } else
                {
                    cash = value;
                }
            }
        }

        public Player(string name, int cash)
        {
            bet = 0;
            Hand = new Hand();
            Name = name;
            this.cash = cash;
        }

        public void ResetBet()
        {
            bet = 0;
        }

        public void AddCash(int amount)
        {
            cash += amount;
        }

        public bool BetCash(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Invalid bet amount:" + amount);
            }
            if (cash < amount)
            {
                return false;
            }
            cash -= amount;
            bet += amount;
            return true;
        }
    }
}