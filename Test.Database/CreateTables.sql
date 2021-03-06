﻿USE TestDB
GO

CREATE TABLE People(
	PeopleID INT IDENTITY(1,1) NOT NULL,
	LastName NVARCHAR(255) NOT NULL UNIQUE,
	FirstName NVARCHAR(255) NOT NULL UNIQUE,

	CONSTRAINT PK_People PRIMARY KEY (PeopleID))
GO

CREATE TABLE Mark(
	PeopleID INT NOT NULL,
	Value INT NULL
	CONSTRAINT PK_Mark PRIMARY KEY (PeopleID))

GO

ALTER TABLE Mark ADD CONSTRAINT [FK_Mark_People] FOREIGN KEY(PeopleID)
	REFERENCES People (PeopleID)
GO
