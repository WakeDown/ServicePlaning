--SELECT ddd.id_device, ddd.serial_num,ccc.id_contract, ccc.number FROM  c2d
--INNER JOIN [unit_prog].[dbo].[srvpl_devices] ddd ON ddd.id_device = c2d.id_device
--INNER JOIN [unit_prog].[dbo].[srvpl_contracts] ccc ON ccc.id_contract = c2d.id_contract
--WHERE c2d.id_device in 

SELECT dd.id_device
 --dd.serial_num
 --, count(1) AS cnt
FROM [unit_prog].[dbo].[srvpl_devices] dd
WHERE dd.serial_num IN 
(SELECT serial_num
 FROM (SELECT serial_num, COUNT(1) AS cnt
  FROM [unit_prog].[dbo].[srvpl_devices] d
  WHERE d.enabled = 1
  GROUP BY d.serial_num) AS t
  WHERE cnt > 1) 
  AND not EXISTS (SELECT 1 from [unit_prog].[dbo].srvpl_contract2devices c2d WHERE c2d.id_device = dd.id_device AND c2d.ENABLED = 1)
  AND dd.enabled = 1
  --AND serial_num != N'W512J302028'
  --group by dd.serial_num
  --) ttt WHERE ttt.cnt > 1
  --order by dd.serial_num

  