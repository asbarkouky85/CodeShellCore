using Asga.Data;
using Asga.Public.Data;
using CodeShellCore.Data;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Services
{
    public class AboutSectionService : EntityService<AboutSection>, IAboutSectionService
    {
        private readonly Language lang;

        public AboutSectionService(IPublicUnit unit, Language lang) : base(unit)
        {
            this.lang = lang;
        }

        public LoadResult<AboutSection> LoadLocalized(LoadOptions opts)
        {

            return Repository.FindAs(d => new AboutSection
            {
                Text = DbFunctions.GetLocalized("AboutSection", d.Id, lang.Culture.LCID, "Text", d.Text)
            }, opts.GetOptionsFor<AboutSection>());
        }
    }
}
