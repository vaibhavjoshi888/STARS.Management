INSERT INTO dbo.UserStarConfiguration (CorpUserId, EmployeeName, Message, Thumbnail, Status, 
 Isactive, CreatedBy, CreatedDate)
VALUES (@corpuserid, @employeename, @message, @thumbnail,
 @status,@isactive, @createdby,getdate())