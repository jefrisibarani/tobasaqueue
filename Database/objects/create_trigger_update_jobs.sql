USE [antri]
GO

CREATE TRIGGER tr_update_jobs
ON sequences
FOR UPDATE AS

BEGIN
 SET NOCOUNT ON
 
 UPDATE jobs
 SET   [number] = i.[number]
      ,[status] = i.[status]
      ,[station] = i.[station]
      ,[post] = i.[post]
      ,[source] = i.[source]
      ,[date] = i.[date]
      ,[starttime] = i.[starttime]
      ,[calltime] = i.[calltime]
      ,[call2time] = i.[call2time]
      ,[endtime] = i.[endtime]
 
 FROM INSERTED as i
 INNER JOIN jobs on jobs.id = i.id
 
END

GO


