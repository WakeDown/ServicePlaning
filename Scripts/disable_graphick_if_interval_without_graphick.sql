SELECT * 
--update sc
--set enabled = 0
FROM dbo.srvpl_service_claims sc
INNER JOIN dbo.srvpl_contract2devices c2d ON c2d.id_contract2devices = sc.id_contract2devices
--INNER JOIN dbo.srvpl_contracts c ON c2d.id_contract = c.id_contract
INNER JOIN dbo.srvpl_service_intervals si ON c2d.id_service_interval = si.id_service_interval
WHERE sc.enabled = 1 AND c2d.enabled = 1 and si.per_month IS NULL and
(YEAR(sc.planing_date)  > 2014 or (YEAR(sc.planing_date)  = 2014 AND month(sc.planing_date) > 11))