CREATE Procedure [dbo].[GetChatMessagesByUserId]  
(  
@UserId varchar(128), 
@ToUserId varchar(128), 
@LastMessageId INT, 
@Op_StatusCode  INT OUTPUT,    
@Op_Status   varchar(Max) OUTPUT   
)  
AS  
/* **********************************************************************  
Author:  Alameen.S.D  
Creation Date: 04-JUl-2020  
Desc:   
  
*************************************************************************  
Change History  
DATE   CHANGED BY   CHANGE CODE   DESCRIPTION  
   
*/    
BEGIN      
      
BEGIN TRY   
  
	Declare @Page INT = 1,@Size Int = 20
	Select 
		ChatMessageId, 
		FromUserId,
		ToUserId,
		Message, 
		Status, 
		CreatedOn, 
		UpdatedOn, 
		ViewedOn,
		IsActive 
	from [dbo].[ChatMessages] 
	Where IsActive = 1 
		and ( ToUserId = @ToUserId or FromUserId = @ToUserId ) 
		and ( ToUserId = @UserId or FromUserId = @UserId )
		and ChatMessageId > @LastMessageId
	Order By CreatedOn
	OFFSET (@Page -1) * @Size ROWS    
	 FETCH NEXT @Size ROWS ONLY   
	 FOR JSON PATH  


  
 Select @Op_StatusCode = 0 ,      
 @Op_Status  = 'Success_message';   
   
END TRY      
      
BEGIN CATCH      
      
 EXEC  LogError @UserId;      
  
 Select @Op_StatusCode = 1 ,      
  @Op_Status  = ERROR_MESSAGE();     
END CATCH      
      
      
END 
