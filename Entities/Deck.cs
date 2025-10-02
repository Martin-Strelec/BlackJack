using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Deck
    {
        //Public Variables
        public List<Card> Cards { get; private set; }
        public int DeckCount { get; private set; }

        //Private Variables
        private int DeckLayout;
        private static Random rng;

        //Private methods
        //Initializes size of the deck
        private void InitDeckCount()
        {
            this.DeckCount = 4 * DeckLayout * Enum.GetNames(typeof(Card.CardSignE)).Length;
        }
        //Initializes the whole deck
        private void InitDeck()
        {
            Cards = new List<Card>();
            Card c = new Card();

            for (int i = 0; i < DeckLayout; i++)
            {
                for (int j = 0; j < Enum.GetNames(typeof(Card.CardSignE)).Length; j++)
                {
                    for (int k = 0; k < Enum.GetNames(typeof(Card.CardPictogramE)).Length; k++)
                    {
                        Card temp = new Card()
                        {
                            CardPictogram = (Card.CardPictogramE)k,
                            CardValue = c.CardSetValues[j],
                            CardSign = (Card.CardSignE)j
                        };
                        Cards.Add(temp);
                    } 
                }
            }
        }
        //Method used for shuffling cards
        private List<Card> ShuffleDeck(List<Card> shuffledDeck)
        {
            //Using Fisher and Yate's Algorithm for shuffling cards
            rng = new Random();

            for (int i = Cards.Count - 1; i > 0; i--)
            {
                int k = rng.Next(i+1);
                var value = shuffledDeck[k];
                shuffledDeck[k] = shuffledDeck[i];
                shuffledDeck[i] = value;
            }
            return shuffledDeck;
        }

        public Deck(int deckLayout)
        {
            this.DeckLayout = deckLayout;
            InitDeckCount(); //Calculating number of cards in the deck
            InitDeck(); //Initializing deck with cards
            ShuffleDeck(Cards); //Shuffling cards
        }
    }
}
