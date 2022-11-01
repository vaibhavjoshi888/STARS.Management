SELECT UserName, LoginTime, LoginAttempt
FROM dbo.LoginHistory WHERE UserName =@userId
