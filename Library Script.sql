USE [libery]
GO
/****** Object:  UserDefinedFunction [dbo].[due_date]    Script Date: 07-08-2024 22:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   function [dbo].[due_date] (@date date)
returns DATE
as
BEGIN
    DECLARE @due date
    set @due = DATEADD (DAY,+7,@date)
    RETURN @due
END;
GO
/****** Object:  Table [dbo].[admin]    Script Date: 07-08-2024 22:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NULL,
	[email] [nvarchar](30) NULL,
	[password] [nvarchar](20) NULL,
 CONSTRAINT [pk_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[books]    Script Date: 07-08-2024 22:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id]  AS ('BOOK'+right('000'+CONVERT([varchar](10),[id]),(3))) PERSISTED,
	[book_name] [varchar](30) NULL,
	[quantity] [int] NULL,
 CONSTRAINT [pk_bookid] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[new_user]    Script Date: 07-08-2024 22:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[new_user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id]  AS ('USER'+right('000'+CONVERT([varchar](10),[id]),(3))) PERSISTED,
	[name] [varchar](20) NULL,
	[age] [int] NULL,
	[email] [nvarchar](30) NULL,
	[password] [nvarchar](20) NULL,
 CONSTRAINT [pk_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[readers]    Script Date: 07-08-2024 22:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[readers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[red_name] [varchar](30) NULL,
	[email] [nvarchar](30) NULL,
	[book_id] [int] NULL,
	[out_date] [date] NULL,
	[in_date] [date] NULL,
 CONSTRAINT [pk_readid] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[admin] ON 

INSERT [dbo].[admin] ([id], [name], [email], [password]) VALUES (1, N'nandha', N'nandha@gmail.com', N'nandha@123')
INSERT [dbo].[admin] ([id], [name], [email], [password]) VALUES (2, N'jega', N'jega@gmail.com', N'jega@123')
INSERT [dbo].[admin] ([id], [name], [email], [password]) VALUES (3, N'kanna', N'kanna@gmail.com', N'kanna@123')
SET IDENTITY_INSERT [dbo].[admin] OFF
GO
SET IDENTITY_INSERT [dbo].[books] ON 

INSERT [dbo].[books] ([id], [book_name], [quantity]) VALUES (1, N'book1', 5)
INSERT [dbo].[books] ([id], [book_name], [quantity]) VALUES (2, N'book2', 2)
INSERT [dbo].[books] ([id], [book_name], [quantity]) VALUES (3, N'book3', 3)
INSERT [dbo].[books] ([id], [book_name], [quantity]) VALUES (4, N'book4', 1)
INSERT [dbo].[books] ([id], [book_name], [quantity]) VALUES (5, N'book5', 5)
INSERT [dbo].[books] ([id], [book_name], [quantity]) VALUES (6, N'book6', 4)
INSERT [dbo].[books] ([id], [book_name], [quantity]) VALUES (7, N'book7', 6)
INSERT [dbo].[books] ([id], [book_name], [quantity]) VALUES (8, N'book8', 2)
INSERT [dbo].[books] ([id], [book_name], [quantity]) VALUES (9, N'book9', 1)
INSERT [dbo].[books] ([id], [book_name], [quantity]) VALUES (10, N'book10', 3)
SET IDENTITY_INSERT [dbo].[books] OFF
GO
SET IDENTITY_INSERT [dbo].[new_user] ON 

INSERT [dbo].[new_user] ([id], [name], [age], [email], [password]) VALUES (1, N'vivin', 24, N'vivin@gmail', N'vivin@123')
SET IDENTITY_INSERT [dbo].[new_user] OFF
GO
SET IDENTITY_INSERT [dbo].[readers] ON 

INSERT [dbo].[readers] ([id], [red_name], [email], [book_id], [out_date], [in_date]) VALUES (1, N'vivin', N'vivin@gmail.com', 1, CAST(N'2024-08-07' AS Date), CAST(N'2024-08-14' AS Date))
SET IDENTITY_INSERT [dbo].[readers] OFF
GO
ALTER TABLE [dbo].[readers]  WITH CHECK ADD  CONSTRAINT [fk_bookid] FOREIGN KEY([book_id])
REFERENCES [dbo].[books] ([id])
GO
ALTER TABLE [dbo].[readers] CHECK CONSTRAINT [fk_bookid]
GO
/****** Object:  StoredProcedure [dbo].[login_details]    Script Date: 07-08-2024 22:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   PROCEDURE [dbo].[login_details] @email NVARCHAR(30),@pass NVARCHAR(20)
AS
SELECT * from admin
WHERE email = @email AND [password]= @pass
GO
/****** Object:  StoredProcedure [dbo].[return_details]    Script Date: 07-08-2024 22:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   PROCEDURE [dbo].[return_details] @book_name NVARCHAR(30),@name NVARCHAR(20)
AS
SELECT book_name, red_name from books
join readers on books.id = readers.id
WHERE book_name = @book_name AND red_name= @name
GO
