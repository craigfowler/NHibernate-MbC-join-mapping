BEGIN TRANSACTION;

-- Schema
CREATE TABLE Person (
    PersonId INT PRIMARY KEY,
    Name VARCHAR
);
CREATE TABLE Address (
    AddressId INT PRIMARY KEY,
    AddressPersonId INT NOT NULL,
    City VARCHAR,
    CONSTRAINT FK_ADDRESS_HAS_PERSON FOREIGN KEY (AddressPersonId) REFERENCES Person
);
CREATE TABLE PayrollInfo (
    PayrollInfoId INT PRIMARY KEY,
    PayrollInfoPersonId INT NOT NULL,
    PayrollNumber INT,
    CONSTRAINT FK_PAYROLLINFO_HAS_PERSON FOREIGN KEY (PayrollInfoId) REFERENCES Person
);

COMMIT;

BEGIN TRANSACTION;

-- Data
INSERT INTO Person(PersonId, Name)
VALUES(1, 'Jane Doe');
INSERT INTO Address(AddressPersonId, City)
VALUES(1, 'Nowhere');
INSERT INTO PayrollInfo(PayrollInfoPersonId, PayrollNumber)
VALUES(1, 1234);

COMMIT;