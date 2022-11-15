SELECT
SUM(CASE WHEN Status IN ('P','A','D') THEN 1 ELSE 0 END) AS TotalSubmited
,SUM(CASE WHEN Status = 'P' THEN 1 ELSE 0 END) AS Pending
,SUM(CASE WHEN Status = 'D' THEN 1 ELSE 0 END) AS Denied
,SUM(CASE WHEN Status = 'A' THEN 1 ELSE 0 END) AS Approved
FROM UserStarConfiguration