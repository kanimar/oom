using System;
using static System.Console;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace Task2
{
    public class AsyncLottoWinNrs
    {
        public static IEnumerable<byte> RandomNr()
        {
            for (int i = 1; i <= 7; i++)
            {
                yield return (byte)new Random().Next(1, 45);
            }
        }

        public static void Run()
        {
            foreach (var x in RandomNr())
            {
                Task.Delay(TimeSpan.FromSeconds(new Random().Next(7))).Wait();
                Task.Run(() =>
                {
                    WriteLine($"checking if {x} is winning number");
                    Task.Delay(TimeSpan.FromSeconds(new Random().Next(15))).Wait();
                    check_data(x);
                });
            }
        }

        public static async void check_data(byte x)
        {
            WriteLine($"Online check for {x} processing...");

            List<byte> liste = await get_winning_nrs();

            if (liste.Contains(x))
            {
                WriteLine($"{x} is winning number!");
            }
            else
            {
                WriteLine($"Unfortunately {x} hasn't won");
            }
        }

        public static async Task<List<byte>> get_winning_nrs()
        {
            Task.Delay(TimeSpan.FromSeconds(new Random().Next(5))).Wait();
            List<byte> list = new List<byte>();
            Uri uri = new Uri("http://win2day.at/gaming/LO_hp.jsp");
            var Client = new HttpClient();
            string data = await Client.GetStringAsync((uri));

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
