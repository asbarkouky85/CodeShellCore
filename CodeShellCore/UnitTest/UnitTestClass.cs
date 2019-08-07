using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.UnitTest
{
    public class UnitTestClass
    {
        public void RunTest(Action<IServiceProvider> action)
        {
            using (var sc = Shell.GetScope())
            {
                UnitTestShell.CurrentScope = sc;
                try
                {
                    action.Invoke(sc.ServiceProvider);
                    UnitTestShell.CurrentScope = null;
                }
                catch (Exception ex)
                {
                    UnitTestShell.CurrentScope = null;
                    Console.WriteLine("TEST ERROR : ");
                    Console.WriteLine(ex.GetMessageRecursive());
                    throw ex;
                }

                
            }
        }
    }
}
