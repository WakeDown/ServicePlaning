DECLARE @date DATETIME, @counter INT, @counter_colour INT, @id_device INT, @id_contract INT

declare curs cursor for
SELECT c.date_came, c.counter, c.counter_colour, cl.id_device, cl.id_contract
FROM dbo.srvpl_service_cames c 
INNER JOIN dbo.srvpl_service_claims cl ON c.id_service_claim = cl.id_service_claim
INNER JOIN dbo.srvpl_contracts ctr ON cl.id_contract = ctr.id_contract 
WHERE c.enabled = 1 AND cl.enabled = 1 AND ctr.enabled = 1

OPEN curs

                                FETCH NEXT
						FROM curs
						INTO @date, @counter, @counter_colour, @id_device, @id_contract

WHILE @@FETCH_STATUS = 0
                                    BEGIN
                             EXEC ui_service_planing @action = 'setDeviceData', @planing_date = @date, @counter = @counter, @counter_colour = @counter_colour,@id_device = @id_device,  @id_contract = @id_contract

  FETCH NEXT
						FROM curs
						INTO @date, @counter, @counter_colour, @id_device, @id_contract

end