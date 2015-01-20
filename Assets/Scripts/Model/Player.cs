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

        public int Bet
        {
            get
            {
                return bet;
            }
            set
            {
                if (value < 0)
                {
                    bet = 0;
                } else
                {
                    bet = value;
                }
            }
        }

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
            Hand = new Hand();
            Name = name;
            this.cash = cash;
        }

        public bool betCash(int amount)
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
            return true;
        }
    }
}