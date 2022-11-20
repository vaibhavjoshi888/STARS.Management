SELECT TOP (6) UserStarId AS UserStarId,
 CorpUserId AS CorpUserId,
  EmployeeName AS EmployeeName,
   Message as Message,
   Thumbnail as Thumbnail,
    CreatedBy AS CreatedBy,
    CreatedDate AS CreatedDate
     FROM dbo.UserStarConfiguration
  WHERE Status ='A' ORDER BY CreatedDate DESC
 