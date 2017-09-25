using System;

namespace ExprTree.Model
{
    public class Person
    {
        public string Surname { get; set; }
        public string Forename { get; set; }
        public int Age { get; set; }
        public bool Alive { get; set; }

        public double Prob { get; set; }
        public decimal Salary { get; set; }

        public DateTime DOB { get; set; }

        public Address Address { get; set; }

        public Town Town { get; set; }

    }
}