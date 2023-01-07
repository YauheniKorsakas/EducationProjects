use common

set transaction isolation level repeatable read;
go
begin try
	begin transaction lol with mark 'default';
		select * from dbo.Priviliges
		insert into dbo.Priviliges values
			(7, 'lol', 3)

			DECLARE @TranName VARCHAR(20);  
			SELECT @TranName = 'MyTransaction';  
			begin transaction @tranName
				insert into dbo.Priviliges values
					(7, 'lol', 3)
			commit transaction @tranName;

		insert into dbo.Priviliges values
			(8, 'lol', 3)
		print 'final result'

		select * from dbo.Priviliges
end try
begin catch
	if @@trancount > 0
	rollback transaction;
	print ERROR_MESSAGE()
	SELECT  
    ERROR_NUMBER() AS ErrorNumber  
    ,ERROR_SEVERITY() AS ErrorSeverity  
    ,ERROR_STATE() AS ErrorState  
    ,ERROR_PROCEDURE() AS ErrorProcedure  
    ,ERROR_LINE() AS ErrorLine  
    ,ERROR_MESSAGE() AS ErrorMessage;  
end catch;

