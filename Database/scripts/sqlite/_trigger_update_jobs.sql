CREATE TRIGGER tr_queue_update_jobs
UPDATE OF status ON queue_sequences 
BEGIN
 UPDATE queue_jobs
 SET   number     = NEW.number
      ,status     = NEW.status
      ,station    = NEW.station
      ,post       = NEW.post
      ,source     = NEW.source
      ,date       = NEW.date
      ,starttime  = NEW.starttime
      ,calltime   = NEW.calltime
      ,call2time  = NEW.call2time
      ,endtime    = NEW.endtime
WHERE id = NEW.id;
END