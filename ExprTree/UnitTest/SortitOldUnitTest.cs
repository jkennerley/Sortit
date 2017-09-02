/*
using Sortit;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static ExprTree.UnitTest.SortitTestables;
using static Sortit.SortitExtensions;

namespace ExprTree.UnitTest
{
    public class SortitOldUnitTest
    {
        private ITestOutputHelper output;

        public SortitOldUnitTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        //[Theory]
        //[InlineData(9, 4)]
        public void X0010_orderBy_propertyName_eq_Surname_should_not_except(int N, int numberPerGroup)
        {
            // Arrange
            var persons =
                GetTestablePersons(N, numberPerGroup)
                .AsQueryable();

            // Act

            // sut call
            var personsSorted =
                persons
                .OrderBy("Surname");

            // Dbg Print
            this.output.WriteLine($"{DateTime.Now}  ; N: {personsSorted.Count()} ;  numberPerGroup: {numberPerGroup} ;   ");
            personsSorted.ToList().ForEach(x =>
                this.output.WriteLine($"{x.Surname} ; {x.Forename} : {x.Age} ")
            );

            // Assert
        }

        #region Examples of sortng with string PropertyName sortring ; OrderBy, ThenBy with strings ...

        //[Fact]
        public void CT020_dynamic_sorter_by_Surname_Forename_Age_should_not_except()
        {
            // Arrange
            var persons =
                    GetTestablePersons(9)
                    .AsQueryable()
                ;

            // Act

            // sut call ; order
            var ps =
                    persons
                    .AsQueryable()
                    .OrderBy("Surname")
                    .ThenBy("Forename")
                    .ThenBy("Age")
                    ;

            // Assert

            // Dbg Print
            ps.ToList().ForEach(x =>
                this.output.WriteLine($"{x.Surname} ; {x.Forename} : {x.Age} ")
            );
        }

        //[Fact]
        public void CT030_dynamic_sorter_by_Surname_Forename_desc_should_not_except()
        {
            // Arrange
            var persons =
                GetTestablePersons(9)
                .AsQueryable()
                ;

            // Act

            // sut call ; order
            var ps =
                persons
                .AsQueryable()
                .OrderBy("Surname")
                .ThenByDescending("Forename")
                ;

            // Dbg Print
            ps.ToList().ForEach(x =>
                this.output.WriteLine($"{x.Surname} ; {x.Forename} : {x.Age} ")
            );

            // Assert
        }

        //[Fact]
        public void CT040_dynmaic_sorter_by_Surname_Forename_Age_should_not_except()
        {
            // Arrange
            var persons =
                GetTestablePersons(9)
                .AsQueryable()
                ;

            // Act

            // sut call ; order
            var ps =
                    persons
                    .AsQueryable()
                    .OrderBy("Surname")
                    .ThenBy("Forename")
                    .ThenBy("Age")
                    ;

            // Dbg Print
            ps.ToList().ForEach(x =>
                this.output.WriteLine($"{x.Surname} ; {x.Forename} : {x.Age} ")
            );

            // Assert
        }

        /// [Theory]
        /// //[InlineData(10, 10)]
        /// [InlineData(100, 10)]
        /// //[InlineData(    100,  100)]
        /// [InlineData(  1_000,   10)]
        /// [InlineData(1_000, 100)]
        /// //[InlineData(  1_000, 1000)]
        /// [InlineData(10_000, 10)]
        /// //[InlineData(100_000,  100)]
        /// //[InlineData(100_000, 1000)]
        public void CT140_sorter_of_N_items_and_M_per_group_by_Surname_Forename_Age_should_be_timely(
            int N
            , int numberPerGroup
        )
        {
            // Arrange
            var persons =
                    GetTestablePersons(N, numberPerGroup)
                        .AsQueryable()
                ;

            // Act

            // sut call ; order
            var ps =
                    persons
                        .AsQueryable()
                        .OrderBy("Surname")
                        .ThenBy("Forename")
                        .ThenBy("Age")
                ;

            this.output.WriteLine($"N : {persons.Count()} ; numberPerGroup : {numberPerGroup}");

            // Dbg Print
            ps.Take(10).ToList().ForEach(x =>
                this.output.WriteLine($"{x.Surname} ; {x.Forename} : {x.Age} ")
            );
            this.output.WriteLine($"...");
            ps.Skip(N - 10).Take(10).ToList().ForEach(x =>
                  this.output.WriteLine($"{x.Surname} ; {x.Forename} : {x.Age} ")
            );

            // Assert
        }

        //[Fact]
        //public void cwallis_ex1_should_not_except()
        //{
        //    // Arrange
        //
        //    // Act
        //
        //    //  expresion of :: persons ->  IOrderedEnumerable
        //    Expression<Func<List<Person>, IOrderedEnumerable<Person>>> xpr = (x) => x.OrderBy(q => q.Surname);
        //
        //    MethodCallExpression body = xpr.Body as MethodCallExpression;
        //
        //    LambdaExpression bodyFirstArgument = body.Arguments[1] as LambdaExpression;
        //
        //    MemberExpression bodyFirstArgumentMember = bodyFirstArgument.Body as MemberExpression;
        //
        //    // bodyFirstArgumentMember.Member *should* be able to be changed to whatever property we want to order by,
        //    // but I don't know how :(
        //    //bodyFirstArgumentMember.Update(something???)
        //
        //    // Assert
        //    Assert.Equal(0, 0);
        //}

        #endregion Examples of sortng with string PropertyName sortring ; OrderBy, ThenBy with strings ...

        //[Theory]
        //[InlineData(true, 20, 3)]
        public void A0011_sortit_by_Metas_of_Surname_Forename_Age_should_not_except(
            bool debugPrint,
            int N,
            int numberPerGroup
        )
        {
            // Arrange
            var personsTestables =
                GetTestablePersons(N, numberPerGroup);

            // AsQueryable
            var personsTestablesQueryable =
                personsTestables
                .AsQueryable();

            // sort definition ; "Surname, Forename, Age"
            var sorterMetas = new List<SorterMeta>
            {
                new SorterMeta { PropertyName = "Surname"}
                , new SorterMeta { PropertyName = "Forename"}
                , new SorterMeta { PropertyName = "Age"}
            };

            //var sorterMetas = new List<SorterMeta>
            //{
            // new SorterMeta { PropertyName = "Surname", Desc = true }
            //    , new SorterMeta { PropertyName = "Forename"}
            //    , new SorterMeta { PropertyName = "Age"}
            //};

            //var sorterMetas = new List<SorterMeta>
            //{
            //    new SorterMeta { PropertyName = "Surname", Desc = true}
            //    , new SorterMeta { PropertyName = "Forename", Desc = true}
            //    , new SorterMeta { PropertyName = "Age"}
            //};

            //var sorterMetas = new List<SorterMeta>
            //{
            //    new SorterMeta { PropertyName = "Surname", Desc = true}
            //    , new SorterMeta { PropertyName = "Forename", Desc = true}
            //    , new SorterMeta { PropertyName = "Age", Desc = true}
            //};

            // sort definition ; "Surname desc , Forename desc, Age desc"
            //var sorterMetas = new List<SorterMeta>
            //{
            //    new SorterMeta { PropertyName = "Surname", Desc = true  }
            //    , new SorterMeta { PropertyName = "Forename", Desc = true }
            //    , new SorterMeta { PropertyName = "Age", Desc = true }
            //};

            // Act

            // sut call
            var personsSorted =
                personsTestablesQueryable
                .Sortit(sorterMetas);

            // Assert

            // Assert - Dbg Print
            var persons =
                personsSorted
                .ToList();

            // Dbg Print
            this.output.WriteLine($"{DateTime.Now} ; N: {personsSorted.Count()} ; numberPerGroup: {numberPerGroup} ; ");
            if (debugPrint)
            {
                persons.ToList().ForEach(x =>
                    this.output.WriteLine($"{x.Surname} ; {x.Forename} ; {x.Age} ; { (x.Alive ? "T" : "F") } ; p: {x.Prob:f2} ; {x.Salary:C} ; {x.DOB:yyyy/MM/dd hh:mm:ss} ; {x.Address.PostCode} ")
                );
            }
        }

        //[Theory]
        //[InlineData(false, 65_000, 4)] // looks like 65K/sec
        public void T011_sortit_by_Metas_of_Surname_Forename_Age_should_not_except(
            bool debugPrint,
            int N,
            int numberPerGroup
        )
        {
            // Arrange
            var personsTestables =
                GetTestablePersons(N, numberPerGroup);

            // AsQueryable
            var personsTestablesQueryable =
                    personsTestables
                        .AsQueryable()
                ;

            // sort definition ; "Surname, Forename, Age"
            var sorterMetas = new List<SorterMeta>
            {
                new SorterMeta { PropertyName = "Surname"}
                , new SorterMeta { PropertyName = "Forename"}
                , new SorterMeta { PropertyName = "Age"}
            };

            // sort definition ; "Surname desc , Forename desc, Age desc"
            //var sorterMetas = new List<SorterMeta>
            //{
            //    new SorterMeta { PropertyName = "Surname", Desc = true , First = true }
            //    , new SorterMeta { PropertyName = "Forename", Desc = true }
            //    , new SorterMeta { PropertyName = "Age", Desc = true }
            //};

            // Act

            // sut call
            var personsSorted =
                personsTestablesQueryable
                .Sortit(sorterMetas);

            // Assert

            // Assert - Dbg Print
            var persons =
                personsSorted
                    .ToList();

            // Dbg Print
            this.output.WriteLine($"{DateTime.Now} ; N: {personsSorted.Count()} ; numberPerGroup: {numberPerGroup} ; ");
            if (debugPrint)
            {
                persons.ToList().ForEach(x =>
                        this.output.WriteLine($"{x.Surname} ; {x.Forename} ; {x.Age} ; { (x.Alive ? "T" : "F") } ; p: {x.Prob:f2} ; {x.Salary:C} ; {x.DOB:yyyy/MM/dd hh:mm:ss} ; {x.Address.PostCode} ")
                //this.output.WriteLine($"{x.Surname} ; {x.Forename} ; {x.Age} ; { (x.Alive ? "T" : "F") } ; p: {x.Prob:f2} ; {x.Salary:C} ; {x.DOB:yyyy/MM/dd hh:mm:ss} ;{x.DOB} ; {x.Address.PostCode} ")
                );
            }
        }

        //[Theory]
        //[InlineData(true, 15, 3)]
        public void T021_sortit_by_single_property_asc_or_desc_should_not_except(
            bool debugPrint,
            int N,
            int numberPerGroup
        )
        {
            // Arrange
            var personsTestables =
                GetTestablePersons(N, numberPerGroup);

            // AsQueryable
            var personsTestablesQueryable =
                personsTestables
                .AsQueryable();

            // Surname
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Surname", Desc = false } };
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Surname", Desc = true } };

            // forename
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Forename", Desc = false} };
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Forename", Desc = true } };

            // Age
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Age"} };
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Age", Desc = true } };

            // Alive
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Alive", Desc = false, First = true } };

            // prob
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Prob", Desc = false, First = true } };
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Prob", Desc = true, First = true } };

            // Salary
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Salary", Desc = false, First = true } };
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Salary", Desc = true, First = true } };

            // DOB
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "DOB", Desc = false, First = true } };
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "DOB", Desc = true , First = true } };

            // Address
            //var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Address.PostCode", Desc = false } };
            var sorterMetas = new List<SorterMeta> { new SorterMeta { PropertyName = "Address.PostCode", Desc = true } };

            // Act

            // sut call
            var personsSorted =
                personsTestablesQueryable
                .Sortit(sorterMetas);

            // Assert

            // Assert - Dbg Print
            var persons =
                personsSorted
                .ToList();

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
        //[InlineData(true, 15, 3)]
        public void T031_sortit_by_single_property_asc_or_desc_should_not_except(
            bool debugPrint,
            int N,
            int numberPerGroup
        )
        {
            // Arrange
            var personsTestables =
                GetTestablePersons(N, numberPerGroup);

            // AsQueryable
            var personsTestablesQueryable =
                personsTestables
                .AsQueryable();

            // address asc , surname asc
            var sorterMetas = new List<SorterMeta>
            {
                new SorterMeta { PropertyName = "Address.PostCode" }
                , new SorterMeta { PropertyName = "Surname"}
            };

            // address asc , surname desc
            //var sorterMetas = new List<SorterMeta>
            //{
            //    new SorterMeta { PropertyName = "Address.PostCode" }
            //    , new SorterMeta { PropertyName = "Surname", Desc = true}
            //};

            // address desc , surname asc
            // var sorterMetas = new List<SorterMeta>
            // {
            //     new SorterMeta { PropertyName = "Address.PostCode", Desc = true }
            //     , new SorterMeta { PropertyName = "Surname"}
            // };

            // address desc , surname asc
            //var sorterMetas = new List<SorterMeta>
            //{
            //    new SorterMeta { PropertyName = "Address.PostCode", Desc = true }
            //    , new SorterMeta { PropertyName = "Surname", Desc = true }
            //};

            // Act

            // sut call
            var personsSorted =
                personsTestablesQueryable
                .Sortit(sorterMetas);

            // Assert

            // Assert - Dbg Print
            var persons =
                personsSorted
                .ToList();

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
        //[InlineData(true, 15, 3, "Surname ")]
        //[InlineData(true, 15, 3, "Surname desc")]
        //[InlineData(true, 15, 3, "Address.PostCode")]
        //[InlineData(true, 15, 3, "Address.PostCode desc")]
        //[InlineData(true, 15, 3, "Surname, Address.PostCode")]
        //[InlineData(true, 15, 3, "Surname desc, Address.PostCode")]
        //[InlineData(true, 15, 3, "Surname desc, Address.PostCode desc")]
        [InlineData(true, 15, 3, "DOB")]
        //[InlineData(true, 15, 3, "DOB desc")]
        //[InlineData(true, 15, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
        public void T041_sortit_by_single_property_asc_or_desc_should_not_except(
            bool debugPrint,
            int N,
            int numberPerGroup,
            string sorterMetaString
        )
        {
            // Arrange
            var personsTestables =
                GetTestablePersons(N, numberPerGroup);

            // AsQueryable
            var personsTestablesQueryable =
                personsTestables
                    .AsQueryable();

            // e.g "Surname, AddressPostCode desc" -> {...}
            var sorterMetas = ToSorterMetas(sorterMetaString);

            #region

            // address asc , surname desc

            //var sorterMetas = new List<SorterMeta>
            //{
            //    new SorterMeta { PropertyName = "Address.PostCode" }
            //    , new SorterMeta { PropertyName = "Surname", Desc = true}
            //};

            // address desc , surname asc
            // var sorterMetas = new List<SorterMeta>
            // {
            //     new SorterMeta { PropertyName = "Address.PostCode", Desc = true }
            //     , new SorterMeta { PropertyName = "Surname"}
            // };

            // address desc , surname asc
            //var sorterMetas = new List<SorterMeta>
            //{
            //    new SorterMeta { PropertyName = "Address.PostCode", Desc = true }
            //    , new SorterMeta { PropertyName = "Surname", Desc = true }
            //};

            #endregion

            // Act

            // sut call
            var personsSorted =
                personsTestablesQueryable
                .Sortit(sorterMetas.ToList());

            // Assert

            // Assert - Dbg Print
            var persons =
                personsSorted
                    .ToList();

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
        //[InlineData(false, 10, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
        //[InlineData(false, 20, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
        //[InlineData(false, 50, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
        //[InlineData(false, 100, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
        //[InlineData(false, 1_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
        //[InlineData(false, 10_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
        //[InlineData(false, 50_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
        //[InlineData(false, 100_000, 3, "Surname desc, Forename desc, Age desc, Address.PostCode desc")]
        public void T051_sortit_by_single_property_asc_or_desc_should_not_except(
            bool debugPrint,
            int N,
            int numberPerGroup,
            string sorterMetaString
        )
        {
            // Arrange
            var personsTestables =
                GetTestablePersons(N, numberPerGroup);

            // AsQueryable
            var personsTestablesQueryable =
                personsTestables
                .AsQueryable();

            // e.g "Surname, AddressPostCode desc" -> {...}
            var sorterMetas = ToSorterMetas(sorterMetaString);

            // Act

            // sut call
            var personsSorted =
                personsTestablesQueryable
                    .Sortit(sorterMetas.ToList());

            // Assert

            // Assert - Dbg Print
            var persons =
                personsSorted
                    .ToList();

            // Dbg Print
            this.output.WriteLine($"{DateTime.Now} ; N: {personsSorted.Count()} ; numberPerGroup: {numberPerGroup} ; ");
            if (debugPrint)
            {
                persons.ToList().ForEach(x =>
                    this.output.WriteLine($"{x.Surname} ; {x.Forename} ; Age: {x.Age:D2} ; { (x.Alive ? "True " : "false") } ; p: {x.Prob:f2} ; {x.Salary:C} ; {x.DOB:yyyy/MM/dd hh:mm:ss} ; {x.Address.PostCode} ")
                );
            }
        }

        [Fact]
        public void toSorterMetas_should_not_execpt()
        {
            // Arrange
            //var sorterMetaString = "Surname";
            //var sorterMetaString = "Surname asc";
            //var sorterMetaString = "Surname desc";
            //var sorterMetaString = "Surname,Forename";
            //var sorterMetaString = "Surname,Forename";
            //var sorterMetaString = "Surname, Forename    ";
            //var sorterMetaString = "         Surname         , Forename    ";
            //var sorterMetaString = "Surname desc, Forename";
            //var sorterMetaString = "Surname desc, Forename desc";
            //var sorterMetaString = "Surname desc, Forename desc, Age desc";
            var sorterMetaString = "Surname desc, Forename desc, Age desc, Address.PostCode desc";

            // Act

            //
            var sorterMetas = ToSorterMetas(sorterMetaString);

            // Assert

            // Dbg Print
            this.output.WriteLine($"{sorterMetaString} ");
            sorterMetas.ToList().ForEach(x =>
                this.output.WriteLine($"SM : { x.PropertyName} ; {x.Desc}")
            );
        }
    }
}
*/