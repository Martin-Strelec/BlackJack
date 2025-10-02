using BlackJack;
using System;
using System.Collections;


namespace BlackJack
{
    class Player
    {
        //Public Variables
        public List<List<Card>> Hand { get; set; }
        public int Budget { get; set; }
        public StatusE Status { get; set; }
        public int Bet { get; set; }
        public int[] MinAndMaxBid { get; private set; }
        public bool GotInsuranceBet { get; set; }
        public enum StatusE
        {
            Default,
            Lost,
            Win,
            Draw,
            BlackJack
        } 

        //Private Variables
        private static int ID = 1;
        private Stack<State> _states;

        public Player( int budget, int[] minAndMaxBid)
        {
            this.Hand = new List<List<Card>>(); //Initializing Empty hand
            this.Budget = budget; //Adding players budget
            this.MinAndMaxBid = minAndMaxBid; //Adding player's Min and Max Bid
        }
        //Public methods
        //Method for counting the sum of each hand
        public List<int> HandSum()
        {
            List<int> temp = new List<int>();

            int sum = 0;
            for (int i = 0; i < this.Hand.Count; i++)
            {
                for (int j = 0; j < this.Hand[i].Count; j++)
                {
                    sum += this.Hand[i][j].CardValue;
                }
                temp.Add(sum);
            }
            return temp;
        }
    }
}