using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace VSB_Shopify_API
{
    public class ws_functions
    {

        public base_return get_webrequest(string full_url)
        {
            base_return ret_obj = new base_return();
            string json_server_response = "";

            ret_obj.status = 0;
            ret_obj.message = "";
            ret_obj.parm_extra = "";

            try
            {
                WebRequest get_request = WebRequest.Create(full_url);

                get_request.Method = "GET";
                get_request.ContentType = "application/json";

                using (WebResponse get_response = get_request.GetResponse())
                {
                    Stream data_stream = get_response.GetResponseStream();
                    StreamReader data_reader = new StreamReader(data_stream);
                    json_server_response = data_reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                ret_obj.status = -999;
                ret_obj.message = ex.Message.ToString();
                ret_obj.parm_extra = "Error getting information from shopify.";
                return ret_obj;
            }

            ret_obj.message = json_server_response;

            return ret_obj;
        }

        public base_return process_orders(List<orders_template.Order> order_list, string country_sw)
        {
            base_return ret_obj = new base_return();
            DbCode db = new DbCode();

            string sql_check = "";
            string sql_return = "";
            string sql_insert = "";
            int insert_check = 0;
            //Set database connection
            db.set_db_country(country_sw);

            ret_obj.status = 0;
            ret_obj.message = "success";
            ret_obj.parm_extra = "";


            foreach (var order in order_list)
            {
                //clear scripts
                sql_insert = "";

                //Check if order exists.
                sql_check = "select count(*) from shopify_orders_hdr where shopify_order_number = '" + order.order_number + "'";
                sql_return = db.odbcSQLLookupValue(sql_check);

                if (sql_return == "0")
                {
                    //Add Order to database


                    //Build header insert
                    sql_insert = "insert into shopify_orders_hdr values('" + order.order_number + "','" + order.order_status_url + "','" + order.reference + "','" + order.processing_method + "','" + order.confirmed + "'); ";

                    //Build detail insert
                    foreach (var line_item in order.line_items)
                    {
                        sql_insert = sql_insert + "insert into shopify_orders_dtl values('" + line_item.title + "','" + line_item.product_id + "','" + line_item.taxable + "','" + line_item.price + "','" + line_item.quantity + "'); ";
                    }

                    sql_insert = sql_insert + "COMMIT; ";

                   //insert_check = db.sqlInsert(sql_insert);

                    if (insert_check != 0)
                    {
                        //Audit Insert Script
                    }
                }

                if (sql_return == "1")
                {
                    //Update status of order if required
                }
            }

            db.close_connection();

            return ret_obj;
        }
    }
}