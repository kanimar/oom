using System;
using System.Collections.Generic; //für die Liste
using System.Linq; //für range

namespace Task2
{
    class Lottery
    {
        //fields
        private string name;
        private byte main_balls, bonus_balls;
        private byte main_to_draw, bonus_to_draw;
        List<string> is_gameday = new List<string>();
        private enum daysofweek { Montag=1, Dienstag, Mittwoch, Donnerstag, Freitag, Samstag, Sonntag}

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
            this.name = name;
            main_balls = amnt_main_balls;
            bonus_balls = amnt_bonus_balls;
            main_to_draw = main_balls_to_draw;
            bonus_to_draw = bonus_balls_to_draw;
        }

        //properties: getter
        public List<string> gamedays { get { return is_gameday; } }
        public string Name { get { return name; } }

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
        /// generate random numbers for a game (quick tip)
        /// </summary>
        /// <returns></returns>
        public List<byte> generate_numbers()
        {
            List<byte> number_list = new List<byte>();

            Random rd = new Random();
            for (uint i = 1; i <= main_to_draw; i++)
            {
                byte rand_nr = (byte)rd.Next(1, main_balls);
                while (number_list.Contains(rand_nr))
                {
                    rand_nr = (byte)rd.Next(1, main_balls);
                }
                number_list.Add(rand_nr);
            }
            number_list.Sort();
            return number_list;
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
            Console.Write("\n");
        }

        /// <summary>
        /// How a game is played?
        /// </summary>
        /// <returns></returns>
        public string get_instructions()
        {
           string is_bonus="";
            switch (bonus_balls)
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
                " from that exactly " + bonus_balls + " balls will be drawn";
                    break;
            }

            return name + " is a game of chance where " + main_to_draw +
                " numbers in range of 1 to " + main_balls + " will be drawn." +
               is_bonus+".\n";
        }
    }
}
