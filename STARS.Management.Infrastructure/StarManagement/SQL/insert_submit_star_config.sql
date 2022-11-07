INSERT INTO dbo.UserStarConfiguration (CorpUserId, EmployeeName, Message, Status, 
 Isactive, CreatedBy, CreatedDate)
VALUES (@corpuserid, @employeename, @message,
 @status,@isactive, @createdby,getdate())