create database banksystem;

CREATE TABLE Agent(ID INT NOT NULL PRIMARY KEY IDENTITY, Name VARCHAR(50) NOT NULL, Pass VARCHAR(50) NOT NULL, PhoneNumber VArCHAR(50) NOT NULL, Address VARCHAR(50) NOT NULL);

CREATE TABLE AdminTbl(AdID INT NOT NULL PRIMARY KEY, AdName VARCHAR(50) NOT NULL, AdPass VARCHAR(50) NOT NULL);

CREATE TABLE Account(CNumber INT NOT NULL PRIMARY KEY IDENTITY(1000,1), CName VARCHAR(50) NOT NULL, CPhone VARCHAR(50) NOT NULL, CAddress VARCHAR(100) NOT NULL, CGen VARCHAR(10) NOT NULL, CBal INT NOT NULL);

CREATE TABLE TransferTbl( TrID INT NOT NULL PRIMARY KEY IDENTITY, TrSrc INT NOT NULL, TrDest INT NOT NULL, TrAmt INT NOT NULL, TrDate DATE NOT NULL);

