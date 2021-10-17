CREATE TABLE [dbo].[temp_U_Data_1](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Address] [varchar](max) NULL,
	[Types] [varchar](250) NULL,
	[Date] [date] NOT NULL,
	[Time] [time](7) NOT NULL,
	[Remarks] [varchar](250) NULL,
 CONSTRAINT [PK_temp_U_Data_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


CREATE TABLE [dbo].[U_Data_1](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Address] [varchar](max) NULL,
	[Types] [varchar](250) NULL,
	[Date] [date] NOT NULL,
	[Time] [time](7) NOT NULL,
	[Remarks] [varchar](250) NULL,
 CONSTRAINT [PK_U_Data_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO