INSERT INTO dbo.AppUser (CorpUserId, Email, Phone, FirstName, LastName, DisplayName, EmployeeType, EmployeeNumber, PhysicalDeliveryOfficeName, Department, Division, Title, ManagerCorpUserId, ManagerDisplayName, Note, CreatedDate, CreatedBy, ActiveStatus)
VALUES (@corpuserid, @email, @phone, @firstname, @lastname, @displayname, @employeetype, @employeenumber, @physicaldeliveryofficename, @department, @division, @title, @managercorpuserid, @managerdisplayname, @note, @createddate, @createdby, @activestatus);

INSERT INTO dbo.UserRole (AppUserId, RoleId, CreatedDate, CreatedBy, ActiveStatus)
VALUES (@appuserid, @roleid, @createddate, @createdby, @activestatus)