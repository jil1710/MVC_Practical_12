CREATE DATABASE PracThirteen

go

USE PracThirteen

go

CREATE TABLE Employee (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    MiddleName VARCHAR(50),
    LastName VARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    MobileNumber VARCHAR(10) NOT NULL,
    Address VARCHAR(100)
);

go 

INSERT INTO Employee (FirstName, MiddleName, LastName, DOB, MobileNumber, Address) VALUES
('Vipul', 'Kumar', 'Upadhyay', '1999-07-07', '1234567890', 'Ahmedabad, Gujarat'),
('Bhavin', 'Kumar', 'Kareliya', '2000-05-10', '9876543210', 'Rajkot, Gujarat'),
('Jil', 'Kumar', 'Patel', '2001-09-15', '5555555555', 'Anand, Gujarat')

go

CREATE TABLE TestTwoEmployee (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    MiddleName VARCHAR(50),
    LastName VARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    MobileNumber VARCHAR(10) NOT NULL,
    Address VARCHAR(100),
    Salary DECIMAL NOT NULL
);

go 

INSERT INTO [PracThirteen].[dbo].[TestTwoEmployee] (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary) 
  VALUES 
  ('Vipul', 'Kumar', 'Upadhyay', '1999-07-07', '1234567890', 'Ahmedabad, Gujarat', 85000),
  ('Bhavin', 'Kumar', 'Kareliya', '2000-05-10', '9876543210', 'Rajkot, Gujarat', 25000),
  ('Jil', 'Kumar', 'Patel', '2001-09-15', '5555555555', 'Anand, Gujarat',75000),
  ('Test1', NULL, 'test', '2001-09-15', '5555555555', 'Anand, Gujarat',65000),
  ('Test2', NULL, 'testing', '2001-09-15', '5555555555', 'Anand, Gujarat',25000)

go


CREATE TABLE Designation(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Designation VARCHAR(50) NOT NULL
)

go

INSERT INTO Designation VALUES('Trainee'),('Jr.Developer'),('Sr.Developer'),('Manager')

go

CREATE TABLE TestThreeEmployee (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    MiddleName VARCHAR(50),
    LastName VARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    MobileNumber VARCHAR(10) NOT NULL,
    Address VARCHAR(100),
    Salary DECIMAL NOT NULL,
    DesignationId INT FOREIGN KEY REFERENCES Designation(Id)
);

 
go

INSERT INTO PracThirteen.[dbo].[TestThreeEmployee] (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary,DesignationId) 
  VALUES 
  ('Vipul', 'Kumar', 'Upadhyay', '1999-07-07', '1234567890', 'Ahmedabad, Gujarat', 85000,1),
  ('Bhavin', 'Kumar', 'Kareliya', '2000-05-10', '9876543210', 'Rajkot, Gujarat', 25000,2),
  ('Jil', 'Kumar', 'Patel', '2001-09-15', '5555555555', 'Anand, Gujarat',75000,1),
  ('Test1', NULL, 'test', '2001-09-15', '5555555555', 'Anand, Gujarat',65000,3),
  ('Test2', NULL, 'testing', '2001-09-15', '5555555555', 'Anand, Gujarat',25000,4)

go

INSERT INTO Employee (FirstName, MiddleName, LastName, DOB, MobileNumber, Address) VALUES 
('Vipul', 'Kumar', 'Upadhyay', '1999-07-07', '1234567890', 'Ahmedabad, Gujarat'),
('Bhavin', 'Kumar', 'Kareliya', '2000-05-10', '9876543210', 'Rajkot, Gujarat'),
('Jil', 'Kumar', 'Patel', '2001-09-15', '5555555555', 'Anand, Gujarat')

 
  --  Write a query to display First Name, Middle Name, Last Name & Designation name

go

  --  Create a database view that outputs Employee Id, First Name, Middle Name, Last Name, Designation, DOB, Mobile Number, Address & Salary
CREATE VIEW EmployeeView AS
SELECT e.Id AS EmployeeId,
       e.FirstName,
       e.MiddleName,
       e.LastName,
       d.Designation,
       e.DOB,
       e.MobileNumber,
       e.Address,
       e.Salary
FROM TestThreeEmployee e
INNER JOIN Designation d ON e.DesignationId = d.Id;

 go



CREATE PROCEDURE InsertDesignation
    @DesignationName VARCHAR(50)
AS
BEGIN

    INSERT INTO Designation (Designation)
    VALUES (@DesignationName);
END

 
go

CREATE PROCEDURE InsertEmployee
    @FirstName VARCHAR(50),
    @MiddleName VARCHAR(50),
    @LastName VARCHAR(50),
    @DOB DATE,
    @MobileNumber VARCHAR(10),
    @Address VARCHAR(100),
    @Salary DECIMAL,
    @DesignationId INT
AS
BEGIN

 

    INSERT INTO TestThreeEmployee(FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary, DesignationId)
    VALUES (@FirstName, @MiddleName, @LastName, @DOB, @MobileNumber, @Address, @Salary, @DesignationId);
END

go

CREATE PROCEDURE GetEmployeeList
AS
BEGIN
    SELECT e.Id AS EmployeeId,
           e.FirstName,
           e.MiddleName,
           e.LastName,
           d.Designation,
           e.DOB,
           e.MobileNumber,
           e.Address,
           e.Salary
    FROM TestThreeEmployee e
    INNER JOIN Designation d ON e.DesignationId = d.Id
    ORDER BY e.DOB;
END

 
 go

CREATE PROCEDURE GetEmployeesByDesignationId
    @DesignationId INT
AS
BEGIN
    SET NOCOUNT ON;

 

    SELECT e.Id AS EmployeeId,
           e.FirstName,
           e.MiddleName,
           e.LastName,
           e.DOB,
           e.MobileNumber,
           e.Address,
           e.Salary
    FROM TestThreeEmployee e
    WHERE e.DesignationId = @DesignationId
    ORDER BY e.FirstName;
END

