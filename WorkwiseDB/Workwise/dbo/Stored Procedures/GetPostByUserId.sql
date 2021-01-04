
CREATE Procedure [dbo].[GetPostByUserId]  
(  
@UserId    varchar(128),  
@Page    INT,    
@Size    INT,   
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
  
  
 Select P.*,(
 
 Select I.Id,I.ImagePath AS ImageUrl from [dbo].[Image] I
 Join [dbo].[MapPostImage] MPI
 ON I.Id =  MPI.ImageId
 Where MPI.PostId = P.Id
 For JSON Path
 ) As PostImages
 from [dbo].[Posts] P  
 Where P.[PostedById] = @UserId  
 ORDER by P.[PostedOn] Desc  
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
