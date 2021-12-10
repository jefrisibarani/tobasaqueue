CREATE TRIGGER tr_update_jobs
UPDATE OF status ON sequences 
BEGIN
 UPDATE jobs
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