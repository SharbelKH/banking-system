create database banksystem;

CREATE TABLE Customers( ID int NOT NULL, FirstName varchar(255), LastName varchar(255), PRIMARY KEY(ID));

INSERT INTO Customers(ID, FirstName, LastName)
VALUES	(1, 'John', 'Doe'),
		(2, 'Jane', 'Doe'),
		(3, 'Bob', 'Brown'),
		(4, 'Frank', 'Stein'),
		(5, 'Steve', 'Hardy');

SELECT *
FROM Customers;