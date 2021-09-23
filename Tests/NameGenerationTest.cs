using FluentAssertions;
using NUnit.Framework;
using Snakes.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    internal class NameGenerationTest
    {
        private class PartialComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }

        [Test]
        public void UniqNamesTest()
        {
            List<string> generatedNamesList = new List<string>();
            int namesCount = 1000;
            NameGenerator nameGenerator = new NameGenerator();
            for (int i = 0; i < namesCount; i++)
            {
                generatedNamesList.Add(nameGenerator.GenerateNext());
            }
            List<string> uniqValuesList = generatedNamesList.Distinct(new PartialComparer()).ToList();
            uniqValuesList.Count.Should().Be(namesCount);
        }
    }
}
