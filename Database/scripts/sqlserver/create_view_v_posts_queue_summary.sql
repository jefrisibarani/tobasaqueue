CREATE VIEW v_posts_queue_summary
AS 
select name,numberprefix,keterangan
,( SELECT MAX(number)   FROM sequences WHERE status = 'PROCESS' AND post = name AND [date] = CAST(getdate()as DATE) ) AS called_last
,( SELECT COUNT(number) FROM jobs      WHERE status = 'PROCESS' AND post = name AND [date] = CAST(getdate()as DATE) ) AS called_total
,( SELECT MIN(number)   FROM sequences WHERE status = 'WAITING' AND post = name AND [date] = CAST(getdate()as DATE) ) AS waiting_first
,( SELECT MAX(number)   FROM sequences WHERE status = 'WAITING' AND post = name AND [date] = CAST(getdate()as DATE) ) AS waiting_last
,( SELECT COUNT(number) FROM sequences WHERE status = 'WAITING' AND post = name AND [date] = CAST(getdate()as DATE) ) AS waiting_total
from posts