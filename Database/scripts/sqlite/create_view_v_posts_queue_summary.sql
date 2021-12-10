CREATE VIEW v_posts_queue_summary
AS 
SELECT name, numberprefix, keterangan
,( SELECT MAX(number)   FROM sequences WHERE status = 'PROCESS' AND post = name AND [date] = date('now','localtime') ) AS called_last
,( SELECT COUNT(number) FROM jobs      WHERE status = 'PROCESS' AND post = name AND [date] = date('now','localtime') ) AS called_total
,( SELECT MIN(number)   FROM sequences WHERE status = 'WAITING' AND post = name AND [date] = date('now','localtime') ) AS waiting_first
,( SELECT MAX(number)   FROM sequences WHERE status = 'WAITING' AND post = name AND [date] = date('now','localtime') ) AS waiting_last
,( SELECT COUNT(number) FROM sequences WHERE status = 'WAITING' AND post = name AND [date] = date('now','localtime') ) AS waiting_total
FROM posts