using Snakes.Services;
using System;
using System.Collections.Generic;

namespace Snakes.Utils
{
    public class NameGenerator : INameGenerator
    {
        private static readonly List<string> names = new List<string>()
        {
            "John", "Bob", "Vasia", "Artem", "Ilya", "Alexandr", "Sanya", "Damir", "Grisha", "Boshy", "Zik", "Klavii", "Tazar"
        };

        private readonly Dictionary<string, int> namesDictionary = new Dictionary<string, int>();

        public string GenerateNext()
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
