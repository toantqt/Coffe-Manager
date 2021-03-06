USE [master]
GO
/****** Object:  Database [QuanLyQuanCafe]    Script Date: 06/29/2020 13:39:50 ******/
CREATE DATABASE [QuanLyQuanCafe] ON  PRIMARY 
( NAME = N'QuanLyQuanCafe', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\QuanLyQuanCafe.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QuanLyQuanCafe_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\QuanLyQuanCafe_log.LDF' , SIZE = 768KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QuanLyQuanCafe] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyQuanCafe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULLS OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_PADDING OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET ARITHABORT OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_CLOSE ON
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [QuanLyQuanCafe] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET  ENABLE_BROKER
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [QuanLyQuanCafe] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [QuanLyQuanCafe] SET  READ_WRITE
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECOVERY SIMPLE
GO
ALTER DATABASE [QuanLyQuanCafe] SET  MULTI_USER
GO
ALTER DATABASE [QuanLyQuanCafe] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [QuanLyQuanCafe] SET DB_CHAINING OFF
GO
USE [QuanLyQuanCafe]
GO
/****** Object:  Table [dbo].[FoodCategory]    Script Date: 06/29/2020 13:39:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 06/29/2020 13:39:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[PassWord] [nvarchar](100) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableFood]    Script Date: 06/29/2020 13:39:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableFood](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[UpdateAccount]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[UpdateAccount] @userName nvarchar(100), @displayName nvarchar(100), @passWord nvarchar(100), @newPassWord nvarchar(100)
as
begin
	declare @isRightPass int = 0
	select @isRightPass = count(*) from Account where UserName = @userName and PassWord = @passWord
	if(@isRightPass = 1)
	begin
		if(@newPassWord = null or @newPassWord = '')
		begin
			update Account set DisplayName = @displayName where UserName = @userName
		end
		else
			update Account set DisplayName = @displayName, PassWord = @newPassWord where UserName = @userName
	end
end
GO
/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Login] @username nvarchar(100), @password nvarchar(100)
as
begin
	select * from Account where username=@username and password=@password
end
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateCheckIn] [date] NOT NULL,
	[DateCheckOut] [date] NULL,
	[idTable] [int] NOT NULL,
	[status] [int] NOT NULL,
	[discount] [int] NULL,
	[totalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[idCategory] [int] NOT NULL,
	[price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetTableList]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetTableList]
as select * from TableFood
GO
/****** Object:  StoredProcedure [dbo].[GetAccountByUsername]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetAccountByUsername] @username nvarchar(100)
as
begin
	select * from Account where username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[InsertBill]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[InsertBill] @idTable int
as
begin
	insert Bill(DateCheckIn, DateCheckOut, idTable, status, discount)
	values (GETDATE(), null, @idTable, 0, 0)
end
GO
/****** Object:  StoredProcedure [dbo].[GetListBillDate]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetListBillDate] @dateCheckIn date, @dateCheckOut date
as
begin
	select b.name as [Tên bàn], a.totalPrice as [Tổng tiền], a.DateCheckIn as [Ngày vào], a.DateCheckOut as [Ngày ra], a.discount as [Giảm giá (%)] 
	from Bill a, TableFood b 
	where a.idTable = b.id and a.DateCheckIn >= @dateCheckIn and a.DateCheckOut <= @dateCheckOut and a.status = 1
end
GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idBill] [int] NOT NULL,
	[idFood] [int] NOT NULL,
	[count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertBillInfo]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[InsertBillInfo] @idBill int, @idFood int, @count int
as
begin
	declare @isExistBillInfo int
	declare @foodCount int = 1
	select @isExistBillInfo = id , @foodCount = count FROM BillInfo WHERE idBill = @idBill and idFood = @idFood
	if(@isExistBillInfo > 0)
		begin
			declare @newCount int = @foodCount + @count
			if(@newCount > 0)
				update BillInfo set count = @newCount where idFood = @idFood
			else
				delete BillInfo where idBill = @idBill and idFood = @idFood
		end
	else
		begin
			insert into BillInfo(idBill,idFood,count) values (@idBill,@idFood,@count)
		end
end
GO
/****** Object:  StoredProcedure [dbo].[SwitchTable]    Script Date: 06/29/2020 13:39:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SwitchTable] @idTable1 int, @idTable2 int 
as
begin
	declare @idFirstBill int
	declare @idSecondBill int

	declare @isFirstTableEmty int = 1
	declare @isSecondTableEmty int = 1

	SELECT @idFirstBill = id FROM Bill WHERE idTable = @idTable1 AND status = 0
	SELECT @idSecondBill = id FROM Bill WHERE idTable = @idTable2 AND status = 0
	if(@idFirstBill is null)
		begin
			insert into Bill(DateCheckIn,DateCheckOut,idTable,status) values (GETDATE(),null,@idTable1,0)
			select @idFirstBill = max(id) from Bill where idTable = @idTable1 and status = 0
		end

	select @isFirstTableEmty = count(*) from BillInfo where idBill = @idFirstBill

	if(@idSecondBill is null)
		begin
			insert into Bill(DateCheckIn,DateCheckOut,idTable,status) values (GETDATE(),null,@idTable2,0)
			select @idSecondBill = max(id) from Bill where idTable = @idTable2 and status = 0
		end

	select @isSecondTableEmty = count(*) from BillInfo where idBill = @idSecondBill

	select id into IDBillIntoTable from BillInfo where idBill = @idSecondBill
	update BillInfo set idBill = @idSecondBill where idBill = @idFirstBill
	update BillInfo set idBill = @idFirstBill where id in (select * from IDBillIntoTable)
	drop table IDBillIntoTable

	if(@isFirstTableEmty = 0)
		update TableFood set status = N'Trống' where id = @idTable2

	if(@isSecondTableEmty = 0)
		update TableFood set status = N'Trống' where id = @idTable1
end
GO
/****** Object:  Default [DF__Account__Type__014935CB]    Script Date: 06/29/2020 13:39:51 ******/
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [Type]
GO
/****** Object:  Default [DF__TableFood__statu__0F975522]    Script Date: 06/29/2020 13:39:51 ******/
ALTER TABLE [dbo].[TableFood] ADD  DEFAULT (N'Trống') FOR [status]
GO
/****** Object:  Default [DF__Bill__DateCheckI__145C0A3F]    Script Date: 06/29/2020 13:39:54 ******/
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [DateCheckIn]
GO
/****** Object:  Default [DF__Bill__status__15502E78]    Script Date: 06/29/2020 13:39:54 ******/
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [status]
GO
/****** Object:  Default [DF__Bill__discount__164452B1]    Script Date: 06/29/2020 13:39:54 ******/
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [discount]
GO
/****** Object:  Default [DF__Bill__totalPrice__173876EA]    Script Date: 06/29/2020 13:39:54 ******/
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [totalPrice]
GO
/****** Object:  Default [DF__Food__price__09DE7BCC]    Script Date: 06/29/2020 13:39:54 ******/
ALTER TABLE [dbo].[Food] ADD  DEFAULT ((0)) FOR [price]
GO
/****** Object:  Default [DF__BillInfo__count__1CF15040]    Script Date: 06/29/2020 13:39:54 ******/
ALTER TABLE [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [count]
GO
/****** Object:  ForeignKey [FK__Bill__idTable__182C9B23]    Script Date: 06/29/2020 13:39:54 ******/
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([idTable])
REFERENCES [dbo].[TableFood] ([id])
GO
/****** Object:  ForeignKey [FK__Food__idCategory__0AD2A005]    Script Date: 06/29/2020 13:39:54 ******/
ALTER TABLE [dbo].[Food]  WITH CHECK ADD FOREIGN KEY([idCategory])
REFERENCES [dbo].[FoodCategory] ([id])
GO
/****** Object:  ForeignKey [FK__BillInfo__idBill__1DE57479]    Script Date: 06/29/2020 13:39:54 ******/
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idBill])
REFERENCES [dbo].[Bill] ([id])
GO
/****** Object:  ForeignKey [FK__BillInfo__idFood__1ED998B2]    Script Date: 06/29/2020 13:39:54 ******/
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idFood])
REFERENCES [dbo].[Food] ([id])
GO
