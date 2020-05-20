using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.MQ;
using CodeShellCore.Security;
using CodeShellCore.Text.Localization;
using CodeShellCore.Text.Localization.Internal;
using Asga.Common.DTO;
using Asga.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asga.Common.Services
{
    public class LocalizationCommonService<T> : LocalizationDataService<T> where T : class, ILocalizable
    {
        private IUserAccessor _user;
        private readonly CurrentTenant _current;

        public LocalizationCommonService(IUnitOfWork unit, Language lang, IUserAccessor user,CurrentTenant current) : base(unit, lang)
        {
            _user = user;
            _current = current;
        }
        public override SubmitResult SetDataFor(Type type, object id, Dictionary<string, LocalizablesDTO> dto)
        {
            var res = base.SetDataFor(type, id, dto);
            
            return res;
        }
    }
}
