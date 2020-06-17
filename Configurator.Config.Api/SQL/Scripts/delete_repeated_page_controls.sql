delete from PageControls
where Id in (
	select Id 
	from (
		select 
			ROW_NUMBER() over (partition by PC.ControlId,PC.PageId order by PC.Id ) REP,
			PC.Id 
		from PageControls PC
		where exists(

			select PageId,ControlId from PageControls
				join Controls on Controls.Id=PageControls.ControlId
				join Pages on Pages.Id=PageControls.PageId
			group by PageId,ControlId,Controls.Identifier,PageControls.SourceCollectionId,Accessability 
			having count(*) > 1 AND PageId=PC.PageId AND ControlId=PC.ControlId
		)
	) tx
	where 
	REP>1
)
