use Northwind;

-- 1.1 ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ����
-- (������� ShippedDate) ������������ � ������� ���������� � ShipVia >= 2. 
-- ������ �������� ���� ������ ���� ������ ��� ����� ������������ ����������, 
-- �������� ����������� ������ �Writing International Transact-SQL Statements� 
-- � Books Online ������ �Accessing and Changing Relational Data Overview�. 
-- ���� ����� ������������ ����� ��� ���� �������. ������ ������ ����������� 
-- ������ ������� OrderID, ShippedDate� ShipVia. 
-- �������� ������ ���� �� ������ ������ � NULL-�� � ������� ShippedDate.

SELECT OrderID
, ShippedDate
, ShipVia 
FROM Northwind.Orders
WHERE ShippedDate >= CONVERT(DATETIME, '19980506', 101)
AND ShipVia >= 2;

-- ������ � NULL � ������� ShippedDate �� ������ � �������,
-- �.�. ��������� NULL � ����� ������ ��������� ���������� UNKNOWN,
-- � ��� �� ����� TRUE

-- 1.2 �������� ������, ������� ������� ������ �������������� ������ �� ������� Orders. 
-- � ����������� ������� ����������� ��� ������� ShippedDate ������ �������� NULL 
-- ������ �Not Shipped� � ������������ ��������� ������� CAS�. ������ ������ ����������� 
-- ������ ������� OrderID � ShippedDate.

SELECT OrderID, 
CASE 
WHEN ShippedDate IS NULL
THEN 'Not Shipped'
END [ShippedDate]
FROM Northwind.Orders
WHERE ShippedDate IS NULL;

-- 1.3 ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (ShippedDate)
-- �� ������� ��� ���� ��� ������� ��� �� ����������. � ������� ������ ������������� ������
-- ������� OrderID (������������� � Order Number) � ShippedDate (������������� � Shipped Date). 
-- � ����������� ������� ����������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped�, 
-- ��� ��������� �������� ����������� ���� � ������� �� ���������.

SELECT OrderID AS [Order Number], 
CASE 
WHEN ShippedDate IS NULL
THEN 'Not Shipped'
ELSE CAST(ShippedDate AS nvarchar)
END [Shipped Date]
FROM Northwind.Orders
WHERE ShippedDate > CONVERT(DATETIME, '19980506', 101)
OR ShippedDate IS NULL;

-- 2.1 ������� �� ������� Customers ���� ����������, ����������� � USA � Canada. 
-- ������ ������� � ������ ������� ��������� IN. ����������� ������� � ������ 
-- ������������ � ��������� ������ � ����������� �������. ����������� ���������� 
-- ������� �� ����� ���������� � �� ����� ����������.

SELECT CompanyName, Country
FROM Northwind.Customers
WHERE Country IN ('USA', 'Canada')
ORDER BY CompanyName, Country;

-- 2.2 ������� �� ������� Customers ���� ����������, �� ����������� � USA � Canada. 
-- ������ ������� � ������� ��������� IN. ����������� ������� � ������ ������������ 
-- � ��������� ������ � ����������� �������. ����������� ���������� ������� �� ����� ����������.

SELECT CompanyName, Country
FROM Northwind.Customers
WHERE Country NOT IN ('USA', 'Canada')
ORDER BY CompanyName;

-- 2.3 ������� �� ������� Customers ��� ������, � ������� ��������� ���������. 
-- ������ ������ ���� ��������� ������ ���� ��� � ������ ������������ �� ��������. 
-- �� ������������ ����������� GROUP BY. ����������� ������ ���� ������� � ����������� �������.

SELECT DISTINCT Country
FROM Northwind.Customers
ORDER BY Country DESC;

-- 3.1 ������� ��� ������ (OrderID) �� ������� Order Details (������ �� ������ �����������), 
-- ��� ����������� �������� � ����������� �� 3 �� 10 ������������ � ��� ������� Quantity � ������� 
-- Order Details. ������������ �������� BETWEEN. ������ ������ ����������� ������ ������� OrderID.

SELECT DISTINCT OrderID
FROM Northwind.[Order Details]
WHERE Quantity BETWEEN 3 AND 10;

-- 3.2 ������� ���� ���������� �� ������� Customers, � ������� �������� ������ ���������� �� �����
-- �� ��������� b � g. ������������ �������� BETWEEN. ���������, ��� � ���������� ������� �������� Germany. 
-- ������ ������ ����������� ������ ������� CustomerID � Country � ������������ �� Country.

SELECT CustomerID, Country
FROM Northwind.Customers
WHERE SUBSTRING(LOWER(Country), 1, 1) BETWEEN 'b' AND 'g'
ORDER BY Country;

