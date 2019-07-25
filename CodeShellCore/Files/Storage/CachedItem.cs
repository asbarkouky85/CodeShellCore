using System;
using System.Reflection;
using System.Linq;

using CodeShellCore.Text;
using CodeShellCore.MQ.Events;
using CodeShellCore.MQ;


namespace CodeShellCore.Files.Storage
{
    public class CachedItem
    {
        public string AssemblyName { get; set; }
        public string TypeName { get; set; }
        public string Data { get; set; }
        public string ExceptionData { get; set; }
        public DateTime Time { get; set; }
        public CachedItem() { }

        public CachedItem(object ob, Exception ex = null)
        {
            AssemblyName = ob.GetType().Assembly.FullName;
            TypeName = ob.GetType().FullName;
            Data = ob.ToJson();
            Time = DateTime.Now;
            if (ex != null)
                ExceptionData = ex.GetMessageRecursive();
        }

        public object GetData()
        {
            return Data.FromJson(GetDataType());
        }

        public Type GetDataType()
        {
            Assembly ass = Assembly.Load(AssemblyName);
            return ass.GetType(TypeName);
        }

        public void RunHandler(IEventHandler con)
        {
            var type = GetDataType();

            var meth = con.GetType().GetMethod("Handle", new Type[] { type });


            if (meth == null)
            {
                if (type.BaseType == typeof(CrudEventBase))
                {
                    Type t = type.GetGenericArguments().FirstOrDefault();
                    meth = con.GetType().GetMethod("Handle").MakeGenericMethod(t);
                }
                else
                {
                    meth = con.GetType().GetMethod("Handle").MakeGenericMethod(type);
                }
            }

            meth.Invoke(con, new object[] { GetData() });
        }

    }
}
