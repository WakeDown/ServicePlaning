select c2d.id_contract2devices, c.number, c.contractor_name, d.serial_num, c2d.address
from dbo.srvpl_contract2devices c2d
LEFT JOIN dbo.srvpl_devices d ON c2d.id_device=d.id_device
LEFT JOIN dbo.srvpl_contracts c ON c2d.id_contract = c.id_contract
WHERE c2d.enabled=1 
AND dbo.srvpl_check_contract_is_active(c2d.id_contract, GETDATE())=1
AND c2d.address IN (N'неизвестно', N'--выберите значение--')