CREATE TABLE [dbo].[tbl_Employee] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (50) NULL,
    [Gender] NVARCHAR (3)  NULL,
    [City]   NVARCHAR (20) NULL,
    [Image]  NCHAR (20)    NULL,
    [DeptId] INT           NULL,
    CONSTRAINT [PK_tbl_Employee] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_Employee_tbl_Deparment] FOREIGN KEY ([DeptId]) REFERENCES [dbo].[tbl_Deparment] ([DeptId])
);

