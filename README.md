# Mapping-By-Code `Join` repro case
This repository is a reproduction case for [a bug in NHibernate] whereby Mapping By Code (MbC) produces incorrect SQL if there is more than one `Join` in a single class mapping.  It would seem that parts of the join mapping from subsequent joins are accidentally overrwriting/overriding the same parts of the join mapping from previous joins in the same class mapping file.  In this repro case you can see the effect with *the key column name*.  More information may be found at the linked issue.

The reproduction case is framed as a pair of NUnit test cases, so to see the results just clone the repo and run **dotnet test**.

* The test case `NHibernate_should_not_throw_when_querying_a_Person_mapped_using_XML` passes, and shows that NHibernate behaves correctly when not using MbC
* The test case `NHibernate_should_not_throw_when_querying_a_Person_mapped_using_MbC` fails, demonstrating the bug

Note that in this reproduction case, to make it as easy as possible to run, I have used an in-memory SQLite database.  I have also seen this same bug reproduced against MS-SQL server 2012, so I do not think that the database, driver or dialect are related to the problem.

### What the tests do
The test project sets up two instances of `ISessionFactory`, which *should theoretically be equivalent*.  One session factory uses MbC (see the class `PersonMap`) and the other session factory uses XML HBM mappings (see the file `PersonMap.hbm.xml`).

A class named `DbInitializer` is used to create a sample database schema matching those mappings, with some sample data.  The tests then query this data using a Linq query.

* In the case of the XML-based mappings, everything works OK and no exception is raised.

* In the case of the MbC-based mappings, the query throws an exception, due to incorrect SQL being sent to the database driver.

### The exception & SQL
The exception raised from the MbC test case is an `NHibernate.Exceptions.GenericADOException`, wrapping  a native ADO exception from the database driver, complaining about invalid SQL logic.

The SQL which is sent to the database (in the failing test) is as follows.  *I have made trivial whitespace changes for readability, as well as adding a comment to point out the error*.

```sql
select
    person0_.PersonId           as personid1_0_,
    person0_.Name               as name2_0_,
    person0_1_.PayrollNumber    as payrollnumber2_1_,
    person0_2_.City             as city2_2_
    
from
    Person person0_
    inner join PayrollInfo person0_1_
        on person0_.PersonId=person0_1_.AddressPersonId
--                                      ^^^ This is the error, it is the wrong column name.
--                                          It should be PayrollInfoPersonId, despite it
--                                          being correct in the mapping.
    inner join Address person0_2_
        on person0_.PersonId=person0_2_.AddressPersonId

where person0_.Name=@p0

limit 1
```

[a bug in NHibernate]: https://github.com/nhibernate/nhibernate-core/issues/1277
