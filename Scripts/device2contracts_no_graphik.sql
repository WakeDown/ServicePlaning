
SELECT  (select name_inn FROM dbo.get_contractor(cc.id_contractor)) AS ctr, number, dd.serial_num, (SELECT name FROM dbo.srvpl_service_intervals si WHERE si.id_service_interval = c2dd.id_service_interval) AS interval 
FROM (
SELECT c2d.id_contract, c2d.id_device 
FROM dbo.srvpl_contract2devices c2d
INNER JOIN dbo.srvpl_contracts c ON c2d.id_contract = c.id_contract
INNER JOIN dbo.srvpl_devices d ON c2d.id_device = d.id_device
--INNER JOIN dbo.srvpl_service_claims sc ON c2d.id_contract2devices = sc.id_contract2devices
WHERE c2d.enabled = 1 AND c.enabled = 1 AND d.enabled = 1 
AND not EXISTS(SELECT 1 FROM dbo.srvpl_service_claims sc WHERE sc.enabled = 1 and c2d.id_contract2devices = sc.id_contract2devices)
--AND sc.enabled = 1
AND dbo.srvpl_fnc('checkContractIsActiveNow', NULL, c2d.id_contract, NULL, NULL) = '1'
GROUP BY c2d.id_contract, c2d.id_device) AS t
INNER JOIN dbo.srvpl_contract2devices c2dd ON t.id_contract = c2dd.id_contract AND t.id_device = c2dd.id_device AND c2dd.enabled = 1
INNER JOIN dbo.srvpl_contracts cc ON t.id_contract = cc.id_contract
INNER JOIN dbo.srvpl_devices dd ON t.id_device = dd.id_device
ORDER BY ctr, dd.serial_num, interval