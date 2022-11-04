SELECT CorpUserId, EmployeeName, Message,'' as Thumbnail, Status, 
 Isactive, CreatedBy, CreatedDate
FROM dbo.UserStarConfiguration WHERE Status IN ('P','A','D')