-- 3.3 ������� ���� ���������� �� ������� Customers, � ������� �������� ������ ���������� �� �����
-- �� ��������� b � g, �� ��������� �������� BETWEEN. � ������� ����� �Execution Plan� ���������� 
-- ����� ������ ���������������� 3.2 ��� 3.3 � ��� ����� ���� ������ � ������ ���������� ���������� 
-- Execution Plan-a ��� ���� ���� ��������, ���������� ���������� Execution Plan ���� ������ � ������ 
-- � ���� ����������� � �� �� ����������� ���� ����� �� ������ � �� ������ ��������� ���� ��������� ���������. 
-- ������ ������ ����������� ������ ������� CustomerID � Country � ������������ �� Country.

SELECT CustomerID, Country
FROM Northwind.Customers
WHERE SUBSTRING(LOWER(Country), 1, 1) IN ('b', 'c', 'd', 'f', 'g')
ORDER BY Country;

-- ������ 1: ��������� ������� (�� ��������� � ������): 50%
-- ������ 2: ��������� ������� (�� ��������� � ������): 50%
-- ��������� ���� ����������� �� ��������� ������� ���������� ������� �� ������� ���������� ����� 
-- ������ ��������. � ���������� ��� ������� ����� ����������� �������� �� ���������� �����.

-- 4.1 � ������� Products ����� ��� �������� (������� ProductName), ��� ����������� ��������� 'chocolade'. 
-- ��������, ��� � ��������� 'chocolade' ����� ���� �������� ���� ����� 'c' � �������� - ����� ��� ��������, 
-- ������� ������������� ����� �������. ���������: ���������� ������� ������ ����������� 2 ������.

SELECT ProductName
FROM Northwind.Products
WHERE ProductName LIKE '%cho_olade%';

-- 5.1 ����� ����� ����� ���� ������� �� ������� Order Details � ������ ���������� 
-- ����������� ������� � ������ �� ���. ��������� ��������� �� ����� � ��������� � 
-- ����� 1 ��� ���� ������ money. ������ (������� Discount) ���������� ������� �� 
-- ��������� ��� ������� ������. ��� ����������� �������������� ���� �� ��������� 
-- ������� ���� ������� ������ �� ��������� � ������� UnitPrice ����. ����������� 
-- ������� ������ ���� ���� ������ � ����� �������� � ��������� ������� 'Totals'.

SELECT CONVERT( nvarchar, CONVERT( money, ROUND( SUM( UnitPrice - UnitPrice * Discount), 3)), 1) AS Totals
FROM Northwind.[Order Details];

-- 5.2 �� ������� Orders ����� ���������� �������, ������� ��� �� ���� ���������� 
-- (�.�. � ������� ShippedDate ��� �������� ���� ��������). ������������ ��� ���� 
-- ������� ������ �������� COUNT. �� ������������ ����������� WHERE � GROUP.

SELECT COUNT(*) - COUNT(ShippedDate) AS ShippedCount
FROM Northwind.Orders;

-- 5.3 �� ������� Orders ����� ���������� ��������� ����������� (CustomerID), ��������� 
-- ������. ������������ ������� COUNT � �� ������������ ����������� WHERE � GROUP.

SELECT COUNT(DISTINCT CustomerID) AS Customers
FROM Northwind.Orders;

-- 6.1 �� ������� Orders ����� ���������� ������� � ������������ �� �����. � ����������� 
-- ������� ���� ����������� ��� ������� c ���������� Year � Total. �������� ����������� 
-- ������, ������� ��������� ���������� ���� �������.

SELECT YEAR(ShippedDate) AS Year
, COUNT(YEAR(ShippedDate)) AS Total
FROM Northwind.Orders
WHERE YEAR(ShippedDate) IS NOT NULL
GROUP BY YEAR(ShippedDate);

-- ����������� ������
SELECT COUNT(YEAR(ShippedDate)) AS TotalCount
FROM Northwind.Orders;

-- 6.2 �� ������� Orders ����� ���������� �������, c�������� ������ ���������. ����� ��� 
-- ���������� �������� � ��� ����� ������ � ������� Orders, ��� � ������� EmployeeID ������ 
-- �������� ��� ������� ��������. � ����������� ������� ���� ����������� ������� � ������ �������� 
-- (������ ������������� ��� ���������� ������������� LastName & FirstName. ��� ������ LastName & 
-- FirstName ������ ���� �������� ��������� �������� � ������� ��������� �������. ����� �������� 
-- ������ ������ ������������ ����������� �� EmployeeID.) � ��������� ������� �Seller� � ������� 
-- c ����������� ������� ����������� � ��������� 'Amount'. ���������� ������� ������ ���� ����������� 
-- �� �������� ���������� �������.

