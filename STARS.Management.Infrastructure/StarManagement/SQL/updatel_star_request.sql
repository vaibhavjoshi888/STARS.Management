UPDATE dbo.UserStarConfiguration
SET Message = @message,
	Status = @status,
	Approvedby = @approvedby,
	Feedback = @feedback,
	StarStartDate =getdate(),
	StarEndDate = getdate(),
	ModifiedBy = @modifiedby,
	ModifiedDate =getdate()
WHERE UserStarId=@userstarid AND CorpUserId=@corpuserid