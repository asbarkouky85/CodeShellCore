using CodeShellCore.Data;
using CodeShellCore.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile.Business
{
    public class MobileEntityService<T> : EntityService<T>
        where T : class, IModel<long>
    {
        protected readonly IAsgaMobileUnit Unit;

        public MobileEntityService(IAsgaMobileUnit unit) : base(unit)
        {
            this.Unit = unit;
        }
    }
}
