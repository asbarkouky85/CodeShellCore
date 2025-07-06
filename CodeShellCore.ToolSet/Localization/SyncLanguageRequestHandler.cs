using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Localization
{
    public class SyncLanguageRequestHandler : CliRequestHandler<SyncLanguagesRequest>
    {
        public override string FunctionDescription => "Sync Resx Files";
        ILocalizationDictionariesService service => Store.GetService<ILocalizationDictionariesService>();
        public SyncLanguageRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<SyncLanguagesRequest> builder)
        {
            builder.Property(e => e.MainDirectory, "folder", "d", 1, isRequired: true);
            builder.Property(e => e.Lang1, "lang1", "s",2);
            builder.Property(e => e.Lang2, "lang2", "t",3);
        }

        protected override async Task<Result> HandleAsync(SyncLanguagesRequest request, CancellationToken token)
        {
            await service.SyncLanguages(request.MainDirectory, request.Lang1, request.Lang2);
            return new Result(0);
        }
    }
}
