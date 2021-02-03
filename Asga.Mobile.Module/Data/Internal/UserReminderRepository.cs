using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile.Data.Internal
{
    public class UserReminderRepository : AsgaMobileRepository<UserReminder, AsgaMobileContext>
    {
        public UserReminderRepository(AsgaMobileContext con) : base(con)
        {
        }

       
    }
}
