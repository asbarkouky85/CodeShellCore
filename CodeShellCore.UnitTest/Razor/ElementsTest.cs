using CodeShellCore.Web.Razor;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Web.Razor.Tables.Angular;
using Moq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using CodeShellCore.Web.UnitTest;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using Asga.Auth;

namespace CodeShellCore.UnitTest.Razor
{
    [TestFixture]
    public class ElementsTest : UnitTestClass
    {
        [SetUp]
        public void SetUp()
        {
            var shell = new UnitTestShell(coll =>
             {
                 coll.AddCodeShellEmbeddedViews();
                 coll.AddAngularRazorHelpers();
                 coll.AddScoped<IHttpContextAccessor, TestHttpContextAccessor>();
             });
            Shell.Start(shell);
            shell.ConfigureAngular2Razor();
        }

        private Mock<IHtmlHelper<T>> GetHelper<T>(List<string> paths, List<object> args)
        {
            var helperMock = new Mock<IHtmlHelper<T>>();
            var viewDictionary = new ViewDataDictionary<T>(new EmptyModelMetadataProvider(), new ModelStateDictionary());

            viewDictionary["ModelName"] = "model";
            viewDictionary["FormName"] = "Form";

            helperMock.SetupGet(d => d.ViewData).Returns(viewDictionary);
            helperMock.Setup(
                    d => d
                    .PartialAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<ViewDataDictionary>()))
                    .Callback((string x, object d, ViewDataDictionary dic) =>
                    {
                        paths.Add(x);
                        args.Add(d);
                    }
                );

            return helperMock;
        }

        [Test]
        public void CheckBoxCell()
        {
            RunScoped(sc =>
            {
                var service = sc.GetService<IAngularTablesHelper>();

                List<string> paths = new List<string>();
                List<object> args = new List<object>();

                var helperMock = GetHelper<User>(paths, args);

                var wt = service.CheckBoxCell(helperMock.Object, "name", "i", "lst", null, null, null, null, "");
                var st = wt.Write(InputControls.CheckBoxCell);

                Assert.Contains("~/ShellComponents/Angular/InputControls/CheckBoxCell.cshtml", paths);
                Assert.Contains("~/ShellComponents/Angular/Containers/Cell.cshtml", paths);
                Assert.AreEqual(2, args.Count);
                Assert.That(!args.Any(d => d == null), () => "some arguments are null");
            });
        }
    }
}
