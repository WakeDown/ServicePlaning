SELECT  ddd.id_device ,
        ddd.serial_num ,
        ccc.id_contract ,
        ccc.number
FROM    [unit_prog].[dbo].srvpl_contract2devices c2d
        INNER JOIN [unit_prog].[dbo].[srvpl_devices] ddd ON ddd.id_device = c2d.id_device
        INNER JOIN [unit_prog].[dbo].[srvpl_contracts] ccc ON ccc.id_contract = c2d.id_contract
WHERE   c2d.id_device IN (
        SELECT  dd.id_device
        FROM    [unit_prog].[dbo].[srvpl_devices] dd
        WHERE   dd.serial_num IN (
                SELECT  serial_num
                FROM    ( SELECT    serial_num ,
                                    COUNT(1) AS cnt
                          FROM      [unit_prog].[dbo].[srvpl_devices] d
                          WHERE     d.enabled = 1
                          GROUP BY  d.serial_num
                        ) AS t
                WHERE   cnt > 1 )
                AND dd.enabled = 1 )
        AND c2d.enabled = 1
  
  