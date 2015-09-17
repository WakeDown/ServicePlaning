DECLARE @volume_counter INT, @volume_counter_colour INT, @id INT, @counter INT, @counter_colour int, @planing_date DATEtime, @id_device INT, @id_contract int

DECLARE curs CURSOR FOR 
SELECT dd.id_device_data FROM dbo.srvpl_device_data dd WHERE dd.enabled = 1

OPEN curs
FETCH NEXT
FROM curs
INTO @id

WHILE @@FETCH_STATUS = 0
BEGIN

SELECT @counter = counter, @counter_colour = counter_colour, @planing_date = date_month , @id_device = id_device, @id_contract = id_contract
FROM dbo.srvpl_device_data dd WHERE dd.id_device_data = @id

--PRINT @planing_date
--PRINT CONVERT(DATETIME, CONVERT(NVARCHAR,year(@planing_date)) + '-' + CONVERT(NVARCHAR,month(@planing_date)) + '-01')
--CAST(DATEPART(year, @planing_date) + '-' + DATEPART(month,@planing_date) + '-01' AS DATEtime)

SET @volume_counter = ISNULL(@counter, 0) - ISNULL(( SELECT 
                                                              MAX([counter])
                                                              FROM
                                                              dbo.srvpl_device_data t
                                                              WHERE
                                                              t.ENABLED = 1
                                                              AND t.id_device = @id_device
                                                              AND t.id_contract = @id_contract
                                                              AND [counter] IS NOT NULL
                                                              AND t.date_month < CONVERT(DATETIME, CONVERT(NVARCHAR,year(@planing_date)) + '-' + CONVERT(NVARCHAR,month(@planing_date)) + '-01')
                                                              ), 0)
                                            SELECT @volume_counter = CASE when @volume_counter = ISNULL(@counter, 0) OR @volume_counter < 0 THEN 0 ELSE @volume_counter end
                                                    
                                        SET @volume_counter_colour = ISNULL(@counter_colour, 0) - ISNULL(( SELECT 
                                                              MAX([counter_colour])
                                                              FROM
                                                              dbo.srvpl_device_data t
                                                              WHERE
                                                              t.ENABLED = 1
                                                              AND t.id_device = @id_device
                                                              AND t.id_contract = @id_contract
                                                              AND [counter_colour] IS NOT NULL
                                                              AND t.date_month < CONVERT(DATETIME, CONVERT(NVARCHAR,year(@planing_date)) + '-' + CONVERT(NVARCHAR,month(@planing_date)) + '-01')
                                                              ), 0)
                                                              
                                                              SELECT @volume_counter_colour = CASE when @volume_counter_colour = ISNULL(@counter_colour, 0) OR @volume_counter_colour < 0 THEN 0 ELSE @volume_counter_colour end
                                            
                                            --IF @volume_counter < 0
                                            --BEGIN
                                            --PRINT @id
                                            --end
                                            
                                            UPDATE dbo.srvpl_device_data
                                            SET volume_counter = @volume_counter, volume_counter_colour = @volume_counter_colour
                                            WHERE id_device_data = @id
                                            
                                            --PRINT 'id_device: '+ ISNULL(CONVERT(NVARCHAR, @id_device), '') + ' id_contract: '+ ISNULL(CONVERT(NVARCHAR, @id_contract), '') + ' counter:' + isnull(CONVERT(NVARCHAR, @counter), '') + ' counter_colour:' + isnull(CONVERT(NVARCHAR, @counter_colour), '') + ' planing_date:' + isnull(CONVERT(NVARCHAR(50),@planing_date), '') + ' volume_counter:' + isnull(CONVERT(NVARCHAR, @volume_counter), '') + ' volume_counter_colour:' + isnull(CONVERT(NVARCHAR, @volume_counter_colour), '')
                                            
                                            
                                            FETCH NEXT
							FROM curs
							INTO @id
                                        END

                                    CLOSE curs

                                    DEALLOCATE curs