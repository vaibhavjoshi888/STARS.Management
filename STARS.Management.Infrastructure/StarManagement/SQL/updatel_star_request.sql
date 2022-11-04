UPDATE dbo.UserStarConfiguration
SET Message = @message,
	Status = @status,
	Approvedby = @approvedby,
	Feedback = @feedback,
	StarStartDate = @starstartdate,
	StarEndDate = @starenddate,
	ModifiedBy = @modifiedby,
	ModifiedDate = @modifieddate
WHERE UserStarId=@userstarid AND CorpUserId=@corpuserid