using ExprTree.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sortit.UnitTest
{
    public static class SortitTestables
    {
        public static List<Person> GetTestablePersons(
            int n = 10,
            int numberPerGroup = 3
        )
        {
            //
            var rnd = new Random(1);

            // Return
            return
                Enumerable
                    .Range(1, n)
                    .Select(x =>
                        new Person
                        {
                            Surname = $"SMITH-{rnd.Next(numberPerGroup).ToString()}",
                            Forename = $"john{rnd.Next(numberPerGroup).ToString()}",
                            Age = rnd.Next(100),
                            Alive = rnd.NextDouble() < 0.5 ? true : false,
                            Prob = rnd.NextDouble(),
                            Salary = (Decimal)rnd.NextDouble(),
                            DOB = DateTime.Now.AddHours(-1 * rnd.NextDouble() * 300),
                            Address = new Address() { PostCode = "PC" + rnd.Next(3).ToString().ToUpper() }
                        }
                    )
                    .ToList();
        }
    }
}