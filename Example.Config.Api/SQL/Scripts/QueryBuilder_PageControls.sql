select 
	Id,
	(select top 1 Name from Pages where Pages.Id=PageId) PageName,
	(select top 1 Identifier from Controls where Controls.Id=ControlId) Identifier,
	(select top 1 TenantId from Pages 
		join TenantDomains ON TenantDomains.Id=Pages.TenantDomainId
	where Pages.Id=PageId) TenantId,
	Accessability,
	SourceCollectionId
from PageControls
where 
	1=1 
	
	--AND ControlId IN (select Id from Controls where Identifier like '%Source%') 
	AND ControlId IN (
		select Controls.Id from Controls
			join PageCategories on PageCategories.Id=Controls.PageCategoryId
		where PageCategories.Name like '%ItemsEdit%'
	)
	--AND PageId=1831046084000
	AND PageId IN (
		select Pages.Id from Pages
			join TenantDomains on TenantDomains.Id=Pages.TenantDomainId 
		where 
			1=1
			AND TenantDomains.TenantId=1 
			--AND Pages.Name in ('ResourceComponentSelection')
			--AND Pages.Name not like '%ResourceTypeFolderListPopUp%'
	)
	--AND Accessability <> (SELECT TOP(1) DefaultAccessibility FROM Pages WHERE Pages.Id=PageControls.PageId)
order by PageName