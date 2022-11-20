SELECT ustar.UserStarId AS UserStarId,
 ustar.CorpUserId AS CorpUserId,
  ustar.EmployeeName AS EmployeeName,
   ustar.Message as Message,
   ustar.Thumbnail as Thumbnail, 
   coalesce(slc.ShareCount,0) AS ShareCount,
   coalesce(slc.LikeCount,0) AS LikeCount,
    ustar.CreatedBy AS CreatedBy, ustar.CreatedDate AS CreatedDate FROM dbo.UserStarConfiguration ustar 
    left JOIN SHareLikeHistoryCount slc
ON slc.UserStarId= ustar.UserStarId
  WHERE Status ='A'