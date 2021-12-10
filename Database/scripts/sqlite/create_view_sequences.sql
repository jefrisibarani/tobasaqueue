CREATE VIEW v_sequences
AS 
SELECT seq.id, 
       seq.number, 
       seq.status, 
       seq.station, 
       seq.post, 
       seq.source, 
       seq.starttime, 
       seq.endtime, 
       vsr.numberleft,
       vsr.numbermax
FROM sequences AS seq
JOIN
   (
       SELECT post, MIN(id) AS idmin , COUNT(number) AS numberleft, MAX(number) AS numbermax
       FROM sequences WHERE status = 'WAITING' AND date = date('now','localtime')
       GROUP BY post
   ) AS vsr
ON seq.post = vsr.post
WHERE seq.id = vsr.idmin
AND date = date('now','localtime')