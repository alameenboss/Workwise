﻿using System.Web.Http;
using System.Web.Mvc;

namespace Workwise.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
    }
}
