

CREATE PROCEDURE [dbo].[InsertRecord]
    @Number int,
    @IsDivisbleByThree bit,
	@IsDivisbleByFive bit,
    @IsDivisbleByThreeAndFive bit,
	@IsNeutral bit
AS
BEGIN
    SET NOCOUNT ON;
    Insert into Output(
            [Number]
           ,[IsDivisibleByThree]
		   ,[IsDivisibleByFive]
		   ,[IsDivisibleByThreeAndFive]
		   ,[IsNeutral]
           )
 Values (@Number, @IsDivisbleByThree, @IsDivisbleByFive, @IsDivisbleByThreeAndFive, @IsNeutral )
END
