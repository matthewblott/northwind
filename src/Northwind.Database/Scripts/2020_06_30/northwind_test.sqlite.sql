BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Categories"
(
  "CategoryId"   INTEGER,
  "CategoryName" TEXT,
  "Description"  TEXT,
  PRIMARY KEY ("CategoryId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Customers"
(
  "CustomerId"   TEXT,
  "CompanyName"  TEXT,
  "ContactName"  TEXT,
  "ContactTitle" TEXT,
  "Address"      TEXT,
  "City"         TEXT,
  "Region"       TEXT,
  "PostalCode"   TEXT,
  "Country"      TEXT,
  "Phone"        TEXT,
  "Fax"          TEXT,
  PRIMARY KEY ("CustomerId")
);
CREATE TABLE IF NOT EXISTS "Employees"
(
  "EmployeeId"      INTEGER,
  "LastName"        TEXT,
  "FirstName"       TEXT,
  "Title"           TEXT,
  "TitleOfCourtesy" TEXT,
  "BirthDate"       TEXT,
  "HireDate"        TEXT,
  "Address"         TEXT,
  "City"            TEXT,
  "Region"          TEXT,
  "PostalCode"      TEXT,
  "Country"         TEXT,
  "HomePhone"       TEXT,
  "Extension"       TEXT,
  "Photo"           BLOB,
  "Notes"           TEXT,
  "ReportsTo"       INTEGER,
  "PhotoPath"       TEXT,
  PRIMARY KEY ("EmployeeId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Orders"
(
  "OrderId"         INTEGER,
  "CustomerId"      TEXT,
  "EmployeeId"      INTEGER NOT NULL,
  "OrderDate"       TEXT,
  "RequiredDate"    TEXT,
  "ShippedDate"     TEXT,
  "ShipVia"         INTEGER,
  "Freight"         REAL NOT NULL,
  "ShipName"        TEXT,
  "ShipAddress"     TEXT,
  "ShipCity"        TEXT,
  "ShipRegion"      TEXT,
  "ShipPostalCode"  TEXT,
  "ShipCountry"     TEXT,
  "OrderDateBackup" TEXT,
  PRIMARY KEY ("OrderId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Products"
(
  "ProductId"       INTEGER,
  "ProductName"     TEXT,
  "SupplierId"      INTEGER NOT NULL,
  "CategoryId"      INTEGER NOT NULL,
  "QuantityPerUnit" TEXT,
  "UnitPrice"       REAL NOT NULL,
  "UnitsInStock"    INTEGER NOT NULL,
  "UnitsOnOrder"    INTEGER NOT NULL,
  "ReorderLevel"    INTEGER NOT NULL,
  "Discontinued"    INTEGER NOT NULL,
  PRIMARY KEY ("ProductId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Regions"
(
  "RegionId"          INTEGER,
  "RegionDescription" TEXT,
  PRIMARY KEY ("RegionId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Shippers"
(
  "ShipperId"   INTEGER,
  "CompanyName" TEXT,
  "Phone"       TEXT,
  PRIMARY KEY ("ShipperId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Suppliers"
(
  "SupplierId"   INTEGER,
  "CompanyName"  TEXT,
  "ContactName"  TEXT,
  "ContactTitle" TEXT,
  "Address"      TEXT,
  "City"         TEXT,
  "Region"       TEXT,
  "PostalCode"   TEXT,
  "Country"      TEXT,
  "Phone"        TEXT,
  "Fax"          TEXT,
  "HomePage"     TEXT,
  PRIMARY KEY ("SupplierId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Territories"
(
  "TerritoryId"          INTEGER,
  "TerritoryDescription" TEXT,
  "RegionId"             INTEGER NOT NULL,
  PRIMARY KEY ("TerritoryId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "OrderDetails"
(
  "OrderId"   INTEGER NOT NULL,
  "ProductId" INTEGER NOT NULL,
  "UnitPrice" REAL NOT NULL,
  "Quantity"  INTEGER NOT NULL,
  "Discount"  REAL  NOT NULL,
  PRIMARY KEY ("OrderId", "ProductId")
);
CREATE TABLE IF NOT EXISTS "EmployeeTerritories"
(
  "EmployeeId"  INTEGER NOT NULL,
  "TerritoryId" TEXT,
  PRIMARY KEY ("EmployeeId", "TerritoryId")
);

CREATE TABLE IF NOT EXISTS "Users"
(
  "Id"       INTEGER NOT NULL,
  "Username" TEXT    NOT NULL UNIQUE,
  "Password" TEXT    NOT NULL,
  PRIMARY KEY ("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Roles"
(
  "Id"   INTEGER NOT NULL,
  "Name" TEXT    NOT NULL UNIQUE,
  PRIMARY KEY ("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "UserRoles"
(
  "UserId" INTEGER NOT NULL,
  "RoleId" INTEGER NOT NULL,
  PRIMARY KEY ("UserId", "RoleId")
);

CREATE VIEW ProductDetailsView as
select p.ProductId,
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
       c.Description as CategoryDescription,
       s.CompanyName as SupplierName,
       s.Region      as SupplierRegion
from "Products" p
       join "Categories" c on p.CategoryId = c.CategoryId
       join "Suppliers" s on s.SupplierId = p.SupplierId;
COMMIT;
