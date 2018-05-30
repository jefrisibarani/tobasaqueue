USE [antri]
GO

CREATE TRIGGER [tr_sequences] 
ON [sequences]
FOR INSERT AS

	DECLARE @id int;
	DECLARE @number int;
	DECLARE @status nchar(10);
	DECLARE @station nchar(32);
	DECLARE @post nchar(32);
	DECLARE @source nchar(32);
	DECLARE @date date;
	DECLARE @starttime datetime;
		
	SELECT @id        = i.id from inserted i;	
	SELECT @number    = i.number from inserted i;	
	SELECT @status    = i.status from inserted i;
	SELECT @station   = i.station from inserted i;
	SELECT @post      = i.post from inserted i;
	SELECT @source	  = i.source from inserted i;
	SELECT @date      = i.date from inserted i;
	SELECT @starttime = i.starttime from inserted i;
BEGIN
PRINT 'AFTER INSERT trigger fired.'
	INSERT INTO jobs
	(
		 [id]
		,[number]
		,[status]
		,[station]
		,[post]
		,[source]
		,[date]
		,[starttime]
	)
	VALUES 
	(
		 @id
		,@number
		,@status
		,@station
		,@post
		,@source
		,@date
		,@starttime
	)

END

GO


