CREATE TRIGGER tr_queue_update_jobs
ON queue_sequences
FOR UPDATE AS
BEGIN
 SET NOCOUNT ON
 UPDATE queue_jobs
 SET   number     = i.number
      ,status     = i.status
      ,station    = i.station
      ,post       = i.post
      ,source     = i.source
      ,date       = i.date
      ,starttime  = i.starttime
      ,calltime   = i.calltime
      ,call2time  = i.call2time
      ,endtime    = i.endtime
 FROM INSERTED as i
 INNER JOIN queue_jobs on queue_jobs.id = i.id
END;