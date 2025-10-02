/*
*Made by Martin Strelec
*This class is used to validate specific data types. Work in progress..
*Fucntions: Test for data type > string, int, double, decimal
*SHORTEND VERSION OF VALIDATION.cs CLASS
*/

using System.ComponentModel;

namespace BlackJack
{
    class Validation
    {

        /*
         * MODIFYING METHODS
         */

        //Modifies ints. Modifiers: Range, Prompt message
        public static int ModifyIntRange(int init, int end, string message)
        {
            int result;
            Console.Write($"{message}: ");

            if (init == 0 && end == 0)
            {
                while (!int.TryParse(Console.ReadLine(), out result))
                {
                    Console.WriteLine("Invalid Input!");
                    Console.Write("> ");
                }
                return result;
            }
            else
            {
                while (!int.TryParse(Console.ReadLine(), out result) && !(result <= init && result >= end))
                {
                    Console.WriteLine("Invalid Input!");
                    Console.Write("> ");
                }
                return result;
            }
        }
    }
}

