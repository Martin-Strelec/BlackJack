using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static BlackJack.Player;

namespace BlackJack.Entities
{
    internal class Dealer
    {
        //Public Variables
        public List<Card> Hand { get; set; }
        public bool GotBlackJack { get; private set; }
        public StatusE Status { get; set; }
        public enum StatusE
        {
            Default,
            Lost,
            Win,
            Draw,
            BlackJack
        }

        //Private variables
        private List<Card> Deck;
        private List<Player> Players;
        
        //Private methods
        public Dealer(List<Card> deck, List<Player> players)
        {
            this.Hand = new List<Card>(); //Empty hand
            this.Deck = deck; //Assigning deck
            this.Players = players; //Passing list of players for dealer to work with
        }

        //Method used for dealing cards at the start of the game
        public void InitialDealOfCards(List<Player> players, Dealer dealer)
        {
            //Creation of temporary Lists that are then passed to players and dealer
            List<List<Card>> tempPlayer = new List<List<Card>>();
            for (int i = 0; i < players.Count; i++)
            {
                tempPlayer.Add(new List<Card>()); //Added by ChatGPT
            }
            List<Card> tempDealer = new List<Card>();
            
            //variables for temporary sums
            int dealerSum = 0;
            int[] playerSum = new int[players.Count];

            //If deck is not empty
            if (Deck.Count != 0) 
            {
                //Draw two cards for player and dealer
                for (int i = 0; i < 2; i++) 
                {
                    //Do this for all players   
                    for (int j = 0; j < this.Players.Count; j++) 
                    {
                        //Checking for Ace card. If current deck + value of Ace > 21 => set card value to 1
                        if (this.Deck[0].CardValue == 11 && (playerSum[j] + 11) > 21)
                        {
                            this.Deck[0].CardValue = 1;
                            tempPlayer[j].Add(this.Deck[0]);
                            playerSum[j] += this.Deck[0].CardValue;
                            this.Deck.RemoveAt(0); //Remove Card from deck
                        }
                        else
                        {
                            tempPlayer[j].Add(this.Deck[0]);
                            playerSum[j] += this.Deck[0].CardValue;
                            this.Deck.RemoveAt(0);
                        }
                    }

                    //Checking for Ace card. If current deck + value of Ace > 21 => set card value to 1
                    if (this.Deck[0].CardValue == 11 && (dealerSum + 11) > 21)
                    {
                        this.Deck[0].CardValue = 1;
                        tempDealer.Add(this.Deck[0]); //Do this for dealer
                        dealerSum += this.Deck[0].CardValue;
                        this.Deck.RemoveAt(0); //Remove Card from deck
                    }
                    else
                    {
                        tempDealer.Add(this.Deck[0]); //Do this for dealer
                        dealerSum += this.Deck[0].CardValue;
                        this.Deck.RemoveAt(0); //Remove Card from deck
                    }
                }
                //Draw cards until dealer's hand sum is over 17
                while (dealerSum < 17)
                {
                    if (dealer.HandSum() + this.Deck[0].CardValue > 21)
                    {
                        dealer.Status = Dealer.StatusE.Lost;
                    }
                    tempDealer.Add(this.Deck[0]);
                    dealerSum += this.Deck[0].CardValue;
                    this.Deck.RemoveAt(0);
                }

                //Add temporary Lists to Players
                for (int i = 0; i < this.Players.Count; i++)
                {
                    if (playerSum[i] == 21)
                    {
                        players[i].Status = Player.StatusE.BlackJack;
                    }
                    players[i].Hand.Add(tempPlayer[i]);
                }

                //Add temporary List to dealer
                dealer.Hand = tempDealer;
            }
        }
        public int HandSum()
        {
            int temp = 0;

            int sum = 0;
            for (int i = 0; i < this.Hand.Count; i++)
            {
                sum += this.Hand[i].CardValue;
            }
            temp = sum;
            return temp;
        }
        //Hit function
        public void Hit(Player player)
        {
            //If player has handSum of over 21 he automatically loses
            if (player.HandSum()[0] + this.Deck[0].CardValue > 21)
            {
                player.Status = Player.StatusE.Lost;
            }


            for (int i = 0; i < player.Hand.Count; i++)
            {
                //Checking for ace 
                if (this.Deck[0].CardValue == 11 && (player.HandSum()[i] + 11) > 21)
                {
                    this.Deck[0].CardValue = 1;
                    player.Hand[i].Add(Deck[0]);
                    this.Deck.RemoveAt(0);
                }
                else
                {
                    player.Hand[i].Add(Deck[0]);
                    this.Deck.RemoveAt(0);
                }
            }
        }
    }
}
