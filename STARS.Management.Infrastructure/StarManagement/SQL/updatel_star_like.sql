IF EXISTS(SELECT * FROM SHareLikeHistoryCount WHERE UserStarId=@userstarid)
BEGIN
 UPDATE SHareLikeHistoryCount SET LikeCount=coalesce(LikeCount,0)+1 WHERE UserStarId=@userstarid
END
ELSE
BEGIN 
	INSERT INTO dbo.SHareLikeHistoryCount (UserStarId, LikeCount)
	VALUES (@userstarid,1)
end