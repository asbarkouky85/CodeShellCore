using Asga.Common.Data;
using Asga.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Data
{
    public class AboutSectionRepository : AsgaRepository<AboutSection, PublicContext>
    {
        public AboutSectionRepository(PublicContext con, AsgaCollectionService service) : base(con, service)
        {
        }

        
    }
}
