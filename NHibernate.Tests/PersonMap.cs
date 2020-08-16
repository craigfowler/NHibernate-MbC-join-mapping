using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.Tests
{
    public class PersonMap : ClassMapping<Person>
    {
        // Note that the XML mapping in the same directory as this is equivalent.
        // Only one of the two mappings should be used at a time.

        public PersonMap()
        {
            Id(x => x.PersonId);

            Property(x => x.Name);

            Join("PayrollInfo", j =>
            {
                j.Key(k => k.Column("PayrollInfoPersonId"));
                j.Property(x => x.PayrollNumber);
            });

            Join("Address", j =>
            {
                j.Key(k => k.Column("AddressPersonId"));
                j.Property(x => x.City);
            });
        }
    }
}
