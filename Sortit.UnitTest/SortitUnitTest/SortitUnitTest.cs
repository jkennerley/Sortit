using System;
using System.Linq;
using SortIt;
using Xunit;
using Xunit.Abstractions;

//using static Sortit.SortitExtensions;
//using static Sortit.;

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
                var sorterMetas = SortitExtensions.ToSorterMetas(sorterMetaString);

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

            //[Theory]
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
                    .SortItOrderBy("Surname");

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

            [Theory]
            //[InlineData(true, 7, 7, "Surname")]      //      1ms

            //[InlineData(true, 7, 7, "Town.Name")]
            //[InlineData(true, 7, 7, "Town.Name desc")]

            //[InlineData(true, 7, 7, "Town.County.Name")]
            //[InlineData(true, 7, 7, "Town.County.Name desc")]

            //[InlineData(true, 7, 7, "Town.County.Name desc, Surname ")]
            //[InlineData(true, 7, 7, "Town.County.Name desc, Surname desc")]


            //[InlineData(true, 7, 7, "Town.County.Country.Name")]
            [InlineData(true, 7, 7, "Town.County.Country.Name desc")]

            //[InlineData(true, 7, 7, "Town.County.Name")]
            //[InlineData(true, 7, 7, "Town.County.Name desc")]

            //[InlineData(true, 7, 7, "Town.County.Name")]
            //[InlineData(true, 7, 7, "Town.County.Name desc")]

            //[InlineData(true, 7, 7, "Town.County.Country.Name desc")]

            //[InlineData(false, 100, 3, "Surname")]     //      2ms
            //[InlineData(false, 1000, 3, "Surname")]    //      7ms 
            //[InlineData(false, 10_000, 3, "Surname")]  //    226ms
            //[InlineData(false, 100_000, 3, "Surname")] //    852ms

            //[InlineData(false, 15, 3, "Surname")]      //      1ms
            //[InlineData(false, 100, 3, "Surname")]     //      2ms
            //[InlineData(false, 1000, 3, "Surname")]    //      7ms 
            //[InlineData(false, 10_000, 3, "Surname")]  //    226ms
            //[InlineData(false, 100_000, 3, "Surname")] //    852ms
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
            //[InlineData(true, 30, 4, "Age desc")]

            public void Sortit_by_sorterMetaString_should_be_timely(
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
                    SortitExtensions.ToSorterMetas(sorterMetaString)
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
                        //this.output.WriteLine($"{x.Surname} ; {x.Forename} ; Age: {x.Age:D2} ; { (x.Alive ? "TRUE " : "false") } ; p: {x.Prob:f2} ; {x.Salary:C} ; {x.DOB:yyyy/MM/dd hh:mm:ss} ; {x.Address.PostCode} ; {x?.Town?.Name} ; {x?.Town?.County.Name} ; {x?.Town?.County.Country.Name} ")
                        this.output.WriteLine($"{x.Surname} ; {x.Forename} ; Age: {x.Age:D2} ; { (x.Alive ? "TRUE " : "false") } ; p: {x.Prob:f2} ; {x.Salary:C} ; {x.DOB:yyyy/MM/dd hh:mm:ss} ; {x.Address.PostCode} ; {x?.Town?.Name} ; {x?.Town?.County?.Name} ; {x?.Town?.County?.Country?.Name} ")
                    );
                }
            }
        }
    }
}