SELECT TOP (1000) s.Id, s.Switch, r.Name, s.State,  s.[Last modified Time], s.[Last modified Date]
  FROM 
	  SwitchesAPIDb.dbo.Rooms AS r
	  LEFT JOIN (Select  Switches.Name as 'Switch',
						 /*Switches.Description as 'Switch Description',*/
						 Switches.State, 
						 (SELECT CONVERT(date, [Switches].[Last modified DateTime])) as 'Last modified Date',
						 (SELECT CONVERT(time(0), [Switches].[Last modified DateTime])) as 'Last modified Time',
						 Switches.RoomId,
						 Switches.Id
	  from [SwitchesAPIDb].[dbo].Switches) AS s ON r.Id = s.RoomId;