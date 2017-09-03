using System;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace Sortit.UnitTest
{
    public class ExprTreeUnitTest
    {
        private ITestOutputHelper output;

        public ExprTreeUnitTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void asb()
        {
            // Arrange

            //  anon method assigned to delegate
            Func<int, int> d2 = delegate (int x) { return x * 2; };

            // lambda
            Func<int, int> f35 = (x) => x * 2;

            //
            Expression<Func<int, int>> fExpression = (x) => x * 2;
            var f = fExpression.Compile();

            //
            ParameterExpression pe = Expression.Parameter(typeof(int));
            //    // the above is really ...
            //    ParameterExpression x = Expression.Parameter(typeof(int));
            //    Expression<Func<int, int>> g = Expression.Lambda<Func<int, int>>(
            //        Expression.Multiply(x, Expression.Constant(2)),
            //        x
            //    );
            //    var gCompiled = g.Compile();
            //

            // Assert

            Assert.Equal(2, d2(1));
            Assert.Equal(2, f35(1));
            Assert.Equal(2, f(1));

            // dbg print
            this.output.WriteLine($" d(1) : {d2(1)} ");
            this.output.WriteLine($" f(1) : {f35(1)} ");
            this.output.WriteLine($" f(1) : {f(1)} ");
        }
    }
}