SELECT
(SELECT CONCAT(emp.LastName, ' ', emp.FirstName)
FROM Northwind.Employees AS emp
WHERE ord.EmployeeID = emp.EmployeeID) AS Seller
, COUNT(ord.EmployeeID) AS Amount
FROM Northwind.Orders AS ord
GROUP BY ord.EmployeeID
ORDER BY Amount DESC;

-- 6.3 �� ������� Orders ����� ���������� �������, c�������� ������ ��������� � ��� ������� ����������. 
-- ���������� ���������� ��� ������ ��� ������� ��������� � 1998 ����. � ����������� ������� ���� ����������� 
-- ������� � ������ �������� (�������� ������� �Seller�), ������� � ������ ���������� (�������� �������Customer�) 
-- � ������� c ����������� ������� ����������� � ��������� 'Amount'. � ������� ���������� ������������ ����������� 
-- �������� ����� T-SQL ��� ������ � ���������� GROUP (���� �� �������� ������� �������� ������ �ALL� � ����������� 
-- �������). ����������� ������ ���� ������� �� ID �������� � ����������. ���������� ������� ������ ���� ����������� 
-- �� ��������, ���������� � �� �������� ���������� ������. � ����������� ������ ���� ������� ���������� �� ��������. 
-- �.�. � ������������� ������ ������ �������������� ������������� � ���������� � �������� �������� ��� ������� 
-- ���������� ��������� �������:

-- Seller	Customer	Amount
-- ALL		ALL			<����� ����� ������>
-- <���>	ALL			<����� ������ ��� ������� ��������> 
-- ALL		<���>		<����� ������ ��� ������� ����������> 
-- <���>	<���>		<����� ������ ������� �������� ��� �������� ����������>

SELECT

CASE
WHEN GROUPING(ord.EmployeeID) = 1
THEN 'ALL'
ELSE
(SELECT CONCAT(emp.LastName, ' ', emp.FirstName)
FROM Northwind.Employees AS emp
WHERE emp.EmployeeID = ord.EmployeeID)
END
AS Seller,

CASE
WHEN GROUPING(ord.CustomerID) = 1
THEN 'ALL'
ELSE
(SELECT c.CompanyName
FROM Northwind.Customers AS c
WHERE ord.CustomerID = c.CustomerID)
END
AS Customer,

COUNT(*) AS Amount

FROM Northwind.Orders AS ord
WHERE YEAR(ord.OrderDate) = 1998
GROUP BY CUBE(ord.EmployeeID, ord.CustomerID)
ORDER BY Seller, Customer, Amount DESC;

-- 6.4 ����� ����������� � ���������, ������� ����� � ����� ������. ���� � ������ ����� ������ 
-- ���� ��� ��������� ��������� ��� ������ ���� ��� ��������� �����������, �� ���������� � ����� 
-- ���������� � ��������� �� ������ �������� � �������������� �����. �� ������������ ����������� JOIN. 
-- � ����������� ������� ���������� ������� ��������� ��������� ��� ����������� �������: �Person�, 
-- �Type� (����� ���� �������� ������ �Customer� ��� �Seller� � ��������� �� ���� ������), �City�. 
-- ������������� ���������� ������� �� ������� �City� � �� �Person�.

SELECT CONCAT(emp.LastName, ' ', emp.FirstName) AS Person,
'Seller' AS Type,
emp.City AS City
FROM Northwind.Employees AS emp
WHERE City IN (SELECT c.City FROM Northwind.Customers AS c)
UNION
SELECT c.CompanyName AS Person,
'Customer' AS Type,
c.City AS City
FROM Northwind.Customers AS c
WHERE City IN (SELECT emp.City FROM Northwind.Employees AS emp)
ORDER BY City, Person;

-- 6.5 ����� ���� �����������, ������� ����� � ����� ������. � ������� ������������ ���������� ������� 
-- Customers c ����� - ��������������. ��������� ������� CustomerID � City. ������ �� ������ ����������� 
-- ����������� ������. ��� �������� �������� ������, ������� ����������� ������, ������� ����������� ����� 
-- ������ ���� � ������� Customers. ��� �������� ��������� ������������ �������.

SELECT DISTINCT c.CustomerID, c.City
FROM Northwind.Customers AS c
JOIN Northwind.Customers AS c2
ON c.City = c2.City
WHERE c.CustomerID <> c2.CustomerID;

SELECT c.City,
COUNT(c.City) AS Count
FROM Northwind.Customers AS c
GROUP BY c.City
HAVING COUNT(c.City) > 1
ORDER BY c.City;

