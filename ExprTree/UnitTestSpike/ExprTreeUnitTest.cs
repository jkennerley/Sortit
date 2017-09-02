////using static ExprTree.Testables;
//
//namespace ExprTree.UnitTest
//{
//    public class ExpreTreeDemo
//    {
//        //private ITestOutputHelper output;
//        //
//        //public ExpreTreeDemo(ITestOutputHelper output)
//        //{
//        //    this.output = output;
//        //}
//        //
//        //[Fact]
//        //public void X0001_expreTree_syntax()
//        //{
//        //    // Arrange
//        //
//        //    // c#2 : anon method, assigned to delegate type
//        //    Func<int, int> foo2 = delegate (int x) { return x * 2; };
//        //
//        //    // c#35 : lamda expression assigned to delegate type
//        //    Func<int, int> foo35 = x => x * 2; ;
//        //
//        //    // c#35 : lamda expression assigned to Expression tree
//        //    Expression<Func<int, int>> fooET = x => x * 2;
//        //    Func<int, int> foo = fooET.Compile();
//        //
//        //    this.output.WriteLine($"{foo.Method.MemberType}");
//        //
//        //    // Act
//        //
//        //    // Assert
//        //    Assert.Equal(2, foo2(1));
//        //    Assert.Equal(2, foo35(1));
//        //    Assert.Equal(2, foo(1));
//        //}
//        //
//        //[Fact]
//        //public void X0005_expreTree_syntax()
//        //{
//        //    // Arrange
//        //
//        //    // c#35 : lamda expression assigned to Expression tree
//        //    Expression<Func<int, int>> f = a => a * 2;
//        //    var fCompiled = f.Compile();
//        //
//        //    // Act
//        //
//        //    // Assert
//        //    Assert.Equal(2, fCompiled(1));
//        //}
//        //
//        //[Fact]
//        //public void X0010_expreTree_syntax()
//        //{
//        //    // Arrange
//        //
//        //    // the above is really ...
//        //    ParameterExpression x = Expression.Parameter(typeof(int));
//        //    Expression<Func<int, int>> g = Expression.Lambda<Func<int, int>>(
//        //        Expression.Multiply(x, Expression.Constant(2)),
//        //        x
//        //    );
//        //    var gCompiled = g.Compile();
//        //
//        //    // Act
//        //
//        //    // Assert
//        //    Assert.Equal(2, gCompiled(1));
//        //}
//
//        //[Fact]
//        //public void main()
//        //public void X0020_a_eq_1()
//        //{
//        //    // Arrange
//        //    //var a = 17;
//        //    Func<int> f = () => 42;
//        //    //Expression<Func<int>> g = () => 42;
//        //
//        //
//        //    // Act
//        //
//        //    // Assert
//        //    //Assert.Equal(2, gCompiled(1));
//        //}
//    }
//}