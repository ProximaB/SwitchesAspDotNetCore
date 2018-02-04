SELECT TOP (1000) s.Id, s.Switch, r.Name AS 'Room', s.State,  s.[Last modified Date], s.[Last modified Time]
  FROM 
	  SwitchesAPIDb2.dbo.Rooms AS r
	  LEFT JOIN (Select  Switches.Name as 'Switch',
						 /*Switches.Description as 'Switch Description',*/
						 Switches.State, 
						 (SELECT CONVERT(date, [Switches].[Last modified DateTime])) as 'Last modified Date',
						 (SELECT CONVERT(time(0), [Switches].[Last modified DateTime])) as 'Last modified Time',
						 Switches.RoomId,
						 Switches.Id
	  from [SwitchesAPIDb2].[dbo].Switches) AS s ON r.RoomId = s.RoomId;