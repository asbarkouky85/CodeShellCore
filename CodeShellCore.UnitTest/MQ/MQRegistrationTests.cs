using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.MQ;
using Moq;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.UnitTest.MQ
{
    [TestFixture]
    public class MQRegistrationTests : UnitTestClass
    {

        [SetUp]
        public void Startup()
        {

        }

        [Test]
        public void TestEvents()
        {
            long addedUserId = 0;
            Shell.Start(new UnitTestShell(coll =>
            {
                coll.Services.AddDefaultServiceBus(d =>
                {
                    d.AddConsumer<UsersConsumer>();
                });
                Mock<ITestUserService> mock = new Mock<ITestUserService>();
                mock.Setup(d => d.UserIsAdded(It.IsAny<UserAdded>())).Callback<UserAdded>(d => { addedUserId = d.Id; });
                coll.Services.AddSingleton<ITestUserService>(mock.Object);
            }));

            Transporter.Publish(new UserAdded { Id = 254 });

            Assert.AreEqual(254, addedUserId);
        }
    }
}
