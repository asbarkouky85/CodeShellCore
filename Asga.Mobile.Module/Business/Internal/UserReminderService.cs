using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile.Business.Internal
{
    public class UserReminderService : MobileEntityService<UserReminder>
    {
        private readonly IModuleNotificationService prov;

        public UserReminderService(IAsgaMobileUnit unit,IModuleNotificationService prov) : base(unit)
        {
            this.prov = prov;
        }

        public SubmitResult AddReminder(long userId, UserReminder rem)
        {
            rem.TextId = "offer_reminder";
            rem.EntityType = "Offer";
            rem.UserId = userId;
            Unit.UserReminderRepository.Add(rem);
            return Unit.SaveChanges();
        }

        public SubmitResult DeleteReminder(long id)
        {
            Unit.UserReminderRepository.DeleteById(id);
            return Unit.SaveChanges();
        }

        public LoadResult<ReminderListDTO> GetReminders(long userId, LoadOptions op)
        {
            var opts = op.GetOptionsFor<ReminderListDTO>();
            if (op.OrderProperty == null)
            {
                opts.OrderProperty = "ReminderTime";
                opts.Direction = SortDir.DESC;
            }
            var res = Unit.UserReminderRepository.FindAs(ReminderListDTO.ReminderExpression, opts, d => d.UserId == userId);

            prov.FillAutoData(res.ListT);

            return res;
        }
    }
}
