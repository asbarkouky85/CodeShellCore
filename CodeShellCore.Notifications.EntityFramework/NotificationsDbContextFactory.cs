using CodeShellCore.EntityFramework.DesignTime;

namespace CodeShellCore.Notifications
{
    public class NotificationsDbContextFactory : CodeShellDesignTimeDbContextFactory<NotificationsContext>
    {
        protected override string ConnectionStringKey => "Notifications";

        public NotificationsDbContextFactory()
        {

        }


    }
}
