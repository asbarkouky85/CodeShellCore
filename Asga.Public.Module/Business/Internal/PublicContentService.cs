using Asga.Public.Business;
using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Business.Internal
{

    public class PublicContentService : PublicEntityService<PublicContent>, IPublicContentService
    {
        private readonly IPublicUnit _unit;
        private readonly Language lang;

        public PublicContentService(IPublicUnit unit,Language lang) : base(unit)
        {
            _unit = unit;
            this.lang = lang;
        }

        public virtual string GetContentPage(string id)
        {
            var l = lang.Culture.TwoLetterISOLanguageName.ToLower();
            return _unit.PublicContentRepository.GetSingleValue(d => d.Content, d => d.Code == id && (d.Locale == l || d.Locale == null));
        }

        public virtual PublicContent GetByCode(string code, string lang)
        {
            var s = Repository.FindSingle(d => d.Code == code && d.Locale == lang);
            if (s == null)
            {
                s = new PublicContent { Code = code, Locale = lang, IsActive = true };
                Repository.Add(s);
                UnitOfWork.SaveChanges();
            }
            return s;
        }
    }
}
