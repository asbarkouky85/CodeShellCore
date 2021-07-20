using CodeShellCore.Moldster.Definitions.AngularJson;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public interface IAngularJsonService
    {
        AngularJsonFile ReadFile();
         void UpdateFileFromDatabase();
    }
}