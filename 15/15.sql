
create table [dbo].[d15](
	[id] [int] identity(1,1) not null,
	[type] [int] null,
	[sensor] [geometry] null,
	[beacon] [geometry] null
) on [primary] TEXTIMAGE_ON [primary]
go


insert d15 (type, sensor, beacon)
values 
(1,geometry::STGeomFromText('POINT(2150774 3136587)',3006), geometry::STGeomFromText('POINT(2561642 2914773)',3006)),
(1,geometry::STGeomFromText('POINT(3983829 2469869)',3006), geometry::STGeomFromText('POINT(3665790 2180751)',3006)),
(1,geometry::STGeomFromText('POINT(2237598 3361)',3006), geometry::STGeomFromText('POINT(1780972 230594)',3006)),
(1,geometry::STGeomFromText('POINT(1872170 78941)',3006), geometry::STGeomFromText('POINT(1780972 230594)',3006)),
(1,geometry::STGeomFromText('POINT(3444410 3965835)',3006), geometry::STGeomFromText('POINT(3516124 3802509)',3006)),
(1,geometry::STGeomFromText('POINT(3231566 690357)',3006), geometry::STGeomFromText('POINT(2765025 1851710)',3006)),
(1,geometry::STGeomFromText('POINT(3277640 2292194)',3006), geometry::STGeomFromText('POINT(3665790 2180751)',3006)),
(1,geometry::STGeomFromText('POINT(135769 50772)',3006), geometry::STGeomFromText('POINT(1780972 230594)',3006)),
(1,geometry::STGeomFromText('POINT(29576 1865177)',3006), geometry::STGeomFromText('POINT(255250 2000000)',3006)),
(1,geometry::STGeomFromText('POINT(3567617 3020368)',3006), geometry::STGeomFromText('POINT(3516124 3802509)',3006)),
(1,geometry::STGeomFromText('POINT(1774477 148095)',3006), geometry::STGeomFromText('POINT(1780972 230594)',3006)),
(1,geometry::STGeomFromText('POINT(1807041 359900)',3006), geometry::STGeomFromText('POINT(1780972 230594)',3006)),
(1,geometry::STGeomFromText('POINT(1699781 420687)',3006), geometry::STGeomFromText('POINT(1780972 230594)',3006)),
(1,geometry::STGeomFromText('POINT(2867703 3669544)',3006), geometry::STGeomFromText('POINT(3516124 3802509)',3006)),
(1,geometry::STGeomFromText('POINT(1448060 201395)',3006), geometry::STGeomFromText('POINT(1780972 230594)',3006)),
(1,geometry::STGeomFromText('POINT(3692914 3987880)',3006), geometry::STGeomFromText('POINT(3516124 3802509)',3006)),
(1,geometry::STGeomFromText('POINT(3536880 3916422)',3006), geometry::STGeomFromText('POINT(3516124 3802509)',3006)),
(1,geometry::STGeomFromText('POINT(2348489 2489095)',3006), geometry::STGeomFromText('POINT(2561642 2914773)',3006)),
(1,geometry::STGeomFromText('POINT(990761 2771300)',3006), geometry::STGeomFromText('POINT(255250 2000000)',3006)),
(1,geometry::STGeomFromText('POINT(1608040 280476)',3006), geometry::STGeomFromText('POINT(1780972 230594)',3006)),
(1,geometry::STGeomFromText('POINT(2206669 1386195)',3006), geometry::STGeomFromText('POINT(2765025 1851710)',3006)),
(1,geometry::STGeomFromText('POINT(3932320 3765626)',3006), geometry::STGeomFromText('POINT(3516124 3802509)',3006)),
(1,geometry::STGeomFromText('POINT(777553 1030378)',3006), geometry::STGeomFromText('POINT(255250 2000000)',3006)),
(1,geometry::STGeomFromText('POINT(1844904 279512)',3006), geometry::STGeomFromText('POINT(1780972 230594)',3006)),
(1,geometry::STGeomFromText('POINT(2003315 204713)',3006), geometry::STGeomFromText('POINT(1780972 230594)',3006)),
(1,geometry::STGeomFromText('POINT(2858315 2327227)',3006), geometry::STGeomFromText('POINT(2765025 1851710)',3006)),
(1,geometry::STGeomFromText('POINT(3924483 1797070)',3006), geometry::STGeomFromText('POINT(3665790 2180751)',3006)),
(1,geometry::STGeomFromText('POINT(1572227 3984898)',3006), geometry::STGeomFromText('POINT(1566446 4774401)',3006)),
(1,geometry::STGeomFromText('POINT(1511706 1797308)',3006), geometry::STGeomFromText('POINT(2765025 1851710)',3006)),
(1,geometry::STGeomFromText('POINT(79663 2162372)',3006), geometry::STGeomFromText('POINT(255250 2000000)',3006)),
(1,geometry::STGeomFromText('POINT(3791701 2077777)',3006), geometry::STGeomFromText('POINT(3665790 2180751)',3006)),
(1,geometry::STGeomFromText('POINT(2172093 3779847)',3006), geometry::STGeomFromText('POINT(2561642 2914773)',3006)),
(1,geometry::STGeomFromText('POINT(2950352 2883992)',3006), geometry::STGeomFromText('POINT(2561642 2914773)',3006)),
(1,geometry::STGeomFromText('POINT(3629602 3854760)',3006), geometry::STGeomFromText('POINT(3516124 3802509)',3006)),
(1,geometry::STGeomFromText('POINT(474030 3469506)',3006), geometry::STGeomFromText('POINT(-452614 3558516)',3006))


