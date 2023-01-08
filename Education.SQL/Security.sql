use common
go

-- you have to create new login (on server level) and user (on database level)
-- and link them 
CREATE LOGIN adminuser WITH PASSWORD = 'Test1234';
GO
-- grant select on particular columns
grant select (Id, [Name])  on dbo.Priviliges to guestuser;
revoke select on dbo.Priviliges to defaultguest;

-- by default user for operations is logged on user, but
-- execute as allows to switch user context 
execute as USER = 'defaultguest'
select * from dbo.Customers
REVERT;

--
create role OnlySelect;
grant select on dbo.Customers to OnlySelect;

alter role OnlySelect add member defaultguest;
EXEC sp_droprolemember 'OnlySelect', 'defaultguest'; 