USE [unit_prog]

DECLARE @id_device INT ,
    @id_device_model INT ,
    @serial_num NVARCHAR(50) ,
    @instal_date DATETIME ,
    @adf BIT ,
    @finisher BIT ,
    @tray BIT ,
    @id_creator INT ,
    @id_device_option INT ,
    @id_device_import INT ,
    @error_text NVARCHAR(MAX) ,
    @age INT ,
    @id_contract INT ,
    @id_service_interval INT ,
    @id_service_admin INT ,
    @id_city INT ,
    @address NVARCHAR(150) ,
    @object_name NVARCHAR(150) ,
    @contact_name NVARCHAR(150)
SET @id_creator = 816

DECLARE crs cursOR
FOR
    SELECT  di.id ,
            di.id_model ,
            di.serial_num ,
            di.instal_date ,
            di.adf ,
            di.finisher ,
            di.tray ,
            di.id_contract ,
            di.id_service_admin ,
            di.id_city ,
            di.ADDRESS ,
            di.OBJECT_NAME ,
            di.contact_name ,
            di.id_service_interval
    FROM    dbo.srvpl_devices_import di
    WHERE   di.import = 0
            AND id_model IS NOT NULL
OPEN crs
FETCH NEXT
                        
                        FROM crs
				INTO @id_device_import, @id_device_model, @serial_num,
    @instal_date, @adf, @finisher, @tray, @id_contract, @id_service_admin,
    @id_city, @address, @object_name, @contact_name, @id_service_interval
			
BEGIN TRY
    BEGIN TRANSACTION
							
    WHILE @@FETCH_STATUS = 0
        BEGIN
        
                                            
            SET @age = NULL
                                            
            IF @instal_date IS NOT NULL
                BEGIN
                    SELECT  @age = DATEDIFF(YEAR, @instal_date, GETDATE())
                END
                     
                     --Вставляем устройство                       
            EXEC @id_device = dbo.sk_service_planing @action = N'insDevice', -- nvarchar(50)
                @id_device_model = @id_device_model, @serial_num = @serial_num,
                @instalation_date = @instal_date, @age = @age,
                @id_creator = @id_creator
             --</Вставляем устройство 
             
             --Вставляем опции устройства                               
                                            --ADF
            IF @adf = 1
                BEGIN
                                            
                    SELECT  @id_device_option = id_device_option
                    FROM    dbo.srvpl_device_options do
                    WHERE   do.enabled = 1
                            AND do.name = 'ADF'
                                            
                    EXEC dbo.ui_service_planing @action = N'saveDevice2Options', -- nvarchar(50)
                        @id_device = @id_device,
                        @id_device_option = @id_device_option,
                        @id_creator = @id_creator
                                            
                END
                                            
                                            --Finisher
            IF @finisher = 1
                BEGIN
                                            
                    SELECT  @id_device_option = id_device_option
                    FROM    dbo.srvpl_device_options do
                    WHERE   do.enabled = 1
                            AND do.name = 'Finisher'
                                            
                    EXEC dbo.ui_service_planing @action = N'saveDevice2Options', -- nvarchar(50)
                        @id_device = @id_device,
                        @id_device_option = @id_device_option,
                        @id_creator = @id_creator
                                            
                END
                                            
                                            --Tray
            IF @tray = 1
                BEGIN
                                            
                    SELECT  @id_device_option = id_device_option
                    FROM    dbo.srvpl_device_options do
                    WHERE   do.enabled = 1
                            AND do.name = 'Tray'
                                            
                    EXEC dbo.ui_service_planing @action = N'saveDevice2Options', -- nvarchar(50)
                        @id_device = @id_device,
                        @id_device_option = @id_device_option,
                        @id_creator = @id_creator
                                            
                END
                --</Вставляем опции устройства
                 
                 --Если указан id_contract, то загружаем связку с контрактом
            IF @id_contract IS NOT NULL
                BEGIN
                    EXEC dbo.ui_service_planing @action = N'saveContract2Devices', -- nvarchar(50)
                        @id_contract = @id_contract, @id_device = @id_device,
                        @lst_id_device = @id_device,
                        @id_service_interval = @id_service_interval,
                        @id_city = @id_city, @address = @address,
                        @contact_name = @contact_name,
                        @id_creator = @id_creator,
                        @id_service_admin = @id_service_admin,
                        @object_name = @object_name
                END                 
                 
                 --Отмечаем что устройство загружено, чтобы не грузить посторно                           
            UPDATE  dbo.srvpl_devices_import
            SET     import = 1 ,
                    date_import = GETDATE()
            WHERE   id = @id_device_import
                                
            FETCH NEXT
					FROM crs
					INTO @id_device_import, @id_device_model, @serial_num,
                @instal_date, @adf, @finisher, @tray, @id_contract,
                @id_service_admin, @id_city, @address, @object_name,
                @contact_name, @id_service_interval
        END

    CLOSE crs

    DEALLOCATE crs
                                    
                                    --Исправляем кириллицу на латинницу
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'А', N'A')
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'В', N'B')
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'С', N'C')
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'Е', N'E')
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'Н', N'H')
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'К', N'K')
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'М', N'M')
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'О', N'O')
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'Р', N'P')
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'Т', N'T')
    UPDATE  [unit_prog].[dbo].srvpl_devices
    SET     serial_num = REPLACE(serial_num, N'Х', N'X')

    COMMIT TRANSACTION

END TRY
BEGIN CATCH
    CLOSE crs

    DEALLOCATE crs

    IF @@TRANCOUNT > 0
        ROLLBACK TRAN

    SELECT  @error_text = ERROR_MESSAGE() + ' Изменения не были сохранены!'

                                    /*RAISERROR (
									@error_text
									,16
									,1
									)*/
    PRINT @error_text
END CATCH