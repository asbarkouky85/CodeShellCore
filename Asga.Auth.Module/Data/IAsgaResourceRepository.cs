using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Asga.Auth.Dto;
using CodeShellCore.Data;
using CodeShellCore.Security.Authorization;

namespace Asga.Auth.Data
{
    public interface IAsgaResourceRepository : IRepository<Resource>
    {
        IEnumerable<DomainWithResourcesDTO> GetClassifiedByDomain(string collectionId = null, Expression<Func<Resource, bool>> ex = null);
        IEnumerable<ResourceWithActionsDTO> GetResourcesWithActions(string collectionId = null, Expression<Func<Resource, bool>> ex = null);
    }
}