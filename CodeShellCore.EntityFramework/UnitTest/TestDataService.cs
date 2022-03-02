using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.UnitTest
{
    public interface ITestDataService
    {
        
    }

    public class TestDataService<T> : ITestDataService where T : DbContext
    {
    }
}
