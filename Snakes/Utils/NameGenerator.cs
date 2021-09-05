using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.Utils
{
    public static class NameGenerator
    {
        private static List<string> names = new List<string>()
        {
            "John", "Bob", "Vasia", "Artem", "Ilya", "Alexandr", "Sanya", "Damir", "Grisha", "Boshy", "Zik", "Klavii", "Tazar"
        };

        private static Dictionary<string, int> namesDictionary = new Dictionary<string, int>();

        public static string GenerateNext()
        {
            string name = names[new Random().Next(0, names.Count)];
            if (namesDictionary.ContainsKey(name))
            {
                namesDictionary[name] = namesDictionary[name] + 1;
            }
            else
            {
                namesDictionary[name] = 1;
            }

            return name + "[" + namesDictionary[name] + "]";
        }
    }
}
