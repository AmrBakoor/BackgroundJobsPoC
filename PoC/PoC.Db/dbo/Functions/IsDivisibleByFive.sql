﻿
create function IsDivisibleByFive (@number int)
returns bit
as
begin
declare @result bit
 if (@number %5 = 0)
   set @result = 1
 else
   set @result = 0
return @result
end