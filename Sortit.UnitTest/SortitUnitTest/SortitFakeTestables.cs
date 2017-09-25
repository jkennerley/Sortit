using ExprTree.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sortit.UnitTest
{
    public static class SortitTestables
    {
        public static Town GetATown(
            int n,
            int numberPerGroup,
            Random rnd,
            bool getTown = true,
            bool getCounty = true,
            bool getCountry = true
        )
        {

            Country country =null;
            if (getCountry)
            {
                country = new Country
                {
                    Id = Guid.NewGuid(),
                    Name = "UK-" + rnd.Next(numberPerGroup).ToString().ToUpper(),
                };
            }

            County county = null;
            if (getCounty)
            {
                county = new County
                {
                    Id = Guid.NewGuid(),
                    Name = "Lancs-" + rnd.Next(numberPerGroup).ToString().ToUpper(),
                    Country = getCountry ? country : null
                };
            }

            Town town = null;
            if (getTown)
            {
                town = new Town
                {
                    Id = Guid.NewGuid(),
                    Name = "Preston-" + rnd.Next(numberPerGroup).ToString().ToUpper(),
                    County = getCounty ? county : null
                };
            }


            return town;
        }

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
                            Address = new Address() { PostCode = "PC" + rnd.Next(3).ToString().ToUpper() },
                            //Town = GetATown(n, numberPerGroup, rnd, true, true, true)
                            Town = GetATown(n, numberPerGroup, rnd, (rnd.NextDouble() < 0.5 ? true : false), (rnd.NextDouble() < 0.5 ? true : false), (rnd.NextDouble() < 0.5 ? true : false) )
                            //Town = GetATown()
                        }
                    )
                    .ToList();
        }
    }
}