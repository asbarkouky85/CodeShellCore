--select * 
update P set ParentId=Asga.Id
from Pages P
join Pages Asga on Asga.Name=P.Name
where P.TenantId=7 AND Asga.TenantId=1 AND P.ParentId is null;

exec dbo.SyncTenants 1,2;

select ViewPath from Pages 
where TenantId=2
order by ViewPath;
--group by ViewPath
--having count(*) > 1;

--delete from PageRoutes where PageId in (select Id from Pages where TenantId=2);
--delete from PageParameters where PageId in (select Id from Pages where TenantId=2);
