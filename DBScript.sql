
CREATE TABLE [dbo].[HM_ExpensesCategories](
	[ExpenseCategory_id] [nvarchar](10) primary key ,
	[Category_desc] [nvarchar](20) NULL

) 

CREATE TABLE [dbo].[HM_Product_categories](
	[Product_category_id] [int] IDENTITY(1,1) primary key,
	[Product_category_desc] [nvarchar](20) NULL,
	)

CREATE TABLE [dbo].[HM_Units](
	[Unit_id] [int] primary key,
	[Unit_desc] [nvarchar](20) NULL
	)


CREATE TABLE [dbo].[HM_Users](
	[UserId] [int]  primary key IDENTITY(1,1) ,
	[UserName] [nvarchar](15) NULL,
	[UserPassword] [nvarchar](15) NULL,
	[Register_time_stmp] [datetime] DEFAULT (getdate()),
	)

CREATE TABLE [dbo].[HM_Expenses](
	[amount] [nvarchar](30) NULL,
	[comment] [nvarchar](30) NULL,
	[ExpenseCategory_id] [nvarchar](10) NULL,
	[PurchesDate] [datetime] NULL,
	[paymentMethod] [nvarchar](30) NULL,
	[UserId] [int] NULL,
	[time_Stamp] [datetime]  DEFAULT (getdate()),
	constraint [ExpenseCategory_id_FK] foreign key ([ExpenseCategory_id]) references [HM_ExpensesCategories] ,

)


CREATE TABLE [dbo].[HM_List](
	[List_id] [int]  IDENTITY(1,1) primary key,
	[List_name] [nvarchar](20) NULL,
	[UserId] [int] ,
	[Creation_date] [datetime] NULL,
	[Is_Active] [bit]  DEFAULT ((1)),	
	[Purchasing_time_stmp] [datetime]DEFAULT (getdate()),
	[Is_CosntantList] [bit] DEFAULT ((0)),
	
	constraint [UserId_FK] foreign key ([UserId]) references [HM_Users] ,
	)




CREATE TABLE [dbo].[HM_Products](
	[Product_id] [int] primary key IDENTITY(1,1) NOT NULL,
	[Product_desc] [nvarchar](20) NULL,
	[Product_category_id] [int] NULL,
	[IsApproved] [bit] DEFAULT ((1)),
	[UserId] [int] NULL,
	[time_stmp] [datetime] NULL  DEFAULT (getdate()),
	constraint [Product_category_FK] foreign key ([Product_category_id]) references [HM_Product_categories] ,
	constraint [UserId_FK1] foreign key ([UserId]) references [HM_Users] ,

	)

CREATE TABLE [dbo].[HM_Product_list](
	[Product_list_id] [int] IDENTITY(1,1) primary key,
	[List_id] [int] NULL,
	[Product_id] [int] NULL,
	[Product_amount] [int] NULL,
	[Unit_id] [int] NULL,
	[Price] [int] NULL,
	[Comment] [nvarchar](20) NULL,
	[Is_purchased] [bit] DEFAULT ((0)),

    constraint [List_id_FK] foreign key (List_id) references [HM_List] ,
    constraint [Product_id_FK] foreign key ([Product_id]) references HM_Products ,
    constraint [Unit_id_FK] foreign key ([Unit_id]) references HM_Units ,
	)

--	drop table [HM_Product_list]
--	drop table [HM_Products]
--	drop table [HM_List]
--	drop table [HM_Expenses]
--	drop table [HM_Users]  
--drop table [HM_Units]
--drop table [HM_Product_categories]
--drop table [HM_ExpensesCategories]
