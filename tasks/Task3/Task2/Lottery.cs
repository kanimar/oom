using System;
using System.Collections.Generic; //ua für die Liste
using System.Linq; //ua für range

namespace Task2
{
    interface IGambling
    {
        string Name { get; }

        void get_instructions();
        List<int> generate_rand();
        void print_list<T>(IEnumerable<T> list, string heading);
        //void check_winnings();
    }

    class Scratch_card : IGambling
    {
        private int[] sums = new int[] { 2, 4, 10, 20, 50, 100, 500, 2000, 10000, 100000 };
        private int drawing = 9;

        //constructor
        public Scratch_card(string gname)
        {
            Name=gname;
        }

        //getter
        public string Name { get; }

        public void get_instructions()
        {
            string output="In the lottery ticket "+ Name +" you will scratch "+ drawing +
                " sums. If a specific sum appears 3 time, you have won this sum once.\n";
            Console.WriteLine(output);
        }

        public List<int> generate_rand()
        {
            var rand = new Random();
            List<int> rand_list = new List<int>();

            for (int i = 0; i < drawing; i++)
            {
                var index = rand.Next(0, sums.Length);
                rand_list.Add(sums[index]);
            }
            return rand_list;
        }

        //ausgeben in 3er Gruppen
        public void print_list<T>(IEnumerable<T> list, string heading)
        {
            Console.Write($"{heading}:\n");
            for (var i = 0; i < list.Count(); i++)
            {
                Console.Write(list.ElementAt(i));
                if ((i + 1) % 3 == 0)
                {
                    Console.Write("\n");
                }
                else { Console.Write("\t"); }
            }
            Console.WriteLine("\n");
        }
    }


    class Lottery : IGambling
    {
        //fields
        private byte main_balls, bonus_balls;
        private byte main_to_draw, bonus_to_draw;
        List<string> is_gameday = new List<string>();
        private enum daysofweek { Montag = 1, Dienstag, Mittwoch, Donnerstag, Freitag, Samstag, Sonntag }

        //constructor
        /// <summary>
        /// Create a new lottery game
        /// </summary>
        /// <param name="name">name of lottery game</param>
        /// <param name="amnt_main_balls">The total amount of main balls in-game. (It must not be 0)</param>
        /// <param name="amnt_bonus_balls">The total amount of bonus balls in-game. (If there are no bonus balls, please enter 0)</param>
        /// <param name="main_balls_to_draw">Number of main balls to be drawn. (It must be smaller than the total amount of balls in-game)</param>
        /// <param name="bonus_balls_to_draw">Number of bonus balls to be drawn.</param>

        public Lottery(string name, byte amnt_main_balls, byte amnt_bonus_balls,
           byte main_balls_to_draw, byte bonus_balls_to_draw)
        {
            //error handling
            if (name.Length < 3) throw new ArgumentException("Invalid lottery name");
            if (amnt_main_balls == 0 || main_balls_to_draw == 0) throw new ArgumentException("Lottery with no main balls is not allowed");
            if (amnt_main_balls <= main_balls_to_draw) throw new ArgumentException("Amount of the main balls to be drawn must not be greater or equal to the total amount of main balls");
            if (amnt_bonus_balls > 0 && amnt_bonus_balls <= bonus_balls_to_draw) throw new ArgumentException("Amount of the bonus balls to be drawn must not be greater or equal to the total amount of main balls");

            //assignments
            Name = name;
            main_balls = amnt_main_balls;
            bonus_balls = amnt_bonus_balls;
            main_to_draw = main_balls_to_draw;
            bonus_to_draw = bonus_balls_to_draw;
        }

        //properties: getter
        public List<string> gamedays { get { return is_gameday; } }
        public string Name { get; }

        //functions
        Func<List<int>, byte, byte, List<int>> iter = iterate;

        //methods

        private string day_to_str(int day)
        {
            return Enum.GetName(typeof(daysofweek), day);
        }

        /// <summary>
        /// Assign a lottery game a day/days, when the balls are drawn
        /// </summary>
        /// <param name="values">Montag=1, Sonntag=7</param>
        public List<string> set_gamedays(params int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (!Enumerable.Range(1, 7).Contains(values[i]))
                {
                    throw new ArgumentOutOfRangeException("The value should be between 1 (Monday) and 7 (Sunday)");
                }
                is_gameday.Add(day_to_str(values[i]));
            }
            is_gameday.Sort();
            return is_gameday;
        }

        /// <summary>
        /// Print the days to the console on which a game takes place
        /// </summary>
        public void print()
        {
            Console.WriteLine($"Spieltage {Name}:");
            foreach (var gd in gamedays)
            {
                Console.WriteLine(gd);
            }
            Console.WriteLine("\n");
        }

        /// <summary>
        /// How a game is played?
        /// </summary>
        /// <returns></returns>
        public void get_instructions()
        {
            string is_bonus = "";
            switch (bonus_to_draw)
            {
                case 0:
                    is_bonus = " There are no bonus numbers.";
                    break;
                case 1:
                    is_bonus = " There is also a pool with bonus numbers between 1 and " + bonus_balls +
                " from that only one ball will be drawn.";
                    break;
                default:
                    is_bonus = " There is also a pool with bonus numbers between 1 and " + bonus_balls +
                " from that exactly " + bonus_to_draw + " balls will be drawn";
                    break;
            }

            string output= Name + " is a game of chance where " + main_to_draw +
                " numbers in range of 1 to " + main_balls + " will be drawn." +
               is_bonus + ".\n";
            Console.WriteLine(output);
        }

        public static List<int> iterate(List<int> number_list, byte total, byte draw)
        {
            Random rd = new Random();
            for (uint i = 1; i <= draw; i++)
            {
                int rand_nr = (int)rd.Next(1, total);
                while (number_list.Contains(rand_nr))
                {
                    rand_nr = (int)rd.Next(1, total);
                }
                number_list.Add(rand_nr);
            }
            number_list.Sort(number_list.Count-draw, draw, Comparer<int>.Default);
            return number_list;
        }


        /// <summary>
        /// generate random numbers for a game (quick tip)
        /// </summary>
        /// <returns></returns>
        public List<int> generate_rand()
        {
            List<int> number_list = new List<int>();
            iter(number_list, main_balls, main_to_draw);
            iter(number_list, bonus_balls, bonus_to_draw);
            return number_list;

        }

        public void print_list<T>(IEnumerable<T> list, string heading)
        {
            foreach (var item in list)
            {
                Console.Write($"{item} \t"); // Replace this with your version of printing
            }
            Console.WriteLine("\n");
        }
    }
}
