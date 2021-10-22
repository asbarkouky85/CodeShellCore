using CodeShellCore.Moldster.CodeGeneration.Dtos;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.CodeGeneration.Services
{
    public interface IAngularJsonService
    {
        AngularJsonFile ReadFile();
        void UpdateFileFromDatabase();
    }
}