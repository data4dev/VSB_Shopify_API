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
        string status_type = ConfigurationManager.AppSettings["CREATE_TYPE"];
        string access_token = "?access_token=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";
        string url = "https://up-bookmarks.myshopify.com/admin/api/2022-04/";

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

        // Sync Products from Shopify
        [System.Web.Http.Route("Sync_Products/")]
        public base_return POST_Products()
        {
            //Instatiate Variables
            //List<product_template.products> item_details = new List<product_template.products>();
            JsonResult json_return = new JsonResult();

            //Sync shopify_datafeed with any new items that pass validation rules
            wsf.sync_shopify_datafeed_products(country_sw);

            //process synced products
            ret_obj = wsf.process_products(country_sw,url,access_token, status_type);

            //string endpoint = "products.json";
            //string count_endpoint = "products/count.json";
            //string extra_info = "&index=";

            ////Build Count URL
            //string full_url = url + count_endpoint + access_token;

            ////Check amount of items
            //ret_obj = wsf.get_webrequest(full_url);

            //int product_count = 0;
            //int total_index = 0;
            //var product_counter = ret_obj.message.Substring(9, ret_obj.message.Length - 10);

            //try
            //{
            //    product_count = int.Parse(product_counter);
            //    total_index = product_count / 50;
            //}
            //catch (Exception ex)
            //{
            //    //audit error
            //}

            ////build index amount
            //for (int i = 0; i < total_index; i++)
            //{
            //    //Build Count URL
            //    full_url = url + endpoint + access_token + extra_info + i;
            //    ret_obj = wsf.get_webrequest(full_url);

            //    string formated_json = ret_obj.message.Replace("null", "");

            //    if (ret_obj.status == 0)
            //    {
            //        //Convert Json to list of orders
            //        item_details = JsonConvert.DeserializeObject<product_template.RootObject>(ret_obj.message).product_list.ToList();
            //    }

          
            //}

            return ret_obj;
        }


        // Sync Products from Shopify
        [System.Web.Http.Route("Add_Product/")]
        public base_return POST([FromBody] product_template.products sp)
        {
            //Instatiate Variables
            JsonResult json_return = new JsonResult();

            string return_value = "Success";
            string sql_script;
            string sql_check;

            string endpoint = "products.json";

            //Build Count URL
            string full_url = url + endpoint + access_token;

            string json_body = "{" +
                    "\"product\":{" +
                    "\"title\":\"Rocco Test Item 4\"," +
                    "\"body_html\":\"\u003cstrong\u003eTesting Items!\u003c\\/strong\u003e\"," +
                    "\"vendor\":\"Dev\"," +
                    "\"product_type\":\"Book\"," +
                    "\"status\":\"draft\"," +
                    "\"variants\":[" +
                    "   {" +
                    "   \"title\":\"Testing Items V1.0\"," +
                    "   \"price\":\"1.30\"," +
                    "   \"inventory_quantity\":\"5\"," +
                    "   \"barcode\":\"Test1234\"" +
                    "   }" +
                    "]," +
                    "\"image\":\"https://source.unsplash.com/oWTW-jNGl9I/600x800\"" +
                    "          }" +
                    "      }";

            //Check amount of items
            ret_obj = wsf.post_webrequest(full_url, json_body);

            int product_count = 0;
            int total_index = 0;
            var product_counter = ret_obj.message.Substring(9, ret_obj.message.Length - 10);

            try
            {
                product_count = int.Parse(product_counter);
                total_index = product_count / 50;
            }
            catch (Exception ex)
            {
                //audit error
            }

            return ret_obj;
        }


    }
}