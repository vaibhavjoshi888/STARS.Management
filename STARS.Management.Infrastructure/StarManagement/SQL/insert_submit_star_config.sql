INSERT INTO dbo.UserStarConfiguration (CorpUserId, EmployeeName, Message, Status, 
 Isactive, CreatedBy, CreatedDate,StarFirstName,StarLastName)
VALUES (@corpuserid, @employeename, @message,
 @status,@isactive, @createdby,getdate(),@starfirstname,@starlastname)