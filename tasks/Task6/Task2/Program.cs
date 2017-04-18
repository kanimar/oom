using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Collections.Generic; //ua für die Liste
using System.Linq; //ua für range

namespace Task2
{
    public static class Program
    {
        static void Main(string[] args)
        {
            // var games = new IGambling[]
            //{
            //         new Lottery("Lotto", 45, 45, 6, 1),
            //         new Lottery("Euromillions", 50, 12, 5, 2),
            //         new Lottery("Joker", 90, 0, 5, 0),
            //         new Scratch_card("Cash"),
            //         new Scratch_card("Schatztruhe")
            //  };

            // foreach (var g in games)
            // {
            //     g.get_instructions();
            //     g.print_list(g.generate_rand(), g.Name);
            // }

            //var sc = new Scratch_card("Lebenlang");
            //sc.play();
            //sc.play();
            //sc.play();

            //Json.Serialization(games);


            //Lesson 6
            Window.Run();
            //AsyncLottoWinNrs.Run();
            
        }
    }
}
