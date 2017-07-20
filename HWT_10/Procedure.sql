use Northwind;
GO

-- 13.1

IF EXISTS
(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'GreatestOrders'))
DROP PROCEDURE GreatestOrders;
GO

CREATE PROCEDURE GreatestOrders
@year int,
@number int
AS
BEGIN
DECLARE @temp table (EmployeeID int, OrderID int, Cost real);
INSERT INTO @temp
SELECT emp.EmployeeID AS EmployeeID
, o.OrderID
, ROUND(SUM(od.Quantity * (od.UnitPrice - od.UnitPrice * od.Discount)), 3) AS Cost
FROM Northwind.Employees AS emp
JOIN Northwind.Orders AS o
ON emp.EmployeeID = o.EmployeeID
JOIN Northwind.[Order Details] AS od
ON o.OrderID = od.OrderID
WHERE YEAR(o.OrderDate) = @year
GROUP BY o.OrderID, emp.EmployeeID;

SELECT TOP (@number)
CONVERT(nvarchar, CONCAT(emp.FirstName, ' ', emp.LastName)) AS Name,
(SELECT TOP 1 OrderID
FROM @temp
WHERE EmployeeID = emp.EmployeeID
ORDER BY Cost DESC) AS OrderID,
(SELECT TOP 1 Cost
FROM @temp
WHERE EmployeeID = emp.EmployeeID
ORDER BY Cost DESC) AS Cost
FROM Northwind.Employees AS emp
ORDER BY Cost DESC;
END
GO

-- 13.2

IF EXISTS
(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'ShippedOrdersDiff'))
DROP PROCEDURE ShippedOrdersDiff;
GO

CREATE PROCEDURE ShippedOrdersDiff
@specifiedDelay int = 35
AS
BEGIN
SELECT o.OrderID
, o.OrderDate
, o.ShippedDate
, DATEDIFF(d, o.OrderDate, o.ShippedDate) AS ShippedDelay
, @specifiedDelay AS SpecifiedDelay
FROM Northwind.Orders AS o
WHERE o.ShippedDate IS NULL OR DATEDIFF(d, o.OrderDate, o.ShippedDate) > @specifiedDelay
ORDER BY o.OrderID;
END
GO

-- 13.3

IF EXISTS
(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'SubordinationInfo'))
DROP PROCEDURE SubordinationInfo;
GO

CREATE PROCEDURE SubordinationInfo
@employeeID int
AS
BEGIN
DECLARE @all_children TABLE (id int, name VARCHAR(max), Level int);

WITH tree (EmployeeID, ReportsTo, FirstName, LastName, Level) 
AS 
(SELECT emp.EmployeeID
, emp.ReportsTo
, emp.FirstName
, emp.LastName
, 0 AS Level
FROM Northwind.Employees AS emp 
WHERE emp.EmployeeID = @employeeID 
UNION ALL 
SELECT inEmp.EmployeeID
, inEmp.ReportsTo
, inEmp.FirstName
, inEmp.LastName
, (Level + 1) AS Level 
FROM Northwind.Employees AS inEmp 
JOIN tree
ON inEmp.ReportsTo = tree.EmployeeID)

INSERT @all_children
SELECT EmployeeID
, CONCAT(FirstName, ' ', LastName)
, Level 
FROM tree;

DECLARE @result nvarchar(max);
SET @result = 
(SELECT CONCAT(REPLICATE(' ', Level * 2), a.name, char(10))
FROM @all_children AS a
FOR XML PATH(''))
PRINT(@result)
END
GO

-- 13.4

IF EXISTS
(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'IsBoss'))
DROP FUNCTION IsBoss;
GO

CREATE FUNCTION IsBoss(@employeeID int)
RETURNS bit
AS
BEGIN
IF EXISTS
(SELECT emp.EmployeeID
FROM Northwind.Employees AS emp
WHERE emp.ReportsTo = @employeeID)
RETURN 1
RETURN 0
END
GO
