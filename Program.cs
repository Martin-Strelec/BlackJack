/*
 * Name: 
 * Author: M.Strelec
 * Date:
 * Purpose: 
 */


namespace BlackJack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; //Console 
            //Processing
            Game game = new Game();
            game.Run();
            Console.WriteLine("\n******End of program******\n");
        }
    }
}