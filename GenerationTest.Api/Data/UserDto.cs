using CodeShellCore.Data;
using System;

namespace GenerationTest.Api.Data
{
    public class UserDto : EntityDto<long>
    {
        public string Name { get; set; }
        public long AppId { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
