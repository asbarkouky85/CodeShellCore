﻿using System;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Text;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Moldster
            SqlParameter par = new SqlParameter("res", System.Data.SqlDbType.NVarChar,2000) { Direction = System.Data.ParameterDirection.Output };
            //string par = "";