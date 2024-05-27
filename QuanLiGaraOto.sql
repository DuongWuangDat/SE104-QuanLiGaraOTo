create database QuanLiGaraOto
go
use QuanLiGaraOto
go

create table Customer(
	ID int identity(1,1),
	Name nvarchar(max),
	Address nvarchar(max),
	PhoneNumber varchar(20),
	Email varchar(100),
	IsDeleted bit default 0,
	Constraint PK_Customer Primary key (ID)
)
go

create table BrandCar(
	ID int identity(1,1),
	Name nvarchar(max),
	IsDeleted bit default 0,
	Constraint PK_Brand Primary key (ID)
)
go
create table Reception(
	ID int identity(1,1),
	CustomerID int,
	LicensePlate varchar(20),
	BrandID int,
	Debt money default 0,
	CreatedAt SmallDateTime default GETDATE(),
	IsDeleted bit default 0,
	Constraint PK_Reception Primary key(ID),
	Constraint FK_Recept_Cus Foreign key(CustomerID) references Customer(ID),
	Constraint FK_Recept_Brand Foreign key(BrandID) references BrandCar(ID)
)
go
create table Supplies(
	ID int identity(1,1),
	Name nvarchar(max),
	CountInStock int default 0,
	InputPrices money,
	OutputPrices money,
	IsDeleted bit default 0,
	Constraint PK_Supplies Primary key(ID),
)
go
create table SuppliesInput(
	ID int identity(1,1),
	DateInput SmallDateTime default GETDATE(),
	TotalMoney Money,
	IsDeleted bit default 0,
	Constraint PK_SuppliesIP primary key(ID)
)
go
create table SuppliesInputDetail(
	InputID int,
	SuppliesID int,
	Count int,
	PriceItem Money,
	IsDeleted bit default 0,
	Constraint PK_SuppliesInputDetail primary key (InputID,SuppliesID),
	Constraint FK_SuppliesInputDetail_Input foreign key(InputID) references SuppliesInput(ID),
	Constraint FK_SuppliesInputDetail_Supply foreign key(SuppliesID) references Supplies(ID)
)
go
create table Wage(
	ID int identity(1,1),
	Name nvarchar(max),
	Price money,
	IsDeleted bit default 0,
	Constraint PK_Wage primary key (ID)
)
go
create table Repair(
	ID int identity(1,1),
	ReceptionID int,
	TotalPrice money,
	RepairDate smalldatetime default GETDATE(),
	IsDeleted bit default 0,
	Constraint PK_Repair primary key (ID),
	Constraint FK_Repair_Recept foreign key(ReceptionID) references Reception(ID)
)
go
create table RepairDetail(
	ID int identity(1,1),
	RepairID int,
	WageID int,
	WagePrice Money,
	Content nvarchar(max),
	Price Money,
	IsDeleted bit default 0,
	Constraint PK_RepairDetail primary key(ID),
	Constraint FK_RepairDT_Repair foreign key(RepairID) references Repair(ID),
	CONSTRAINT FK_RepairDT_Wage Foreign key (WageId) REFERENCES Wage(Id)
)
go
create table RepairSuppliesDetail(
	RepairDetailID int,
	SuppliesID int,
	Count int,
	PriceItem money,
	IsDeleted bit default 0,
	Constraint PK_RepairSupDT primary key(RepairDetailID, SuppliesID),
	Constraint FK_RepairSupDT_RepairDT foreign key (RepairDetailID) references RepairDetail(ID),
	Constraint FK_RepairSupDT_Sup foreign key (SuppliesID) references Supplies(ID)
)
go
create table Bill(
	ID int identity(1,1),
	ReceptionID int,
	CreateAt SmallDateTime default GETDATE(),
	Proceeds money,
	IsDeleted bit default 0,
	Constraint PK_Bill primary key (ID) ,
	Constraint FK_Bill_Recept foreign key (ReceptionID) references Reception(ID)
)
go
create table Revenue(
	ID int identity(1,1),
	Month int,
	Year int,
	TotalPrice money default 0,
	IsDeleted bit default 0,
	Constraint PK_Revenue primary key(ID)
)
go
create table RevenueDetail(
	RevenueId int,
	BrandCarId int,
	RepairCount int,
	Ratio float,
	Price money,
	IsDeleted bit default 0,
	Constraint PK_RevenueDT primary key(RevenueID, BrandCarID),
	Constraint FK_RevenueDT_Revenue foreign key(RevenueID) references Revenue(ID),
	Constraint FK_RevenueDT_Brand foreign key(BrandCarID) references BrandCar(ID)
)
go
create table InventoryReport(
	ID int identity(1,1),
	Month int,
	Year int,
	IsDeleted bit default 0,
	constraint PK_Inventory primary key(ID)
)
go
create table InventoryReportDetail(
	InventoryReportID int,
	SuppliesID int,
	TonDau int,
	PhatSinh int,
	TonCuoi int,
	IsDeleted bit default 0,
	constraint PK_InventoryDT primary key (InventoryReportID, SuppliesID),
	constraint FK_InventoryDT_Inventory foreign key(InventoryReportID) references InventoryReport(ID),
	constraint FK_InventoryDT_Supplies foreign key(SuppliesID) references Supplies(ID)
)
go
create table Parameter(
	Name char(100),
	Value float,
	Constraint PK_Parameter primary key (Name)
)