using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.UnitTest
{
    public class UnitTestClass
    {
        static bool _ran = false;
        protected void RunOnce(Action<IServiceProvider> provide)
        {
            if (!_ran)
            {
                RunScoped(provide);
                _ran = true;
            }
        }
        public void RunScoped(Action<IServiceProvider> action)
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
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw ex;
                }

                
            }
        }
    }
}
