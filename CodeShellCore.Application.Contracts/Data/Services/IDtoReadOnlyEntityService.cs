﻿using CodeShellCore.Linq;

namespace CodeShellCore.Data.Services
{
    public interface IDtoReadOnlyEntityService<TPrime, TOptionsDto, TListDto, TGetDto>
        where TGetDto : class
        where TListDto : class
        where TOptionsDto : LoadOptions
    {
        LoadResult<TListDto> Get(TOptionsDto opts);
        TGetDto GetSingle(TPrime id);
        bool IsUnique(PropertyUniqueDTO dto);
    }
}
