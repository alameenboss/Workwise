
CREATE Procedure [dbo].[AddPost]
(
@Worktype			int,
@Rate				decimal(18,2),
@Title				nvarchar(max),
@Location			nvarchar(max),
@Description		nvarchar(max),
@ListImage			dbo.TVP_Image Readonly,
@PostedById			varchar(128),
@Op_StatusCode		INT OUTPUT,  
@Op_Status			varchar(Max) OUTPUT  
)
AS
/* **********************************************************************
Author:  Alameen.S.D
Creation Date: 04-JUl-2020
Desc: 

*************************************************************************
Change History
DATE			CHANGED BY			CHANGE CODE			DESCRIPTION
	
*/     
BEGIN    
    
BEGIN TRY 

DECLARE @Lv_InsertedImages dbo.TVP_Image;
DECLARE @Lv_PostId Int;


INSERT INTO [dbo].[Posts]
([Worktype],[Rate],[Title],[PostedOn],[Location],[Description],[PostedById])
VALUES (@Worktype,@Rate,@Title	,GetDate(),@Location,@Description,@PostedById)

Set @Lv_PostId = SCOPE_IDENTITY() 

Insert Into Image (ImagePath,IsActive)
OUTPUT inserted.Id,inserted.ImagePath INTO @Lv_InsertedImages
Select [Path],1 from @ListImage



Insert Into [dbo].[MapPostImage] (PostId,ImageId)
Select @Lv_PostId,Id from @Lv_InsertedImages

		Select @Lv_PostId  as Id     
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER

		Select	@Op_StatusCode = 0 ,    
		@Op_Status		= 'Success_message'; 

END TRY    
    
BEGIN CATCH    
    
 EXEC  LogError @PostedById;    

 Select @Op_StatusCode = 1 ,    
  @Op_Status  = ERROR_MESSAGE();   
END CATCH    
    
    
END 
