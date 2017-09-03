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

            //[Theory]
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
            public void Z0010_ToSorterMetas_should_return_expected(string sorterMetaString)
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
            public void Z0110_orderBy_propertyName_eq_Surname_should_not_except(
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
                var personsSortedQueryable =
                    persons
                    .OrderBy("Surname");

                var personsSorted =
                    personsSortedQueryable
                    .ToList();

                // Assert

                // todo : put in some proper test!

                // Dbg Print
                this.output.WriteLine($"{DateTime.Now} ; N: {personsSorted.Count()} ; numberPerGroup: {numberPerGroup} ; ");
                if (debugPrint)
                {
                    persons.ToList().ForEach(x =>
                        this.output.WriteLine($"{x.Surname} ; {x.Forename} ; Age: {x.Age:D2} ; { (x.Alive ? "True " : "false") } ; p: {x.Prob:f2} ; {x.Salary:C} ; {x.DOB:yyyy/MM/dd hh:mm:ss} ; {x.Address.PostCode} ")
                    );
                }
            }

            //[Theory]
            //[InlineData(true, 15, 3, "Surname, Forename , Age , Address.PostCode ")]
            //[InlineData(true, 15, 3, "Surname desc , Forename desc , Age , Address.PostCode ")]
            //[InlineData(false, 10, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            //[InlineData(false, 20, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            //[InlineData(false, 50, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            ////[InlineData(false, 100, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            //[InlineData(false, 1_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            //[InlineData(false, 10_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            //[InlineData(false, 50_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            //[InlineData(false, 100_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
            //[InlineData(true, 20, 3, "Address.PostCode desc, Surname desc, Forename desc ")]
            //[InlineData(true, 20, 3, "Salary desc")]
            [InlineData(true, 30, 4, "Age des")]
            public void A0210_sortit_by_a_sorterMetaString_should_be_timely_and_not_except_and_should_yield_correct_result(
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
                var sorterMetas =
                    ToSorterMetas(sorterMetaString)
                    .ToList();

                // Act

                // sut call
                var personsSorted =
                    persons
                    .Sortit(sorterMetas)
                    .ToList();

                // Assert

                // todo : put in some proper test!

                // Dbg Print
                this.output.WriteLine($"{DateTime.Now} ; N: {personsSorted.Count()} ; numberPerGroup: {numberPerGroup} ; ");
                if (debugPrint)
                {
                    personsSorted.ToList().ForEach(x =>
                        this.output.WriteLine($"{x.Surname} ; {x.Forename} ; Age: {x.Age:D2} ; { (x.Alive ? "TRUE " : "false") } ; p: {x.Prob:f2} ; {x.Salary:C} ; {x.DOB:yyyy/MM/dd hh:mm:ss} ; {x.Address.PostCode} ")
                    );
                }
            }
        }
    }
}