using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Task2
{
    class Json
    {
        public static void Serialization(IGambling[] games)
        {
            var fn = Path.Combine(Environment.CurrentDirectory, "objects.json");

            var init_array = new JsonSerializerSettings() { Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.Auto };
            string json = JsonConvert.SerializeObject(games, init_array);
            File.WriteAllText(fn, json);

            var fnstr = File.ReadAllText(fn);
            var list = JsonConvert.DeserializeObject<IGambling[]>(fnstr, init_array);
            Console.WriteLine("Games to play:");
            foreach (var l in list) Console.WriteLine($"{l.Name}");
        }
    }
}
