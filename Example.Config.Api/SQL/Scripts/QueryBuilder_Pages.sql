select 
	Id,
	Name,
	(select top 1 TenantId from TenantDomains where Id=Pages.TenantDomainId) TenantId,
	--ViewPath,
	--Layout,
	--Apps,
	ViewParams,
	DefaultAccessibility,
	(select top 1 Name from PageCategories where Id=Pages.PageCategoryId) Category,
	(select top 1 BaseComponent from PageCategories where Id=Pages.PageCategoryId) Base,
	Presistant
from Pages
where
	1=1
	AND Presistant=1
	AND TenantDomainId in (select Id from TenantDomains where TenantId=4)
	----AND TenantDomainId in (select Id from TenantDomains where DomainId=1)
	----AND Name like '%AgentListEmbed%'
	AND PageCategoryId in (
		select Id
		from PageCategories
			where 
				1=1
				AND BaseComponent='Edit'
				AND Name like '%WorkOrder%'
	)