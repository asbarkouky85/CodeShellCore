select 
	CONCAT(PP.Id,','),
	PP.ParameterValue,
	PCP.Id PCP_Id,
	PCP.Type,
	PCP.Name,
	PP.UseDefault,
	P.ViewPath,
	P.TenantId,
	PP.LinkedPageId,
	
	PCP.DefaultValue
	
 from PageParameters PP
join Pages P on P.Id=PP.PageId
join PageCategoryParameters PCP on PCP.Id=PP.PageCategoryParameterId
where 
--PP.Id=2006446510001 
(LinkedPageId is null OR LinkedPageId=0 ) AND
PCP.Type!=1 AND
 P.TenantId=1
order by ParameterValue;

--delete from PageRoutes where PageId in (select Id from Pages where TenantId=5)

--delete from PageCategoryParameters where Id=2008655127001;

--select * from Pages where Id=1911367359004