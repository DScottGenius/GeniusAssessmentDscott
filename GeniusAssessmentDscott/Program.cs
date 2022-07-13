using GeniusAssessmentDscott.Menus;
using System;

namespace GeniusAssessmentDscott
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please select a menu by typing the corresponding number \n '1' - User \n '2' - Payments \n '3' - Exit program");

                string input = Console.ReadLine();

                if (input.Trim() == "3")
                {
                    break;
                }
                else if (input == "1" || input == "2")
                {
                    SelectMenu(input);
                }
                else
                {
                    Console.WriteLine("Please enter a valid option");
                }
            }
        }

        public static Menu SelectMenu(string input)
        {
            switch (input)
            {
                case "1":
                    return new UserMenu();
                case "2":
                    return new PaymentsMenu();
                default:
                    throw new Exception();
            }
        }
    }
}
