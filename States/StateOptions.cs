using BlackJack;
using System.Collections;

namespace BlackJack
{
    class StateOptions : State
    {
        //Variables
        protected int NumberOfPlayers;
        protected int DeckLayout;
        protected int GameLayout;
        protected int PlayerBudget;
        protected int[] MinAndMaxBid;
        protected bool Extras;

        //Private Methods
        //User can change the number of players using this method. Default value is set to 1
        private void DefinePlayers()
        {
            int[] range = { 1, 8 };

            Console.Clear();
            Gui.Title("Number of Players");
            Gui.Announcment($"{this.NumberOfPlayers} (current) | From {range[0]} to {range[1]}");
            int? input = Gui.GetInputInt(Gui.InputOption($"{range[0]}-{range[1]}"), true);
            if (input == -1)
            {
            }
            else if (input >= range[0] && input <= range[1])
            {
                NumberOfPlayers = (int)input;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong Input!");
                Gui.PressKeyToContinue();
                DefinePlayers();
            }
        }
        //User can set the amount of budget they can spend
        private void ChooseBudget()
        {
            int[] range = { 1000, 10000 };

            Console.Clear();
            Gui.Title("Player's Budget");
            Gui.Announcment($"{this.PlayerBudget:c} (current) | From {range[0]} to {range[1]}");
            int? input = Gui.GetInputInt(Gui.InputOption($"{range[0]}-{range[1]}"), true);
            if (input == -1)
            {
            }
            else if (input >= range[0] && input <= range[1])
            {
                PlayerBudget = (int)input;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong Input!");
                Gui.PressKeyToContinue();
                ChooseBudget();
            }
        }
        //Player can set Min and Max budget for all players
        private void ChooseMinAndMaxBid()
        {
            int[] range = { (int)(PlayerBudget * 0.1), (int)(PlayerBudget * 0.3) };
            int[] def = { 25, 200 };

            Console.Clear();
            Gui.Title("Min and Max bid");
            Gui.Announcment($"MIN BID: {MinAndMaxBid[0]:c} (default) | From 0 to {range[0]}");
            int? input = Gui.GetInputInt(Gui.InputOption($"1-{range[0]}"), true);
            if (input == -1)
            {
            }
            else if (input >= 1 && input <= range[1])
            {
                MinAndMaxBid[0] = (int)input;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong Input!");
                Gui.PressKeyToContinue();
                ChooseMinAndMaxBid();
            }
            Console.Clear();

            Gui.Announcment($"MAX BID: {MinAndMaxBid[1]:c} (default) | From {range[0]} to {range[1]}");
            input = Gui.GetInputInt(Gui.InputOption($"{MinAndMaxBid[0]}-{range[1]}"), true);
            if (input == -1)
            {
            }
            else if (input >= range[0] && input <= range[1])
            {
                MinAndMaxBid[1] = (int)input;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong Input!");
                Gui.PressKeyToContinue();
                ChooseMinAndMaxBid();
            }
        }
        
        //User can change the amount of decks that will be used in game. Default is 1
        private void DeckType()
        {
            Console.Clear();
            Gui.Title("Deck Type");
            Gui.Announcment("Cards mixed from 1 (default)/ 2 / 4 / 8 deck(s)");
            int? input = Gui.GetInputInt(Gui.InputOption("1", "2", "4", "8"), true);

            if (input == -1)
            {
            }
            else if (input == 1 || input == 2 || input == 4 || input == 8)
            {
                DeckLayout = (int)input;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong Input!");
                Gui.PressKeyToContinue();
                DeckType();
            }
        }
        //Player can enable extra settings. Default is off
        private void InitVariables()
        {
            this.DeckLayout = 1;
            this.GameLayout = 0;
            this.NumberOfPlayers = 1;
            this.PlayerBudget = 1000;
            this.MinAndMaxBid = [25, 200];
            this.Extras = false;
        }

        //User can change the game layout. Default is "European" style or 0

        //private void GameStyle()
        //{
        //    int[] range = { 0, 2 };

