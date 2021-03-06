﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SortIt
{
    public static class IOrderedQueryableExtensions

    {
        public static IOrderedQueryable<T> SortItOrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }

        public static IOrderedQueryable<T> SortItOrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable<T> SortItThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }

        public static IOrderedQueryable<T> SortItThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        #region Original

        //// https://stackoverflow.com/questions/41244/dynamic-linq-orderby-on-ienumerablet
        //private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        //{
        //    string[] props = property.Split('.');
        //    Type type = typeof(T);
        //    ParameterExpression arg = Expression.Parameter(type, "x");
        //    Expression expr = arg;
        //    foreach (string prop in props)
        //    {
        //        // use reflection (not ComponentModel) to mirror LINQ
        //        PropertyInfo pi = type.GetProperty(prop);
        //        expr = Expression.Property(expr, pi);
        //        type = pi.PropertyType;
        //    }
        //    Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
        //    LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
        //
        //    object result = typeof(Queryable).GetMethods().Single(
        //            method => method.Name == methodName
        //                      && method.IsGenericMethodDefinition
        //                      && method.GetGenericArguments().Length == 2
        //                      && method.GetParameters().Length == 2)
        //        .MakeGenericMethod(typeof(T), type)
        //        .Invoke(null, new object[] { source, lambda });
        //    return (IOrderedQueryable<T>)result;
        //}

        #endregion Original

        /// <summary>
        /// e.g. ApplyOrder( queryable, "t.Address.PostCode" , "OrderBy")
        /// It does this by
        ///   1. searching for the methodName e.g. "OrderBy" method, which is an extension method on IQueryable<T>
        ///   2.  1st param is the queryable passed in as param to this fun
        ///   3.  the 2nd param, is a expression, it this expression the propertyPath param e.g. "t.Address.PostCode"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="propertyPath"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private static IOrderedQueryable<T> ApplyOrder<T>(
            IQueryable<T> queryable,
            string propertyPath,
            string methodName
        )
        {
            // Code
            // get an array of string that 'paths' to the property
            // e.g. property = "Surname"  -> ["Surname"]
            // e.g. property = "Address.PostCode" -> ["Address","PostCode"]
            var properties = propertyPath.Split('.');

            // This function is generic on T
            // What is the typeOfT? e.g. Person
            var typeOfT = typeof(T);

            // A method will be called, that method will take a parameter
            // That parameter is represented here
            //  it will be called "t"
            //  its type is T e.g. Person
            var parameterExpressionOfT = Expression.Parameter(typeOfT, "t");

            // for each property in the property path e.g. Person.Surname get its (expression, type)
            Expression pathPropertyExpression = parameterExpressionOfT; // initialise propertyExpression with "t"
            Stack<Expression> pathPropertExpressions = new Stack<Expression>();
            pathPropertExpressions
                .Push(parameterExpressionOfT); // list of each property path as they get built up so that we can build a nested if/else block later.
            var propertyType =
                typeOfT; // initialise propertyType with typeOfT, but this will become the type of the property e.g. typeof Surname

            foreach (var property in properties)
            {
                // For the property get it's (expression , type )
                var propertyInfo = propertyType.GetProperty(property);

                if (propertyInfo != null)
                {
                    // for each property down the path, get its (expression, type)
                    // the first time through : propertyExpression on the rhs might be "t", and after the assignment, will be "t.Surname"
                    // 2nd time through : propertyExpression on the rhs will be "t.Address", and after the assignment, will be "t.Address.PostCode"

                    pathPropertyExpression = Expression.Property(pathPropertyExpression, propertyInfo);
                    pathPropertExpressions.Push(pathPropertyExpression);

                    // this will be used to create expression for the OrderBy( expression )
                    propertyType = propertyInfo.PropertyType;
                }
            }

            Expression ifExpression = pathPropertyExpression; // assume no depth to start with
            //foreach (Expression exp in pathPropertExpressions)
            Expression exp;
            while (pathPropertExpressions.Count > 0)
            {
                exp = pathPropertExpressions.Pop();
                ifExpression =
                    Expression.Condition(
                        Expression.Equal(exp, Expression.Default(exp.Type) /*Expression.Constant(null)*/),
                        Expression.Default(pathPropertyExpression.Type), ifExpression);
            }

            // get a delegate, that delegate belongs to typeOfT and string because t.Surname is type string
            // e.g. for "Person.Surname" ,
            //Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), propertyType);
            Type delegateType = typeof(Func<,>).MakeGenericType(typeOfT, propertyType);

            // wrap that delegate in an expression,
            //LambdaExpression expressionForPathProperty = Expression.Lambda(delegateType, pathPropertyExpression, parameterExpressionOfT);
            LambdaExpression expressionForPathProperty =
                Expression.Lambda(delegateType, ifExpression, parameterExpressionOfT);

            // get a the single method info that matches the criteria
            //  There is e method on System.Linq.Queryable
            //   it has a name,
            //   as was sent into this function e.g. "OrderBy"
            //  that method is Generic & has 2 generic arguments & 2 params

            var methodInfo =
                typeof(Queryable)
                    .GetMethods()
                    .Single(method =>
                        method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2
                    );

            // make a genericMethod, using the methodInfo
            var genericMethod = methodInfo.MakeGenericMethod(typeof(T), propertyType);

            // call the method, that method is a extension method of the queryable
            //   the 'this' parameter is the queryable
            //   the 2nd param is an expression for the path property
            //   e.g OrderBy and it's homo-iconicity(!) is something like "t.Surname" or "t.Address.PostCode"
            var result = genericMethod.Invoke(null, new object[] { queryable, expressionForPathProperty });

            //result = genericMethod.Invoke(null, new object[] { queryable, expressionForPathProperty });

            // Note genericMethod.Invoke(...) is typed is returning a object.
            // But we know Queryable.OrderBy(), and other funs like this, always return IOrderedQueryable
            // So we should be able to cast that return to IOrderedQueryable<T>
            // rfc : ?could probably do something like var ret = result as IOrderedQueryable<T>, and assert before the fun goers back

            // Return
            return (IOrderedQueryable<T>)result;
        }
    }
}
