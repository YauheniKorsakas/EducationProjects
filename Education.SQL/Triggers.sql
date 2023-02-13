use common
go

create trigger insert_Priviliges
on dbo.Priviliges for insert
as
	print @@rowcount
	IF (ROWCOUNT_BIG() = 0) -- not start a trigger to reduce trigger invocation time as often triggers are performed in transaction
	RETURN;

	select [Name] from inserted
go;