using CodeShellCore.Localization;
using CodeShellCore.Text.Localization;
using System.Threading.Tasks;

namespace CodeShellCore.Locatization
{
    public class LocalizationDictionariesService : ILocalizationDictionariesService
    {
        ILocaleTextProvider _textProvider;
        private readonly Language lang;

        public LocalizationDictionariesService(ILocaleTextProvider textProvider, Language lang)
        {
            _textProvider = textProvider;
            this.lang = lang;
        }

        public Task<LocalizationDictionaryDto> Get(LocalizationDictionariesRequestDto dto)
        {
            return Task.Run(() =>
            {
                var cult = dto.Lang ?? lang.Culture.TwoLetterISOLanguageName;
                return new LocalizationDictionaryDto
                {
                    Columns = _textProvider.GetAllColumns(cult),
                    Messages = _textProvider.GetAllMessages(cult),
                    Pages = _textProvider.GetAllPages(cult),
                    Words = _textProvider.GetAllWords(cult)
                };
            });
        }
    }
}
