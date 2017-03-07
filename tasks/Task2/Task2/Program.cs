using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var lotto = new Lottery("Lotto", 45, 45, 6, 1);
                var euromill = new Lottery("Euromillions", 50, 12, 5, 2);
                var joker = new Lottery("Joker", 90, 0, 5, 0);

                lotto.set_gamedays(3, 7);
                euromill.set_gamedays(2, 5);

                lotto.print();
                euromill.print();

                Console.WriteLine(lotto.get_instructions());
                Console.WriteLine(euromill.get_instructions());
                Console.WriteLine(joker.get_instructions());

                var Liste=euromill.generate_numbers();
                Liste.ForEach(i => Console.Write("{0}\t", i));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
