select ViewPath, Domains.NameChain from Pages
join Domains on DomainId=Domains.Id
 where TenantId=4 AND
 ViewPath not in (select ViewPath from Pages where TenantId=1)
 order by ViewPath;

select ViewPath, Count(*) from Pages where TenantId=4 group by ViewPath --having count(*)>1;

 select CS.Id, Asga.ViewPath,CS.ViewPath
 from 

(select * from Pages where TenantId=1) Asga
 right join  (select * from Pages where TenantId=4) CS  on CS.ViewPath=Asga.ViewPath

 where Asga.ViewPath is null OR CS.ViewPath is null
  order by CS.ViewPath;

select * from CustomFields where PageId in (
1911367359078,
1911367359077,
1911367359114,
1911367359084,
1911367359085,
1911367359039,
1917640672003,
1911367359042,
1911367359083,
1911367359040
);


select Pages.Id,pp.Id,Pages.ViewPath,pp.ViewPath from Pages
join Pages pp on pp.Name=Pages.Name AND pp.TenantId=Pages.TenantId AND pp.Id!=Pages.Id
 where Pages.Id in (
1911367359078,
1911367359077,
1911367359114,
1911367359084,
1911367359085,
1911367359039,
1917640672003,
1911367359042,
1911367359083,
1911367359040
)

select ViewPath from Pages where TenantId=1 order by ViewPath;