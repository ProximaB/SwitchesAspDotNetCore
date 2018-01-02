SELECT r.Id, r.Description, s.Name AS Switch, s.State, (SELECT CONVERT(date, s.[Last modified DateTime]) AS Date)
FROM dbo.Rooms AS r INNER JOIN dbo.Switches AS s ON r.RoomId=s.RoomId;

