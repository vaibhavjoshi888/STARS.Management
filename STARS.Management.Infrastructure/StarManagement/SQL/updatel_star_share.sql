IF EXISTS(SELECT * FROM SHareLikeHistoryCount WHERE UserStarId=@userstarid)
BEGIN
 UPDATE SHareLikeHistoryCount SET ShareCount=coalesce(ShareCount,0)+1 WHERE UserStarId=@userstarid
END
ELSE
BEGIN 
	INSERT INTO dbo.SHareLikeHistoryCount (UserStarId, ShareCount)
	VALUES (@userstarid,1)
end