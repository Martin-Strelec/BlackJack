using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Card
    {
        //Public Variables
        public int CardValue { get; set; }
        public CardSignE CardSign { get; set; }
        public CardPictogramE CardPictogram { get; set; }
        public enum CardSignE
        {
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }
        public enum CardPictogramE
        {
            Hearts,
            Spades,
            Diamonds,
            Clubs
        }
        public int[] CardSetValues = { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
        public Card()
        {
        }
    }
}
