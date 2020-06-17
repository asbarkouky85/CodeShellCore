using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace CodeShellCore.Types
{
    public class Keyss
    {
        public string Key1 { get; set; }
        public string Key2 { get; set; }
    }
    public class DefaultCallSiteBinder : CallSiteBinder
    {
        public override Expression Bind(object[] args, ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel)
        {
            
            throw new NotImplementedException();
        }
    }
}
