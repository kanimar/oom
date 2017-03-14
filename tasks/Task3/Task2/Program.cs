using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var game1 = new Lottery("Lotto", 45, 45, 6, 1);
                var game2 = new Lottery("Euromillions", 50, 12, 5, 2);
                var game3 = new Lottery("Joker", 90, 0, 5, 0);
                var game4 = new Scratch_card("Cash");

                IGambling[] games = new IGambling[] { game1,game2,game3,game4};

               foreach(var g in games)
                {
                    g.get_instructions();
                    g.print_list(g.generate_rand(),g.Name);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
