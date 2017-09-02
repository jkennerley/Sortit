using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

using static Sortit.SortitExtensions;
using static Sortit.UnitTest.SortitTestables;

namespace Sortit.UnitTest
{
    public class SortitUnitTest
    {
        private ITestOutputHelper output;

        public SortitUnitTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        public class ToSortitMetas
        {
            private ITestOutputHelper output;

            public ToSortitMetas(ITestOutputHelper output)
            {
                this.output = output;
            }

            [Theory]
            [InlineData("Surname")]
            [InlineData("Surname asc")]
            [InlineData("Surname desc")]
            [InlineData("Surname,Forename")]
            [InlineData("Surname,Forename")]
            [InlineData("Surname, Forename    ")]
            [InlineData("         Surname         , Forename    ")]
            [InlineData("Surname desc, Forename")]
            [InlineData("Surname desc, Forename desc")]
            [InlineData("Surname desc, Forename desc, Age desc")]
            [InlineData("Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            public void ToSorterMetas_should_return_expected(string sorterMetaString)
            {
                // Arrange

                // Act

                // sut call
                var sorterMetas = ToSorterMetas(sorterMetaString);

                // Assert

                // todo : put in some proper test!

                // Dbg Print
                this.output.WriteLine($"{DateTime.Now}  ");
                this.output.WriteLine($"{sorterMetaString} ");
                sorterMetas.ToList().ForEach(x =>
                    this.output.WriteLine($"  { x.PropertyName} ; {x.Desc}")
                );
            }
        }

        public class Sortit
        {
            private ITestOutputHelper output;

            public Sortit(ITestOutputHelper output)
            {
                this.output = output;
            }

            [Theory]
            [InlineData(true, 9, 4)]
            public void orderBy_propertyName_eq_Surname_should_not_except(
                bool debugPrint,
                int n,
                int numberPerGroup)
            {
                // Arrange
                var persons =
                    GetTestablePersons(n, numberPerGroup)
                    .AsQueryable();

                // Act

                // sut call
                var personsSorted =
                    persons
                    .OrderBy("Surname");

                // Assert

                // Dbg Print
                this.output.WriteLine($"{DateTime.Now} ; N: {personsSorted.Count()} ; numberPerGroup: {numberPerGroup} ; ");
                if (debugPrint)
                {
                    persons.ToList().ForEach(x =>
                        this.output.WriteLine($"{x.Surname} ; {x.Forename} ; Age: {x.Age:D2} ; { (x.Alive ? "True " : "false") } ; p: {x.Prob:f2} ; {x.Salary:C} ; {x.DOB:yyyy/MM/dd hh:mm:ss} ; {x.Address.PostCode} ")
                    );
                }
            }

            [Theory]
            [InlineData(true, 15, 3, "Surname, Forename , Age , Address.PostCode ")]
            [InlineData(true, 15, 3, "Surname desc , Forename desc , Age , Address.PostCode ")]
            [InlineData(false, 10, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            [InlineData(false, 20, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            [InlineData(false, 50, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            [InlineData(false, 100, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            [InlineData(false, 1_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            [InlineData(false, 10_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            [InlineData(false, 50_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            [InlineData(false, 100_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            public void sortit_by_by_sorter_meta_string_should_be_timely(
                bool debugPrint,
                int n,
                int numberPerGroup,
                string sorterMetaString
            )
            {
                // Arrange

                // AsQueryable
                var persons =
                    GetTestablePersons(n, numberPerGroup)
                    .AsQueryable();

                // e.g "Surname, Address, PostCode desc" ->
                var sorterMetas = ToSorterMetas(sorterMetaString);

                // Act

                // sut call
                var personsSorted =
                    persons
                    .Sortit(sorterMetas)
                    .ToList();

                // Assert

                // todo ; tests!
                // ...

                // Dbg Print
                this.output.WriteLine($"{DateTime.Now} ; N: {personsSorted.Count()} ; numberPerGroup: {numberPerGroup} ; ");
                if (debugPrint)
                {
                    persons.ToList().ForEach(x =>
                        this.output.WriteLine($"{x.Surname} ; {x.Forename} ; Age: {x.Age:D2} ; { (x.Alive ? "True " : "false") } ; p: {x.Prob:f2} ; {x.Salary:C} ; {x.DOB:yyyy/MM/dd hh:mm:ss} ; {x.Address.PostCode} ")
                    );
                }
            }
        }
    }
}