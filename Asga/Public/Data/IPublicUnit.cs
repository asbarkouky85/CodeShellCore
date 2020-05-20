using Asga.Public.Dto;
using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Data
{
    public interface IPublicUnit : IUnitOfWork
    {
        IRepository<HomeSlide> HomeSlideRepository { get; }
        IRepository<ContactItem> ContactItemRepository { get; }
    }
}
