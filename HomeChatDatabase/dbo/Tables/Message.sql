CREATE TABLE [dbo].[Message]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SenderId] INT NOT NULL, 
    [ReceiverId] INT NOT NULL, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    [TimeStamp] DATETIME NOT NULL, 
    [IsRead] INT NOT NULL
)
