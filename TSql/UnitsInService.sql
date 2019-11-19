CREATE VIEW UnitsInService
AS SELECT l.Address1, bt.[Description] AS BrewerTypeDescription, u.Acquired, u.Cost
     FROM Units AS u 
     INNER JOIN Locations AS l ON l.LocationId = u.LocationId
     INNER JOIN BrewerTypes AS bt ON bt.BrewerTypeId = u.BrewerTypeId
   WHERE (u.OutOfService IS NULL)