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
using Microsoft.AspNetCore.Html;
using System.Linq.Expressions;

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

        private Mock<IHtmlHelper> GetHelper(List<string> paths = null, List<object> args = null)
        {
            var helperMock = new Mock<IHtmlHelper>();
            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());

            viewDictionary["ModelName"] = "model";
            viewDictionary["FormName"] = "Form";

            helperMock.SetupGet(d => d.ViewData).Returns(viewDictionary);
            if (paths != null || args != null)
            {
                helperMock.Setup(
                   d => d
                   .PartialAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<ViewDataDictionary>()))
                   .Callback((string x, object d, ViewDataDictionary dic) =>
                   {

                       paths?.Add(x);
                       args?.Add(d);
                   }
               );
            }


            return helperMock;
        }

        private Mock<IHtmlHelper<T>> GetHelper<T>(List<string> paths = null, List<object> args = null, HttpContext prov = null)
        {
            var helperMock = new Mock<IHtmlHelper<T>>();

            var mock2 = helperMock.As<IHtmlHelper>();
            var viewDictionary = new ViewDataDictionary<T>(new EmptyModelMetadataProvider(), new ModelStateDictionary());

            viewDictionary["ModelName"] = "model";
            viewDictionary["FormName"] = "Form";

            Expression<Func<IHtmlHelper, ViewDataDictionary>> ex = d => d.ViewData;

            helperMock.SetupGet(d => d.ViewData).Returns(viewDictionary);
            mock2.SetupGet(d => d.ViewData).Returns(viewDictionary);
            if (prov != null)
                mock2.SetupGet(e => e.ViewContext).Returns(new ViewContext { HttpContext = prov });

            helperMock.Setup(
                    d => d
                    .PartialAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<ViewDataDictionary>()))
                    .Callback((string x, object d, ViewDataDictionary dic) =>
                    {
                        paths?.Add(x);
                        args?.Add(d);
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

                var helperMock = GetHelper<User>(paths, args, sc.GetService<IHttpContextAccessor>().HttpContext);
                var dd = helperMock.Object.ViewData;
                var wt = service.CheckBoxCell(helperMock.Object, "name", "i", "lst", null, null, null, null, "", "");
                var st = wt.Write(InputControls.CheckBoxCell);

                Assert.Contains("~/ShellComponents/Angular/InputControls/CheckBoxCell.cshtml", paths);
                Assert.Contains("~/ShellComponents/Angular/Containers/Cell.cshtml", paths);
                Assert.AreEqual(2, args.Count);
                Assert.That(!args.Any(d => d == null), () => "some arguments are null");
            });
        }

        [Test]
        public void LocalizationTest()
        {
            RunScoped(sc =>
            {
                var h = GetHelper();
                h.Setup(d => d.Raw(It.IsAny<string>())).Returns((string d) =>
                {
                    return new HtmlString(d);
                });
                IHtmlContent c = h.Object.Word(TestEnum.One);

                Assert.AreEqual("{{'Words.TestEnum_One' | translate }}", c.ToString());
            });
        }
    }

    enum TestEnum { One, Two };
}
