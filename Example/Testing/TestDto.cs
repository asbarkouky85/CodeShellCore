using CodeShellCore.Data;
using CodeShellCore.Localization;

namespace Example.Testing
{
    [EntityName("Test")]
    public class TestDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
