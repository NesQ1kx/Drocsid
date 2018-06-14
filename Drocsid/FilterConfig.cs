using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drocsid
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var extHandleErrorAttribute = DependencyResolver.Current.GetService<ExtHandleErrorAttribute>();// new ExtHandleErrorAttribute();
            filters.Add(extHandleErrorAttribute);
        }
    }
}