using System;
using System.Collections.Generic;
using System.Text;

//Add reference to System.Web if not yet referenced
using System.Web;
using System.Web.Configuration;

namespace Luminious.Connection
{
    public abstract class Configuration
    {
        public static String ConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["abcd"].ToString();
            }
        }

       
    }
}
