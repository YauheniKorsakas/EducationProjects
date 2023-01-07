select c.[Name],
	case when c.Age = 20 then 'It is ok'
	     else 'default'
	end [Description]
from dbo.Customers c

--Two forms of case when
declare @age integer = 11;
declare @description varchar(10) =
	case @age when 123 then 'not ok'
			  when 124 then 'ok'
	end;
set @description =
	case when @age > 123 then 'not ok'
	     when @age = 124 then 'ok'
	end;

print @description

-- nullif compares two values and sets null if are equal
-- coalesce takes first not null value
select [Name], NULLIF(Age, 19), COALESCE(Comments, 'Default comment')
from dbo.Customers