        //    Console.Clear();
        //    Gui.Title("Game Style");
        //    Gui.Announcment("European = 0 (default), Special = 1, Show Info = 2");
        //    int? input = Gui.GetInputInt(Gui.InputOption("0", "1", "2"), true);
        //    if (input == -1)
        //    {
        //    }
        //    else if (input >= range[0] && input <= range[1])
        //    {
        //        if (input == 0)
        //        {
        //            GameLayout = 0;
        //        }
        //        else if (input == 1)
        //        {
        //            GameLayout = 1;
        //        }
        //        else
        //        {
        //            Console.Clear();

        //            Gui.GeneralText("European: In European style games only the dealer’s face up card is dealt at the start of the round. " +
        //                "Dealer's second card is dealt after all players have acted, and the dealer checks for Blackjack at this point. " +
        //                "Player Blackjacks are paid at the end of the round if the dealer does not have Blackjack. " +
        //                "If the dealer has Blackjack the rules regarding Doubled and Split hands vary from casino to casino.\n" +
        //                "OldSchool: In some casinos the players' initial two-card hands are dealt face down. All additional cards dealt to the player are given face up. " +
        //                "The initial cards are revealed by the player if the hand goes bust, or if the player wishes to split a pair. " +
        //                "Otherwise the dealer reveals the cards at the end of the round when it is time to settle the bets. " +
        //                "This style of game is rare nowadays: casinos don't like to allow players to touch the cards, because of the risk of card marking."
        //                );
        //            Gui.PressKeyToContinue();
        //            SelectExtras();
        //        }
        //    }
        //    else
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Wrong Input!");
        //        Gui.PressKeyToContinue();
        //        GameStyle();
        //    }
        //}

        //private void SelectExtras()
        //{
        //    int[] range = { 0, 2 };

        //    Console.Clear();
        //    Gui.Title("Enable Extras");
        //    Gui.Announcment($"Disabled (default) | 0 = Disable / 1 = Enable / 2 = info");
        //    int? input = Gui.GetInputInt(Gui.InputOption($"0", "1", "2"), true);
        //    if (input == -1)
        //    {
        //    }
        //    else if (input >= range[0] && input <= range[1])
        //    {
        //        if (input == 0)
        //        {
        //            Extras = false;
        //        }
        //        else if (input == 1)
        //        {
        //            Extras = true;
        //        }
        //        else
        //        {
        //            Console.Clear();
        //            Gui.GeneralText(
        //                            "This option enables a special feature in BlackJack called Five Card Charlie. \n" +
        //                            "If player collects 5 cards and does not go bust he will be immediately paid off, irrespective\n" +
        //                            "of delar's cards.");
        //            Gui.PressKeyToContinue();
        //            SelectExtras();
        //        }
        //    }
        //    else
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Wrong Input!");
        //        Gui.PressKeyToContinue();
        //        SelectExtras();
        //    }
        //}

        //Public methods
        public StateOptions(Stack<State> states) : base(states)
        {
            InitVariables(); //Initialize default variables
        }
        public override void ProcessInput(int? input)
        {
            switch (input)
            {
                case 1:
                    this._states.Push(new StateGame(
                        this._states,
                        this.NumberOfPlayers,
                        this.DeckLayout,
                        this.GameLayout,
                        this.PlayerBudget,
                        this.MinAndMaxBid,
                        this.Extras)
                        );
                    break;
                case 2:
                    DefinePlayers();
                    break;
                case 3:
                    ChooseBudget();
                    break;
                case 4:
                    ChooseMinAndMaxBid();
                    break;
                case 5:
                    DeckType();
                    break;
                case 6:
                    this._end = true;
                    break;
                default:
                    Console.WriteLine("Invalid Input!");
                    break;
            }
        }
        public override void Update()
        {

            //Display settings
            Console.Clear();
            Gui.Title("BLACKJACK");
            Console.Write("\n");
            Gui.Title($"Number of Players: {NumberOfPlayers}");
            Gui.Title($"Game Layout: {(GameLayout == 0 ? "European" : "Special")}");
            Gui.Title($"Deck Layout: {DeckLayout}");
            Gui.Title($"Each Player's Budget: {PlayerBudget:c}");
            Gui.MenuTitle("Choose");
            Gui.MenuOption("Start Game", "Choose Players", "Choose Budget", "Choose Min and Max Bid","Choose Deck Type","Exit");

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