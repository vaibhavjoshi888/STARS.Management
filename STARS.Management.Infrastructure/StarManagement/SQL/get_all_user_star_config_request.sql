SELECT UserStarId,CorpUserId, EmployeeName, Message,'' as Thumbnail, Status, 
 Isactive, CreatedBy,FORMAT(CreatedDate,'MM/dd/yyyy') as CreatedDate
FROM dbo.UserStarConfiguration WHERE Status IN ('P','A','D')