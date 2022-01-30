using CodeShellCore.Data;
using CodeShellCore.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Business
{
    public class PublicEntityService<T> : EntityService<T>
        where T : class,IAsgaPublicModel
    {
        public PublicEntityService(IPublicUnit unit) : base(unit)
        {
        }
    }
}
