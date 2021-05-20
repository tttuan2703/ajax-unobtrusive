CREATE TABLE [dbo].[tbl_Deparment] (
    [DeptId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_Deparment] PRIMARY KEY CLUSTERED ([DeptId] ASC)
);

