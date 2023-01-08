declare @customersWithValidComments table (Id integer, [Name] varchar(50));

insert into @customersWithValidComments values (1, 'as')
select * from @customersWithValidComments

use common
go
create function dbo.GetCustomersWithPriviliges()
returns table
as
	return (select c.Id as CustomerId, c.[Name] as CustomerName,  p.[Name] as Privilege
	from dbo.Customers c inner join dbo.Priviliges p on c.Id = p.CustomerId);
go

select * from dbo.GetCustomersWithPriviliges()