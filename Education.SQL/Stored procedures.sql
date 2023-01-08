use common
go

create procedure spGetcustomersByAge
	@age integer
as
begin
	select *
	from dbo.Customers c
	where c.Age = @age
end
go

create procedure spGetCustomersWithComments
	@totalCount integer output
as
begin
	set @totalCount = (select COUNT(*)
		from dbo.Customers c
		where c.Comments is not null)
end
go

create procedure spGetAllStuff
as
begin
	return select * from dbo.Stuff
end

execute spGetcustomersByAge 123

declare @totalCount integer = 0;
execute spGetCustomersWithComments @totalCount output
print @totalCount

select Id from
(exec spGetAllStuff)