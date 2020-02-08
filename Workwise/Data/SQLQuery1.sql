Select * from Information_schema.Tables


Select * from OnlineUsers

Select UP.FirstName MsgFrom,TP.FirstName ToUser, CM.Message as msg from ChatMessages CM 
Join UserProfiles UP On UP.UserId = CM.FromUserID
Join UserProfiles TP ON TP.UserId = CM.ToUserID

Select UP.FirstName MsgFrom,TP.FirstName ToUser from FriendMappings CM 
Join UserProfiles UP On UP.UserId = CM.UserId
Join UserProfiles TP ON TP.UserId = CM.EndUserId
