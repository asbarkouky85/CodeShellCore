using System;
using Asga.Common.Data;
using CodeShellCore.Data;
using CodeShellCore.Security;

namespace Asga.Public.Data
{
    public class PublicUnit : AsgaUnitBase<PublicContext>, IPublicUnit
    {
        public PublicUnit(IServiceProvider acc) : base(acc)
        {
        }

        protected override Type GenericRepositoryType => typeof(AsgaRepository<,>);

        public IRepository<HomeSlide> HomeSlideRepository => GetRepositoryFor<HomeSlide>();

        public IRepository<ContactItem> ContactItemRepository => GetRepositoryFor<ContactItem>();
    }
}
