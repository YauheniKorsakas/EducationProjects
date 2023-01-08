select *
from dbo.Customers c inner join dbo.Priviliges p on p.CustomerId = c.Id
where c.Id = 1

select * from dbo.Customers
select * from dbo.Priviliges

-- not updatable view as it contains more than 1 table. Also it can be unupdatable if
-- it contains subqueries in where clause.
create view CustomersWithPriviliges as
	select c.Id CustomerId, c.[Name] CustomerName, p.[Name] Privilige 
	from dbo.Customers c inner join dbo.Priviliges p on p.CustomerId = c.Id
	with check option

	 -- updatable view as it does not contain distinct and contains only one source table
create view CustomersWithComments as
	select * from dbo.Customers c
	where c.Comments is not null
	with check option

select * from dbo.CustomersWithComments
select * from dbo.Customers

-- there is no error while trying to update rows that are not in where statement, but they will
-- not be updated
	update dbo.CustomersWithComments
	set [Name] = 'ZERO1'
	where Id = 3 or Id = 13
