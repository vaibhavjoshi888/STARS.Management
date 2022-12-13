SELECT UserStarId AS UserStarId,
 CorpUserId AS CorpUserId,
  EmployeeName AS EmployeeName,
   Message as Message,
   Thumbnail as Thumbnail,
    CreatedBy AS CreatedBy,
    FORMAT(CreatedDate,'dd/MM/yyyy')  AS CreatedDate,
    StarFirstName AS FirstName,
    StarLastName AS LastName 
     FROM dbo.UserStarConfiguration
  WHERE Status ='A' ORDER BY CreatedDate DESC