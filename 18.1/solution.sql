


/****** Object:  Table [dbo].[d18]    Script Date: 2022-12-18 15:25:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[d18](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[x] [int] NOT NULL,
	[y] [int] NOT NULL,
	[z] [int] NOT NULL,
 CONSTRAINT [PK_d18] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO







select sum(c)
from
(
select 
	*,
	(
	select 6 - count(*)
	from d18
	 where	((x = m.x - 1 or x = m.x + 1) and m.y = y and m.z = z) or
		((y = m.y - 1 or y = m.y + 1) and m.x = x and m.z = z) or
		((z = m.z - 1 or z = m.z + 1) and m.x = x and m.y = y)
		) c
	from d18 m
) s1

select * from d18