using Asga.Public.Data;
using Asga.Public.Dto;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Services
{
    public class PublicDataService : ServiceBase, IPublicDataService
    {
        private readonly IPublicUnit unit;

        public PublicDataService(IPublicUnit unit)
        {
            this.unit = unit;
        }

        public HomePageData Get()
        {
            return new HomePageData
            {
                HomeSlides = unit.HomeSlideRepository.GetValues(d => d.Image, d => d.IsActive)
            };
        }

        public FooterModel GetFooterData()
        {
            return new FooterModel
            {
                Contacts = unit.ContactItemRepository.FindAs(d => new ContactDTO
                {
                    Icon = d.ContactItemType.Icon,
                    Value = d.Value
                }, d => d.IsVisible)
            };
        }
    }
}
