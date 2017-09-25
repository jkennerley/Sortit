using System;

namespace ExprTree.Model
{
    public class Town
    {
        public Guid Id { get; set; }

        public string  Name { get; set; }

        public County County { get; set; }
    }
}

namespace ExprTree.Model
{
    public class County
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Country Country { get; set; }
    }
}

namespace ExprTree.Model
{
    public class Country
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}