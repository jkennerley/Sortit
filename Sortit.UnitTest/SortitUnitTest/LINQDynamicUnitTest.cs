using System;
using System.Linq;

using Xunit;
using Xunit.Abstractions;
using static Sortit.UnitTest.SortitTestables;

namespace Sortit.UnitTest
{
    public class LINQDynamicUnitTest
    {
        private ITestOutputHelper output;

        public LINQDynamicUnitTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        public class LINQDynamic
        {
            private ITestOutputHelper output;

            public LINQDynamic(ITestOutputHelper output)
            {
                this.output = output;
            }

            [Theory]
            //[InlineData(false,     15, 4,  "Surname")] //       1ms
            [InlineData(false,    100, 4,  "Surname")] //       1ms
            [InlineData(false,   1000, 4,  "Surname")] //       6ms
            [InlineData(false,  10_000, 4, "Surname")] //     258ms  
            [InlineData(false, 100_000, 4, "Surname")] //     838ms
            public void LINQDynamic_should_be_timely(
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
                //var sorterMetas =
                //    ToSorterMetas(sorterMetaString)
                //        .ToList();

                // Act

                // sut call
                var personsSorted =
                    persons
                    .OrderBy("Surname")
                    .ToList();

                // Assert

                // todo : put in some proper test!

                //// Dbg Print
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
