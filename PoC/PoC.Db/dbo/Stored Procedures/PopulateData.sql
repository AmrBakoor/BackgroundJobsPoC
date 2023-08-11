
create procedure [dbo].[PopulateData] (@firstName nvarchar(150), @lastName nvarchar(150))
as
begin
declare @floor int
declare @ceil int
declare @IsDivisibleByThree bit
declare @IsDivisibleByFive bit
declare @IsDivisibleByThreeAndFive bit
declare @IsNeutral bit
set @floor = 1
set @ceil = 100
while @floor <= @ceil
begin
set @IsDivisibleByThree = (select dbo.IsDivisibleByThree(@floor))
set @IsDivisibleByFive = (select dbo.IsDivisibleByFive(@floor))
set @IsDivisibleByThreeAndFive = (select dbo.IsDivisibleByThreeAndFive(@floor))
set @IsNeutral = (select dbo.IsNeutral(@floor))
exec dbo.InsertRecord @floor,@IsDivisibleByThree, @IsDivisibleByFive, @IsDivisibleByThreeAndFive, @IsNeutral
set @floor +=1
end
end
