using CodeShellCore.Cli;
using CodeShellCore.Moldster.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Cli
{
    public class WebpackConsoleController : ConsoleController
    {
        protected bool Lazy { get; private set; }
        IMoldsterService Moldster { get { return GetService<IMoldsterService>(); } }
        IDataService Data { get { return GetService<IDataService>(); } }
        public override Dictionary<int, string> Functions
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 1,"WriteWebPackFiles"},
                    { 2,"DevelopmentPack"},
                    { 3,"ProductionPack"},
                    { 4,"PublicProductionPack"},
                    { 5,"VendorPack"}
                };
            }
        }

        public void WriteWebPackFiles()
        {
            Moldster.WriteWebpackConfigFiles(Lazy);
        }

        public void VendorPack()
        {
            var prod = GetBoolFromUser("Production?");
            Moldster.VendorPack(prod);
        }

        public void DevelopmentPack()
        {
            Moldster.DevelopmentPack();
        }


        public void ProductionPack()
        {
            while (true)
            {
                var ten = Data.GetModuleCodes();
                string modCode = GetSelectionFromUser("Select Module", ten);

                string version = Moldster.GetUIVersion();
                var res = Moldster.ProductionPack(modCode, version);
                if (res.Code != 0)
                {
                    using (ColorSetter.Set(ConsoleColor.Red))
                    {
                        Console.WriteLine(res.Message);
                    }

                }
            }

        }

        public void PublicProductionPack()
        {
            string version = Moldster.GetUIVersion();
            var res = Moldster.ProductionPack("Public", version);
            if (res.Code != 0)
            {
                using (ColorSetter.Set(ConsoleColor.Red))
                {
                    Console.WriteLine(res.Message);
                }

            }
        }
    }
}
