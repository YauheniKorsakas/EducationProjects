use common
go

with CustWithComments (Id, [Name]) as
(
	select Id, [Name] from dbo.Customers
	where Comments is not null
)

select * from CustWithComments;
with CustWithComments (Id, [Name]) as
(
	select Id, [Name] from dbo.Customers
	where Comments is not null
)

declare @tempData table (Id integer, Name varchar(50));

insert into @tempData
	select Id, Name from dbo.Customers

select *  from @tempData




use common
go

declare @valuableDataFromCustomers table (Id integer, Name varchar(50))
	select Id, Name
	into @valuableDataFromCustomers
	from dbo.Customers

select * from @valuableDataFromCustomers

select * from dbo.Customers

declare @removedData table (Id integer, Name varchar(50))

delete dbo.Customers
output deleted.Age, deleted.Name
into @removedData
output deleted.Id
where Name in ('Name7', 'Name8')

select * from @removedData
select * from dbo.Customers
SELECT @@SPID
SELECT @@SPID AS 'ID', SYSTEM_USER AS 'Login Name', USER AS 'User Name'; 