SELECT ap.CorpUserId,ap.AppUserId, r.RoleId,r.RoleName,r.RoleDisplayName 
FROM AppUser ap  INNER JOIN UserRole ur ON ur.AppUserId = ap.AppUserId 
INNER JOIN Role r ON r.RoleId = ur.RoleId WHERE ap.ActiveStatus='1' AND ap.CorpUserId=@userId
