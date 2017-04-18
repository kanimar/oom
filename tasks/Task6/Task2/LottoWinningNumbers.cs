using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Http;
using static System.Console;

namespace Task2
{
    public class LottoWinningNumbers
    {
        public static IEnumerable<byte> RandomNr()
        {
            var rand = new Random();

            for (int i = 1; i <= 7; i++)
            {
                yield return (byte)rand.Next(1, 45);
            }
        }

        public static void Run()
        {
            var tasks = new List<Task<bool>>();

            foreach(var x in RandomNr())
            {
                Task.Delay(TimeSpan.FromSeconds(new Random().Next(5))).Wait();
                Task.Run(() =>
                {
                    WriteLine($"checking if {x} is winning number");
                    check_data(x);
                    Task.Delay(TimeSpan.FromSeconds(new Random().Next(15))).Wait();
                    return true;
                });

                //tasks.Add(task);
            }
        }

        public static async void check_data(byte x)
         {
            WriteLine($"Online check for {x} processing...");

            List<byte> liste = await get_winning_nrs();
            if(liste.Contains(x))
            WriteLine($"{x} is winning number!");
            else
            WriteLine($"Unfortunately {x} hasn't won");
        }

        public static async Task<List<byte>> get_winning_nrs()
        {
            Task.Delay(TimeSpan.FromSeconds(new Random().Next(5))).Wait();
            List<byte> list = new List<byte>();
            Uri uri = new Uri("http://win2day.at/gaming/LO_hp.jsp");
            var Client=new HttpClient();
            string data = await Client.GetStringAsync((uri));

            //string data= System.IO.File.ReadAllText(@"C:\Users\Maria\Desktop\Test.html");

            int start = data.IndexOf("<div id=\"LO_Kugeln\"");
            int end = data.LastIndexOf("border=\"0\" /></div></td></tr>");
            int length = end - start + 1;
            data = data.Substring(start, length);
            foreach (Match match in Regex.Matches(data, "alt=\"Z?Z?:?([0-9]+)\""))
            {
                list.Add(Convert.ToByte(match.Groups[1].Value));
            }
            return list;
        }
        
    }
}
