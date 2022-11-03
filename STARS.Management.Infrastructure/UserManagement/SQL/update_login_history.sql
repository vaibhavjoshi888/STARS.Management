UPDATE dbo.LoginHistory SET LoginAttempt=LoginAttempt+1, LoginTime=getdate()  WHERE UserName = @corpuserid
