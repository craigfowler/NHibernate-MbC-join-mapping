using System;
namespace NHibernate.Tests
{
    public class Person
    {
        public virtual int PersonId { get; set; }

        // Comes from the Person table
        public virtual string Name { get; set; }

        // Comes from the Address table
        public virtual string City { get; set; }

        // Comes from the PayrollInfo table
        public virtual int PayrollNumber { get; set; }
    }
}
