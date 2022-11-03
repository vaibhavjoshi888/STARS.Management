update dbo.UserRole SET  RoleId =@roleid WHERE AppUserId IN (SELECT AppUserId FROM AppUser WHERE CorpUserId= @corpuserid)