-- 6.6 �� ������� Employees ����� ��� ������� �������� ��� ������������, �.�. ���� �� ������ �������. 
-- ��������� ������� � ������� 'User Name' (LastName) � 'Boss'. � �������� ������ ���� ��������� ����� 
-- �� ������� LastName. ��������� �� ��� �������� � ���� �������?

SELECT emp1.LastName AS [User Name]
, emp2.LastName AS [Boss]
FROM Northwind.Employees AS emp1
JOIN Northwind.Employees AS emp2
ON emp1.ReportsTo = emp2.EmployeeID;

-- ������ ��� ��������
SELECT emp.LastName, emp.ReportsTo
FROM Northwind.Employees AS emp;

-- � ������� ��������� �� ��� ��������, ��� ��� ���������� ������ � ������� ������ NULL,
-- �� ���� ���������� ����, � ������� ��� ������������

-- 7.1 ���������� ���������, ������� ����������� ������ 'Western' (������� Region). 
-- ���������� ������� ������ ����������� ��� ����: 'LastName' �������� � �������� ������������� 
-- ���������� ('TerritoryDescription' �� ������� Territories). ������ ������ ������������ JOIN �
-- ����������� FROM. ��� ����������� ������ ����� ��������� Employees � Territories ���� ������������
-- ����������� ��������� ��� ���� Northwind.

SELECT emp.LastName
, t.TerritoryDescription
FROM Northwind.Employees AS emp
JOIN Northwind.EmployeeTerritories AS et
ON emp.EmployeeID = et.EmployeeID
JOIN Northwind.Territories AS t
ON et.TerritoryID = t.TerritoryID
JOIN Northwind.Region AS r
ON t.RegionID = r.RegionID
WHERE r.RegionDescription = 'Western';

-- 8.1 ��������� � ����������� ������� ����� ���� ���������� �� ������� Customers � ��������� 
-- ���������� �� ������� �� ������� Orders. ������� �� ��������, ��� � ��������� ���������� ��� 
-- �������, �� ��� ����� ������ ���� �������� � ����������� �������. ����������� ���������� ������� 
-- �� ����������� ���������� �������.

SELECT
(SELECT c1.CompanyName
FROM Northwind.Customers AS c1
WHERE c.CustomerID = c1.CustomerID)
AS CompanyName
, COUNT(ord.OrderID) AS Orders
FROM Northwind.Customers AS c
LEFT JOIN Northwind.Orders AS ord
ON c.CustomerID = ord.CustomerID
GROUP BY c.CustomerID
ORDER BY Orders;

-- 9.1 ��������� ���� ����������� ������� CompanyName � ������� Suppliers, � ������� ��� ���� �� 
-- ������ �������� �� ������ (UnitsInStock � ������� Products ����� 0). ������������ ��������� SELECT 
-- ��� ����� ������� � �������������� ��������� IN. ����� �� ������������ ������ ��������� IN
-- �������� '='? 

SELECT sup.CompanyName
FROM Northwind.Suppliers AS sup
WHERE sup.SupplierID IN
(SELECT p.SupplierID
FROM Northwind.Products AS p
WHERE p.UnitsInStock = 0);

-- ������������ �������� '=' �����, �� ��� ����� ��������� ������ ���������� �� ��������� �������,
-- � ���� �������� (��������, 1 ��� 0). �� � ���� ��� ������.

-- 10.1 ��������� ���� ���������, ������� ����� ����� 150 �������. 
-- ������������ ��������� ��������������� SELECT.

SELECT emp.LastName
FROM Northwind.Employees AS emp
WHERE
(SELECT COUNT(ord.OrderID) 
FROM Northwind.Orders AS ord
WHERE ord.EmployeeID = emp.EmployeeID) > 150;

-- 11.1 ��������� ���� ���������� (������� Customers), ������� �� ����� �� ������ ������ 
-- (��������� �� ������� Orders). ������������ ��������������� SELECT � �������� EXISTS.

SELECT c.CompanyName
FROM Northwind.Customers AS c
WHERE NOT EXISTS
(SELECT ord.OrderID
FROM Northwind.Orders AS ord
WHERE ord.CustomerID = c.CustomerID);

-- 12.1 ��� ������������ ����������� ��������� Employees ��������� �� ������� Employees ������ 
-- ������ ��� ���� ��������, � ������� ���������� ������� Employees (������� LastName ) �� ���� 
-- �������. ���������� ������ ������ ���� ������������ �� �����������.

SELECT SUBSTRING(emp.LastName, 1, 1) AS Letters
FROM Northwind.Employees AS emp
ORDER BY Letters;

