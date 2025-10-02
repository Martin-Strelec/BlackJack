using BlackJack.Entities;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlackJack
{
    class Gui
    {
        //Input methods
        public static void GetInput(string str)
        {
            str = $"{str}: ";

            Console.Write(str);
        }
        public static int GetInputInt(string message, bool allowNulls)
        {
            try
            {
                int input;
                Gui.GetInput(message);
                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    return allowNulls ? -1 : throw new FormatException();
                }
                else
                {
                    return input;
                }
            }
            catch (FormatException e)
            {
                Console.Clear();
                Console.WriteLine("Nothing entered!");
                Gui.PressKeyToContinue();
                return -1;
            }

        }
        //Announcments
        public static void PressKeyToContinue()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("==> Press any key to continue");
            Console.ReadKey();
        }
        public static void Title(string str)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            str = $"==== {str} ====";

            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + (str.Length / 2)) + "}\n", str));
            Console.ResetColor();
        }
        public static void MenuTitle(string str)
        {

            Console.ForegroundColor = ConsoleColor.Green;
            str = $"=== {str} ===";

            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + (str.Length / 2)) + "}\n", str));
            Console.ResetColor();
        }
        public static void MenuOption(params string[] str)
        {
            for (int i = 1; i <= str.Length; i++)
            {
                Console.Write($"> ({i}): {str[i - 1]}\n");
            }
        }
        public static string InputOption(params string[] str)
        {
            string temp = "";
            for (int i = 0; i < str.Length; i++)
            {
                temp += $" {str[i]} /";
            }
            return temp;
        }
        public static void Announcment(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            str = $"\n(~) {str}\n";

            Console.Write(str);
            Console.ResetColor();
        }
        public static void GeneralText(string str)
        {
            str = $"* {str}\n";

            Console.Write(str);
        }
        public static string CenterText(string str, int centeringElementWidth)
        {
            string formattedString = String.Format("{0," + ((centeringElementWidth / 2) + (str.Length / 2)) + "}\n", str);
            return formattedString;
        }
        //Main Game GUI
        public static void DrawGameGui(Player player,int playerID, Dealer dealer)
        {
            Gui.Title($"Player{playerID+1} Turn");
            Console.Write("\n\n\n\n");
            
            //Drawing Dealer's Hand
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(CenterText("*************************", Console.WindowWidth));
            Console.Write(CenterText("* === Dealer's hand === *", Console.WindowWidth));
            Console.Write(CenterText("*************************", Console.WindowWidth));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(CenterText($"{dealer.Hand[0].CardSign} of {dealer.Hand[0].CardPictogram}", Console.WindowWidth));
            Console.Write(CenterText("------- Hidden ------", Console.WindowWidth));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(CenterText($"Sum: {dealer.Hand[0].CardValue}", Console.WindowWidth));
            Console.ResetColor();

            Console.Write("\n\n\n\n");

            //Drawing Player's Hand
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(CenterText("*************************", Console.WindowWidth));
            Console.Write(CenterText("* === Player's hand === *", Console.WindowWidth));
            Console.Write(CenterText("*************************", Console.WindowWidth));

            //Changing the color of the text based on the status of the player
            if (player.HandSum()[0] >= 21)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                if (player.HandSum()[0] > 21)
                {
                    Console.Write(CenterText($"BUSTED", Console.WindowWidth));
                }
                else if (player.HandSum()[0] == 21 && (Player.StatusE.BlackJack == player.Status))
                {
                    Console.Write(CenterText($"BLACKJACK", Console.WindowWidth));
                }
                Console.ResetColor();
            }

            Console.ResetColor();

            //Displaying the hand of the player
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < player.Hand[0].Count; i++)
            {
                Console.Write(CenterText($"{player.Hand[0][i].CardSign} of {player.Hand[0][i].CardPictogram}", Console.WindowWidth));
            }
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            //Displaying the sum for all hands
            for (int i = 0; i < player.Hand.Count; i++)
            {
                Console.Write(CenterText($"Sum: {player.HandSum()[0]}", Console.WindowWidth));
            }
            Console.ResetColor();
        }
        public static void DrawResults(Player player, int playerID, Dealer dealer)
        {
            const string OUTPUT_TAB = "{0,-30}{1,-30}";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(OUTPUT_TAB, $"Player{playerID}", "");
            Console.WriteLine(OUTPUT_TAB, "**************************", "*************************");
            Console.WriteLine(OUTPUT_TAB, $"=== Player{playerID}'s hand ===", "=== Dealer's Hand ===");
            Console.WriteLine(OUTPUT_TAB, "", "");

            for (int i = 0; player.Hand[0].Count > dealer.Hand.Count ? i < player.Hand[0].Count : i < dealer.Hand.Count; i++)
            {
                Console.WriteLine(OUTPUT_TAB, player.Hand[0].Count > i ? $"{player.Hand[0][i].CardSign} of {player.Hand[0][i].CardPictogram}" : "----", dealer.Hand.Count > i ? $"{dealer.Hand[i].CardSign} of {dealer.Hand[i].CardPictogram}" : "---");
            }
            Console.WriteLine(OUTPUT_TAB, "", "");
            Console.WriteLine(OUTPUT_TAB, $"-------------------------", "-------------------------");
            Console.WriteLine(OUTPUT_TAB, $"Result: {player.Status}", $"Result: {dealer.Status}");
            Console.WriteLine(OUTPUT_TAB, $"Bet: {player.Bet:c}", "");
            Console.WriteLine(OUTPUT_TAB, $"Budget: {player.Budget:c}", "");
            Console.WriteLine(OUTPUT_TAB, "**************************", "*************************");
        }
    }
}