using BlackJack;
using BlackJack.Entities;
using System.Collections;
using System.Transactions;

namespace BlackJack
{
    class StateGame : State
    {
        //Variables
        protected int NumberOfPlayers;
        protected int DeckLayout;
        protected int GameLayout;
        protected int PlayerBudget;
        protected int[] MinAndMaxBid;
        protected bool Extras;

        //Private Variables
        private List<Player> Players;
        private Deck Deck;
        private Dealer Dealer;

        //Private methods
        //Initialize Players
        private void InitPlayers()
        {
            this.Players = new List<Player>();
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                Players.Add(new Player(this.PlayerBudget, this.MinAndMaxBid));
            }
        }

        //Initialize Deck
        private void InitDeck()
        {
            this.Deck = new Deck(DeckLayout);
        }

        //Initialize Dealer
        private void InitDealer()
        {
            this.Dealer = new Dealer(Deck.Cards, Players);
        }

        //Method for the Main game event
        private void PlayerTurn(Player player, int playerID)
        {
            int turn = 0;
            bool exit = false;
            
            //Ask the user to enter his bet at the start of the round
            if (turn == 0)
            {
                Console.Clear();
                Gui.Title("Choose bet");
                int bet = Validation.ModifyIntRange(player.MinAndMaxBid[0], player.MinAndMaxBid[1], $"Enter bet ({MinAndMaxBid[0]}-{MinAndMaxBid[1]})");
                player.Bet += bet;
                player.Budget -= bet;
            }
            
            //Display menu accordingly when player has a blackjack
            if (player.HandSum()[0] == 21)
            {
                Console.Clear();
                Gui.DrawGameGui(player, playerID, this.Dealer);
                Gui.MenuOption("Continue");
                switch (Gui.GetInputInt("Input", false))
                {
                    case 1:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            }

            while (!exit)
            {
                //Display menu accordingly when player busts
                if (player.HandSum()[0] > 21)
                {
                    Console.Clear();
                    Gui.DrawGameGui(player, playerID, this.Dealer);
                    Gui.MenuOption("Continue");
                    switch (Gui.GetInputInt("Input", false))
                    {
                        case 1:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid Input!");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Gui.DrawGameGui(player, playerID, this.Dealer);
                    Gui.MenuOption("Hit", "Stand","Surrender");
                    switch (Gui.GetInputInt("Input",false))
                    {
                        case 1:
                            Dealer.Hit(player);
                            break;
                        case 2:
                            exit = true;
                            break;
                        case 3:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid Input!");
                            break;
                    }
                }
                turn++;
            }
        }
        //Public methods
        public StateGame(Stack<State> states, int numberOfPlayers, int deckLayout, int gameLayout, int playerBudget, int[] minAndMaxBid, bool extras) : base(states)
        {
            this.DeckLayout = deckLayout;
            this.GameLayout = gameLayout;
            this.NumberOfPlayers = numberOfPlayers;
            this.PlayerBudget = playerBudget;
            this.MinAndMaxBid = minAndMaxBid;
            this.Extras = extras;
            InitDeck();
            InitPlayers();
            InitDealer();
            Dealer.InitialDealOfCards(this.Players, this.Dealer);
        }

        public override void ProcessInput(int? input)
        {
            switch (input)
            {
                case 1:
                    this._states.Push(new StateResults(this._states, this.Players, this.Dealer));
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

        public override void Update()
        {   
            for (int i = 0; i < Players.Count; i++)
            {
                Console.Clear();
                Gui.Title($"Player: {i+1}");
                Gui.PressKeyToContinue();
                PlayerTurn(Players[i], i);
            }

            //Display the menu
            Console.Clear();
            Gui.Announcment("Players finished their turn. Do you want to display the results?");
            Gui.MenuOption("Display Results", "Exit");

            int? input = Gui.GetInputInt("Input", false);
            if (input == -1)
            {
                Update();
            }
            else
            {
                ProcessInput(input);
            }
        }


    }
}