-- 13.1 �������� ���������, ������� ���������� ����� ������� ����� ��� ������� �� ��������� �� ������������ ���. 
-- � ����������� �� ����� ���� ��������� ������� ������ ��������, ������ ���� ������ ���� � ����� �������. 
-- � ����������� ������� ������ ���� �������� ��������� �������: ������� � ������ � �������� �������� 
-- (FirstName � LastName � ������: Nancy Davolio), ����� ������ � ��� ���������. � ������� ���� ��������� 
-- Discount ��� ������� �������. ��������� ���������� ���, �� ������� ���� ������� �����, � ���������� ������������ 
-- �������. ���������� ������� ������ ���� ����������� �� �������� ����� ������. ��������� ������ ���� ����������� 
-- � �������������� ��������� SELECT � ��� ������������� ��������. �������� ������� �������������� GreatestOrders. 
-- ���������� ������������������ ������������� ���� ��������. ����� ������ ������������ ������� �������� � ������� 
-- Query.sql ���� �������� ��������� �������������� ����������� ������ ��� ������������ ������������ ������ ��������� 
-- GreatestOrders. ����������� ������ ������ �������� � ������� ��� ��������� � ������������ ������ �������� ���� ��� 
-- ������������� �������� ��� ���� ��� ������� �� ������������ ��������� ��� � ����������� ��������� �������: ��� 
-- ��������, ����� ������, ����� ������. ����������� ������ �� ������ ��������� ������, ���������� � ���������, - ��
-- ������ ��������� ������ ��, ��� ������� � ����������� �� ����. ��� ������� �� ������ �������� ������ ���� �������� 
-- � ����� Query.sql � ��. ��������� ���� � ������� ����������� � �����������.

EXEC GreatestOrders
@year = 1997,
@number = 3;

-- ����������� ������

-- ID Margaret Peacock
DECLARE @employeeID int = 4;
DECLARE @year int = 1997;

SELECT emp.EmployeeID AS EmployeeID
, o.OrderID
, ROUND(SUM(od.Quantity * (od.UnitPrice - od.UnitPrice * od.Discount)), 3) AS Cost
FROM Northwind.Employees AS emp
JOIN Northwind.Orders AS o
ON emp.EmployeeID = o.EmployeeID
JOIN Northwind.[Order Details] AS od
ON o.OrderID = od.OrderID
WHERE YEAR(o.OrderDate) = @year AND emp.EmployeeID = @employeeID
GROUP BY o.OrderID, emp.EmployeeID
ORDER BY Cost DESC;

-- 13.2 �������� ���������, ������� ���������� ������ � ������� Orders, �������� ���������� ����� �������� 
-- � ���� (������� ����� OrderDate � ShippedDate). � ����������� ������ ���� ���������� ������, ���� �������
-- ��������� ���������� �������� ��� ��� �������������� ������. �������� �� ��������� ��� ������������� �����
-- 35 ����. �������� ��������� ShippedOrdersDiff. ��������� ������ ����������� ��������� �������: OrderID, 
-- OrderDate, ShippedDate, ShippedDelay (�������� � ���� ����� ShippedDate � OrderDate), SpecifiedDelay 
-- (���������� � ��������� ��������). ���������� ������������������ ������������� ���� ���������.

EXEC ShippedOrdersDiff
@specifiedDelay = 30;

-- 13.3 �������� ���������, ������� ����������� ���� ����������� ��������� ��������, ��� ����������������, 
-- ��� � ����������� ��� �����������. � �������� �������� ��������� ������� ������������ EmployeeID. ���������� 
-- ����������� ����� ����������� � ��������� �� � ������ (������������ �������� PRINT) �������� �������� ����������. 
-- ��������, ��� �������� ���� ����� ����������� ����� ������ ���� ��������. �������� ��������� SubordinationInfo. 
-- � �������� ��������� ��� ������� ���� ������ ���� ������������ ������, ����������� � Books Online � ��������������� 
-- Microsoft ��� ������� ��������� ���� �����. ������������������ ������������� ���������.

EXEC SubordinationInfo
@employeeID = 2;

-- 13.4 �������� �������, ������� ����������, ���� �� � �������� �����������. ���������� ��� ������ BIT. 
-- � �������� �������� ��������� ������� ������������ EmployeeID. �������� ������� IsBoss. ������������������ 
-- ������������� ������� ��� ���� ��������� �� ������� Employees.

SELECT emp.EmployeeID
, emp.ReportsTo
, dbo.IsBoss(emp.EmployeeID) AS IsBoss
FROM Northwind.Employees AS emp;