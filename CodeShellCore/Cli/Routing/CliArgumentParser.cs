using CodeShellCore.Cli.Routing.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Cli.Routing
{
    public class CliArgumentParser<T> where T : class
    {
        public ICliRequestBuilder<T> Builder { get; private set; }
        public CliArgumentParser()
        {
            Builder = new CliRequestBuilder<T>();
        }
        public virtual T Parse(string[] args)
        {
            var t = Activator.CreateInstance<T>();
            ArgumentItem<T> valueFor = null;

            for (var i = 1; i < args.Length; i++)
            {
                if (valueFor != null)
                {
                    valueFor.WriteValue(t, args[i]);
                    valueFor = null;
                }
                else if (TryGetArgumentItem(args[i], out ArgumentItem<T> item))
                {
                    valueFor = item;
                }
            }

            if (Builder.IsInvalid(out string[] lst))
            {
                using (ColorSetter.Set(ConsoleColor.Red))
                {
                    foreach (var it in lst)
                    {
                        Console.WriteLine(it, ConsoleColor.Red);
                    }
                }
                return null;
            }
            return t;
        }

        protected virtual bool TryGetArgumentItem(string arg, out ArgumentItem<T> argItem)
        {
            argItem = null;
            if (!arg.StartsWith('-'))
                return false;

            if (arg.StartsWith("--"))
            {
                var key = arg.Replace("--", "");
                argItem = Builder.KeyList.FirstOrDefault(e => e.Key == key);
            }
            else
            {
                var key = arg.Replace("-", "")[0];
                argItem = Builder.KeyList.FirstOrDefault(e => e.CharacterSymbol == key);
            }

            return argItem != null;
        }
    }
}
