SELECT AppUser.CorpUserId AS CorpID,
AppUser.Email,
AppUser.Phone,
AppUser.FirstName +' '+AppUser.LastName as FullName,
AppUser.DisplayName,
'' AS GivenName,
AppUser.LastName AS Surname,
'' AS SamaAccountName,
AppUser.PhysicalDeliveryOfficeName,
AppUser.EmployeeType,
'' as EmployeeId,
AppUser.EmployeeNumber ,
AppUser.Title,
AppUser.Department,
AppUser.Division,
'' AS Manager,
AppUser.ManagerDisplayName,
AppUser.ManagerEmail,
AppUser.ManagerCorpUserId AS ManagerCorpID,
AppUser.ThumbnailPhoto,
UserRole.RoleId as UserRoleId,
AppUser.Createdby,
AppUser.createddate,
FROM AppUser  INNER join UserRole ON UserRole.AppUserId = AppUser.AppUserId WHERE AppUser.ActiveStatus='1'