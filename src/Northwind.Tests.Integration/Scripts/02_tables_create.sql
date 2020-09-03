CREATE TABLE IF NOT EXISTS Categories
(
  CategoryId   INTEGER,
  CategoryName VARCHAR(8000),
  Description  VARCHAR(8000),
  PRIMARY KEY (CategoryId AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS Customers
(
  CustomerId   VARCHAR(8000),
  CompanyName  VARCHAR(8000),
  ContactName  VARCHAR(8000),
  ContactTitle VARCHAR(8000),
  Address      VARCHAR(8000),
  City         VARCHAR(8000),
  Region       VARCHAR(8000),
  PostalCode   VARCHAR(8000),
  Country      VARCHAR(8000),
  Phone        VARCHAR(8000),
  Fax          VARCHAR(8000),
  PRIMARY KEY (CustomerId)
);
CREATE TABLE IF NOT EXISTS Employees
(
  EmployeeId      INTEGER,
  LastName        VARCHAR(8000),
  FirstName       VARCHAR(8000),
  Title           VARCHAR(8000),
  TitleOfCourtesy VARCHAR(8000),
  BirthDate       VARCHAR(8000),
  HireDate        VARCHAR(8000),
  Address         VARCHAR(8000),
  City            VARCHAR(8000),
  Region          VARCHAR(8000),
  PostalCode      VARCHAR(8000),
  Country         VARCHAR(8000),
  HomePhone       VARCHAR(8000),
  Extension       VARCHAR(8000),
  Photo           BLOB,
  Notes           VARCHAR(8000),
  ReportsTo       INTEGER,
  PhotoPath       VARCHAR(8000),
  PRIMARY KEY (EmployeeId AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS Orders
(
  OrderId         INTEGER,
  CustomerId      VARCHAR(8000),
  EmployeeId      INTEGER NOT NULL,
  OrderDate       VARCHAR(8000),
  RequiredDate    VARCHAR(8000),
  ShippedDate     VARCHAR(8000),
  ShipVia         INTEGER,
  Freight         DECIMAL NOT NULL,
  ShipName        VARCHAR(8000),
  ShipAddress     VARCHAR(8000),
  ShipCity        VARCHAR(8000),
  ShipRegion      VARCHAR(8000),
  ShipPostalCode  VARCHAR(8000),
  ShipCountry     VARCHAR(8000),
  OrderDateBackup VARCHAR(23),
  PRIMARY KEY (OrderId AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS Products
(
  ProductId       INTEGER,
  ProductName     VARCHAR(8000),
  SupplierId      INTEGER NOT NULL,
  CategoryId      INTEGER NOT NULL,
  QuantityPerUnit VARCHAR(8000),
  UnitPrice       DECIMAL NOT NULL,
  UnitsInStock    INTEGER NOT NULL,
  UnitsOnOrder    INTEGER NOT NULL,
  ReorderLevel    INTEGER NOT NULL,
  Discontinued    INTEGER NOT NULL,
  PRIMARY KEY (ProductId AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS Regions
(
  RegionId          INTEGER,
  RegionDescription VARCHAR(8000),
  PRIMARY KEY (RegionId AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS Shippers
(
  ShipperId   INTEGER,
  CompanyName VARCHAR(8000),
  Phone       VARCHAR(8000),
  PRIMARY KEY (ShipperId AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS Suppliers
(
  SupplierId   INTEGER,
  CompanyName  VARCHAR(8000),
  ContactName  VARCHAR(8000),
  ContactTitle VARCHAR(8000),
  Address      VARCHAR(8000),
  City         VARCHAR(8000),
  Region       VARCHAR(8000),
  PostalCode   VARCHAR(8000),
  Country      VARCHAR(8000),
  Phone        VARCHAR(8000),
  Fax          VARCHAR(8000),
  HomePage     VARCHAR(8000),
  PRIMARY KEY (SupplierId AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS Territories
(
  TerritoryId          INTEGER,
  TerritoryDescription VARCHAR(8000),
  RegionId             INTEGER NOT NULL,
  PRIMARY KEY (TerritoryId AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS OrderDetails
(
  OrderId   INTEGER NOT NULL,
  ProductId INTEGER NOT NULL,
  UnitPrice DECIMAL NOT NULL,
  Quantity  INTEGER NOT NULL,
  Discount  DOUBLE  NOT NULL,
  PRIMARY KEY (OrderId, ProductId)
);
CREATE TABLE IF NOT EXISTS EmployeeTerritories
(
  EmployeeId  INTEGER NOT NULL,
  TerritoryId VARCHAR(8000),
  PRIMARY KEY (EmployeeId, TerritoryId)
);

CREATE TABLE IF NOT EXISTS Users
(
  Id       INTEGER NOT NULL,
  Username TEXT    NOT NULL UNIQUE,
  Password TEXT    NOT NULL,
  PRIMARY KEY (Id AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS Roles
(
  Id   INTEGER NOT NULL,
  Name TEXT    NOT NULL UNIQUE,
  PRIMARY KEY (Id AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS UserRoles
(
  UserId INTEGER NOT NULL,
  RoleId INTEGER NOT NULL,
  PRIMARY KEY (UserId, RoleId)
);

CREATE VIEW IF NOT EXISTS ProductDetailsView AS
SELECT
  p.ProductId,
  p.ProductName,
  p.SupplierId,
  p.CategoryId,
  p.QuantityPerUnit,
  p.UnitPrice,
  p.UnitsInStock,
  p.UnitsOnOrder,
  p.ReorderLevel,
  p.Discontinued,
  c.CategoryName,
  c.Description AS CategoryDescription,
  s.CompanyName AS SupplierName,
  s.Region AS SupplierRegion
FROM Products p
  JOIN Categories c ON p.CategoryId = c.CategoryId
  JOIN Suppliers s ON s.SupplierId = p.SupplierId;
