using System;
using CodeShellCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Helpers;
using CodeShellCore.Security;
using Asga.Security;
using CodeShellCore.Data.ConfiguredCollections;

namespace Asga.Common.Data
{
    public class AsgaUnitBase<T> : CollectionEFUnit<T> where T : DbContext
    {
        public override Action<ChangeLists> OnBeforeSave { get { return beforeSave; } }
        //public override Action<ChangeLists> OnSaveSuccess { get { return afterSave; } }
        IUserAccessor _acc;
        public AsgaUnitBase(IServiceProvider provider):base(provider)
        {
            _acc = provider.GetService<IUserAccessor>();
        }

        //private void afterSave(ChangeLists lsts)
        //{
        //    if (lsts.Added.Count > 0)
        //    {
        //        foreach (object o in lsts.Added)
        //        {
        //            if (o is ISharedModel)
        //            {
        //                CrudEventBase ev = EventFactory.GetAddedEvent((ISharedModel)o, AsgaShell.CurrentTenant);
        //                Transporter.Publish(ev, ev.GetType());
        //            }
        //        }
        //    }

        //    if (lsts.Updated.Count > 0)
        //    {
        //        foreach (object o in lsts.Updated)
        //        {
        //            if (o is ISharedModel)
        //            {
        //                CrudEventBase ev = EventFactory.GetUpdatedEvent((ISharedModel)o, AsgaShell.CurrentTenant);
        //                Transporter.Publish(ev, ev.GetType());
        //            }
        //        }
        //    }

        //    if (lsts.Deleted.Count > 0)
        //    {
        //        foreach (object o in lsts.Deleted)
        //        {
        //            if (o is ISharedModel)
        //            {
        //                CrudEventBase ev = EventFactory.GetDeletedEvent((ISharedModel)o, AsgaShell.CurrentTenant);
        //                Transporter.Publish(ev, ev.GetType());
        //            }
        //        }
        //    }
        //}

        private void beforeSave(ChangeLists lst)
        {
            var user = (UserDTO)_acc.User;
            long? userId = user?.Id;

            foreach (IAsgaModel mod in lst.Added)
            {
                if (mod.Id == 0)
                    mod.Id = Utils.GenerateID();
                if (userId != null)
                {
                    mod.CreatedBy = userId;
                    mod.UpdatedBy = userId;
                }

                if (mod.CreatedOn == null)
                    mod.CreatedOn = DateTime.Now;
                mod.UpdatedOn = DateTime.Now;

            }

            foreach (IAsgaModel mod in lst.Updated)
            {
                if (userId != null)
                    mod.UpdatedBy = userId;
                mod.UpdatedOn = DateTime.Now;

            }


        }

    }

}
