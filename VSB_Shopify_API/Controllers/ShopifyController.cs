using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace VSB_Shopify_API
{
    public class ShopifyController : ApiController
    {
        //Get Webservice and Country type from webconfig file.
        string ws_type = ConfigurationManager.AppSettings["WSTYPE"];
        string country_sw = ConfigurationManager.AppSettings["COUNTRY_SW"];
        string access_token = "?access_token=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";

        ws_functions wsf = new ws_functions();


        DataTable dt = new DataTable();
        base_return ret_obj = new base_return();

        // Sync Orders from Shopify
        [System.Web.Http.Route("Sync_Orders/")]
        public base_return POST()
        {
            //Instatiate Variables
            List<orders_template.Order> item_details = new List<orders_template.Order>();
            JsonResult json_return = new JsonResult();


            string return_value = "Success";
            string sql_script;
            string sql_check;
            string url = "https://up-bookmarks.myshopify.com/admin/api/2022-04/";
            string endpoint = "orders.json";

            //TEST
            if (ws_type == "TEST")
            {
                url = "https://up-bookmarks.myshopify.com/admin/api/2022-04/";
            }

            //LIVE
            if (ws_type == "LIVE")
            {
                url = "https://up-bookmarks.myshopify.com/admin/api/2022-04/";
            }

            //Build URL
            string full_url = url + endpoint + access_token;

            ret_obj = wsf.get_webrequest(full_url);

            if (ret_obj.status == 0)
            {
                //Convert Json to list of orders
                item_details = JsonConvert.DeserializeObject<orders_template.Rootobject>(ret_obj.message).orders.ToList();
                ret_obj = wsf.process_orders(item_details, country_sw);
            }



            return ret_obj;
        }
    }
}