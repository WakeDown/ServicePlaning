UPDATE  dbo.srvpl_service_claims
SET     enabled = 0 ,
        dattim2 = GETDATE()
WHERE   enabled = 1

DECLARE @id_contract2devices INT, @id_creator INT, @lst_schedule_dates NVARCHAR(500), @id_contract INT, @id_device INT, @dstart DATETIME, @dend datetime
SET @id_creator = 816

DECLARE curs CURSOR
FOR
    SELECT  c2d.id_contract2devices, c.date_begin, c.date_end
    FROM    dbo.srvpl_contracts c
            INNER JOIN dbo.srvpl_contract2devices c2d ON c2d.id_contract = c.id_contract
            INNER JOIN dbo.srvpl_service_intervals si ON si.id_service_interval = c2d.id_service_interval
    WHERE   c.enabled = 1
            AND c2d.enabled = 1
            AND si.id_service_interval = 1

OPEN curs
FETCH NEXT
                        
                        FROM curs
				INTO @id_contract2devices, @dstart, @dend
				
WHILE @@FETCH_STATUS = 0
    BEGIN
								
                                SET @lst_schedule_dates =(SELECT CONVERT(NVARCHAR, thedate) + ','
FROM dbo.ExplodeDates(@dstart,@dend) FOR XML PATH(''))

		

		PRINT @lst_schedule_dates
		
        EXEC dbo.ui_service_planing @action = 'saveServiceClaim',
                                                              @id_contract2devices = @id_contract2devices,
                                                              @lst_schedule_dates = @lst_schedule_dates,
                                                              @id_creator = @id_creator
                                                                                        
                                            
                                            
        FETCH NEXT
					FROM curs
					INTO @id_contract2devices, @dstart, @dend
    END

CLOSE curs

DEALLOCATE curs