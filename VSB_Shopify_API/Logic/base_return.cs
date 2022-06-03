using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSB_Shopify_API
{
    public class base_return
    {
        //properties
        public int status { get; set; }
        public string message { get; set; }
        public string parm_extra { get; set; }


        public base_return()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}