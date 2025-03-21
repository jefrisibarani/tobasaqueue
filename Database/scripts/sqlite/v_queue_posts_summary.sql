CREATE VIEW v_queue_posts_summary
AS 
SELECT p.name, p.numberprefix, p.keterangan
,( SELECT MAX(NUMBER)   FROM queue_sequences WHERE status = 'PROCESS' AND post = p.name AND date = date('now','localtime') ) AS called_last
,( SELECT COUNT(number) FROM queue_sequences WHERE status = 'PROCESS' AND post = p.name AND date = date('now','localtime') ) AS called_total
,( SELECT MIN(number)   FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = date('now','localtime') ) AS waiting_first
,( SELECT MAX(number)   FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = date('now','localtime') ) AS waiting_last
,( SELECT COUNT(number) FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = date('now','localtime') ) AS waiting_total
FROM queue_posts p;