CREATE Procedure [dbo].[LogError]
(
@UserId [varchar](128)
)
AS
BEGIN

	Declare @ErrorNumber INT,@Message Varchar(200),@errorState int;

	Set @ErrorNumber = ERROR_NUMBER();
	Set @Message = ERROR_MESSAGE();
	Set @errorState = ERROR_STATE();

   INSERT INTO ErrorLog Values( 
      @ErrorNumber
      ,ERROR_SEVERITY() 
      ,@errorState
      ,ERROR_PROCEDURE() 
      ,ERROR_LINE() 
      ,@Message
	  ,@UserId,
	  GETDATE()
	  )

	

END
