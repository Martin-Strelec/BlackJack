using BlackJack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class StateResults : State
    {
        private List<Player> Players { get; set; }
        private Dealer Dealer { get; set; }
        private void ResetHands()
        {
            //Number of players
            for (int i = 0; i < this.Players.Count; i++)
            {
                //Number of hands of each player
                for (int j = 0; j < this.Players[i].Hand.Count; j++)
                {
                    this.Players[i].Hand.Clear();
                }   
            }
            this.Dealer.Hand = new List<Card>();
            Dealer.InitialDealOfCards(this.Players, this.Dealer);
        }
        private void Results(Player player, Dealer dealer)
        {
            //Dealer has BlackJack
            if (player.Status.Equals(Player.StatusE.BlackJack))
            {
                if (dealer.Status.Equals(Dealer.StatusE.BlackJack))
                {
                    player.Status = Player.StatusE.Draw;
                    dealer.Status = Dealer.StatusE.Draw;
                    player.Budget += player.Bet;
                }
                else
                {
                    player.Status = Player.StatusE.Win;
                    player.Budget += player.Bet + (player.Bet*4);
                    dealer.Status = Dealer.StatusE.Lost;
                }
            }
            //Dealer Busted
            else if (player.Status.Equals(Player.StatusE.Lost))
            {
                if (player.Status.Equals(dealer.Status))
                {
                    player.Status = Player.StatusE.Draw;
                    dealer.Status = Dealer.StatusE.Draw;
                    player.Budget += player.Bet;
                }
                else
                {
                    player.Status = Player.StatusE.Lost;
                    dealer.Status = Dealer.StatusE.Win;
                }   
            }
            //Dealer did not go over or does not have a BlackJack
            else
            {
                if (!dealer.Status.Equals(Dealer.StatusE.Lost))
                {
                    if (player.HandSum()[0] > dealer.HandSum())
                    {
                        player.Status = Player.StatusE.Win;
                        player.Budget += player.Bet + (player.Bet * 2);

                        dealer.Status = Dealer.StatusE.Lost;
                    } 
                    else
                    {
                        player.Status = Player.StatusE.Lost;
                        dealer.Status = Dealer.StatusE.Win;
                    }
                }
                else
                {
                    player.Status = Player.StatusE.Win;
                    player.Budget += player.Bet + (player.Bet * 2);

                    dealer.Status = Dealer.StatusE.Lost;
                }
            }
        }
        public override void Update()
        {
            Console.Clear();
            Gui.MenuTitle("Results");
            for (int i = 0; i < this.Players.Count; i++)
            {
                Results(this.Players[i], this.Dealer);
                Gui.DrawResults(this.Players[i], i + 1, this.Dealer);
                Players[i].Bet = 0;
            }
            Gui.MenuTitle("Options");
            Gui.MenuOption("Continue", "Exit menu");

            int? input = Gui.GetInputInt("Input", false);
            if (input == -1)
            {
                Update();
            }
            else
            {
                ProcessInput(input);
            }
            //Console.Clear();
            //Gui.Announcment("In Results state");
            //Gui.PressKeyToContinue();
        }

        public StateResults(Stack<State> states, List<Player> players, Dealer dealer) : base(states)
        {
            this.Players = players;
            this.Dealer = dealer;
        }

        public override void ProcessInput(int? input)
        {
            switch (input)
            {
                case 1:
                    ResetHands();
                    this._end = true;
                    break;
                case 2:
                    this._states.Clear();
                    this._states.Push(new StateOptions(this._states));
                    break;
                default:
                    Console.WriteLine("Invalid Input!");
                    break;
            }
        }
    }
}