select geometry::STLineFromText('LINESTRING(-2000000 2000000, 5200000 2000000)', 3006)
	.STIntersection(d)
	.STLength()
from
(
	select geometry::UnionAggregate(c).ToString() d from
	(
		select geometry::ConvexHullAggregate(a) c from
		(
			select id, geometry::Point(sensor.STX + (ABS(sensor.STX - beacon.STX) + ABS(sensor.STY - beacon.STY)), sensor.STY, 3006) as a from d15
			union all
			select id,geometry::Point(sensor.STX - (ABS(sensor.STX - beacon.STX) + ABS(sensor.STY - beacon.STY)), sensor.STY, 3006) from d15
			union all
			select id,geometry::Point(sensor.STX, sensor.STY + (ABS(sensor.STX - beacon.STX) + ABS(sensor.STY - beacon.STY)), 3006) from d15
			union all 
			select id,geometry::Point(sensor.STX, sensor.STY - (ABS(sensor.STX - beacon.STX) + ABS(sensor.STY - beacon.STY)), 3006) from d15
		) a 
		group by id
	)b
) c



select geometry::STPolyFromText('POLYGON((0 0, 0 4000000, 4000000 4000000, 4000000 0, 0 0))', 3006)
	.STDifference(d)
	.STCentroid()
	.ToString()
from
(
	select geometry::UnionAggregate(c) d from
	(
		select geometry::ConvexHullAggregate(a) c from
		(
		select id, geometry::Point(sensor.STX + (ABS(sensor.STX - beacon.STX) + ABS(sensor.STY - beacon.STY)), sensor.STY, 3006) as a from d15
		union all
		select id,geometry::Point(sensor.STX - (ABS(sensor.STX - beacon.STX) + ABS(sensor.STY - beacon.STY)), sensor.STY, 3006) from d15
		union all
		select id,geometry::Point(sensor.STX, sensor.STY + (ABS(sensor.STX - beacon.STX) + ABS(sensor.STY - beacon.STY)), 3006) from d15
		union all 
		select id,geometry::Point(sensor.STX, sensor.STY - (ABS(sensor.STX - beacon.STX) + ABS(sensor.STY - beacon.STY)), 3006) from d15

		) a 
		group by id
	)b
) c




--insert d15 (type, sensor, beacon)
--values 
--(1,geometry::STGeomFromText('POINT(2 18)',3006),geometry::STGeomFromText('POINT(-2 15)',3006)),
--(1,geometry::STGeomFromText('POINT(9 16)',3006),geometry::STGeomFromText('POINT(10 16)',3006)),
--(1,geometry::STGeomFromText('POINT(13 2)',3006),geometry::STGeomFromText('POINT(15 3)',3006)),
--(1,geometry::STGeomFromText('POINT(12 14)',3006),geometry::STGeomFromText('POINT(10 16)',3006)),
--(1,geometry::STGeomFromText('POINT(10 20)',3006),geometry::STGeomFromText('POINT(10 16)',3006)),
--(1,geometry::STGeomFromText('POINT(14 17)',3006),geometry::STGeomFromText('POINT(10 16)',3006)),
--(1,geometry::STGeomFromText('POINT(8 7)',3006),geometry::STGeomFromText('POINT(2 10)',3006)),
--(1,geometry::STGeomFromText('POINT(2 0)',3006),geometry::STGeomFromText('POINT(2 10)',3006)),
--(1,geometry::STGeomFromText('POINT(0 11)',3006),geometry::STGeomFromText('POINT(2 10)',3006)),
--(1,geometry::STGeomFromText('POINT(20 14)',3006),geometry::STGeomFromText('POINT(25 17)',3006)),
--(1,geometry::STGeomFromText('POINT(17 20)',3006),geometry::STGeomFromText('POINT(21 22)',3006)),
--(1,geometry::STGeomFromText('POINT(16 7)',3006),geometry::STGeomFromText('POINT(15 3)',3006)),
--(1,geometry::STGeomFromText('POINT(14 3)',3006),geometry::STGeomFromText('POINT(15 3)',3006)),
--(1,geometry::STGeomFromText('POINT(20 1)',3006),geometry::STGeomFromText('POINT(15 3)',3006))


--truncate table d15
--select ABS(sensor.STX - beacon.STX) + ABS(sensor.STY - beacon.STY) from d15

--select geometry::STPolyFromText('POLYGON((0 0, 0 20, 20 20, 20 0, 0 0))', 3006)