using BlackJack;
using System;
using System.Collections;


namespace BlackJack
{
    class Game
    {
        //Public Variables
        public int NumberOfPlayers { get; private set;}
        public int DeckLayout { get; private set; }
        public int GameLayout { get; private set; }
        public int PlayerBudget { get; private set; }
        public int[] MinAndMaxBid { get; private set; }
        public bool Extras { get; private set; }
        
        private Stack<State> _states;
     
        //Private functions

        //Initializing variables
        private void InitVariables()
        {
            this.DeckLayout = 1;
            this.GameLayout = 0;
            this.NumberOfPlayers = 1;
            this.PlayerBudget = 1000;
            this.MinAndMaxBid = [25, 200];
            this.Extras = false;
        }

        //Initializing states
        private void InitState()
        {
            this._states = new Stack<State>();

            //Push the first state
            this._states.Push(new StateOptions(this._states));
        }

        public Game()
        {
            InitVariables();
            InitState();
        }

        //Method for starting the game
        public void Run()
        {

            while (this._states.Count > 0)
            {
                this._states.Peek().Update();

                if (this._states.Peek().RequestEnd())
                {
                    this._states.Pop();
                }
            }

            Console.WriteLine("Ending game...");
        }
    }
}