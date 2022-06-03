using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

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

        public base_return post_webrequest(string full_url, string request_body)
        {
            base_return ret_obj = new base_return();
            string json_server_response = "";

            ret_obj.status = 0;
            ret_obj.message = "";
            ret_obj.parm_extra = "";

            try

            {
                WebRequest request = WebRequest.Create(full_url);
                request.Method = "POST";
                string postData = request_body;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response1 = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response1).StatusDescription);
                dataStream = response1.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
                reader.Close();
                dataStream.Close();
                response1.Close();

                json_server_response = responseFromServer;
            }
            catch (Exception ex)
            {
                ret_obj.status = -999;
                ret_obj.message = ex.Message.ToString();
                ret_obj.parm_extra = "Error Adding information to shopify.";
                return ret_obj;
            }

            ret_obj.message = json_server_response;

            return ret_obj;
        }

        public base_return put_webrequest(string full_url, string request_body)
        {
            base_return ret_obj = new base_return();
            string json_server_response = "";

            ret_obj.status = 0;
            ret_obj.message = "";
            ret_obj.parm_extra = "";

            try

            {
                WebRequest request = WebRequest.Create(full_url);
                request.Method = "PUT";
                string postData = request_body;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response1 = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response1).StatusDescription);
                dataStream = response1.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
                reader.Close();
                dataStream.Close();
                response1.Close();

                json_server_response = responseFromServer;
            }
            catch (Exception ex)
            {
                ret_obj.status = -999;
                ret_obj.message = ex.Message.ToString();
                ret_obj.parm_extra = "Error updating information to shopify.";
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

        public void sync_shopify_datafeed_products(string country_sw)
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

            sql_insert = "CALL sp_sync_shopify_items()";
            sql_return = db.odbcSQLLookupValue(sql_insert);

        }

        public base_return process_products(string country_sw, string url, string access_token, string status, string post_sw)
        {
            //live_sw = 1 :Send to Shopify
            //Live_sw = 0 :Audit Only

            base_return ret_obj = new base_return();
            DbCode db = new DbCode();

            string sql_check = "";
            string sql_return = "";
            string sql_update = "";
            string sql_insert = "";
            int insert_check = 0;
            //Set database connection
            db.set_db_country(country_sw);

            ret_obj.status = 0;
            ret_obj.message = "success";
            ret_obj.parm_extra = "";

            //Get List Of Items to process
            DataTable product_modified_list = db.getShopifyDataFeedItemsDataTable();

            foreach (DataRow data_line in product_modified_list.Rows)
            {
                string action_type = data_line["action_type"].ToString();
                string item_id = data_line["item_id"].ToString();
                string shopify_id = data_line["shopify_id"].ToString();
                string variant_id = data_line["variant_id"].ToString();
                string title = data_line["title"].ToString();
                string inventory_qty = data_line["inventory_qty"].ToString();
                string isbn_number = data_line["isbn_number"].ToString();
                string inventory_price = data_line["selling_price"].ToString();
                string item_status = data_line["current_status"].ToString();

                //Insert New Item into shoppify
                if (action_type == "1")
                {
                    ret_obj = insert_products(data_line, url, access_token, status, country_sw, post_sw);

                    if (ret_obj.status == 0)
                    {
                        audit_api(country_sw, "Insert Shopify Item", ret_obj.status.ToString(), "Success", ret_obj.parm_extra, ret_obj.message);
                    }
                    else
                    {
                        audit_api(country_sw, "Error: Insert Shopify Item", ret_obj.status.ToString(),"Failed", ret_obj.parm_extra, ret_obj.message);
                    }
                }

                //Update Current Item into shoppify
                if (action_type == "2")
                {
                    product_template.products line_update = new product_template.products();
                    line_update.id = int.Parse(shopify_id);
                    line_update.title = title;
                    line_update.body_html = title;
                    line_update.variants[1].product_id = int.Parse(shopify_id);
                    line_update.variants[1].barcode = isbn_number;
                    line_update.variants[1].barcode = isbn_number;
                    line_update.variants[1].price = float.Parse(inventory_price);
                    line_update.variants[1].inventory_quantity = int.Parse(inventory_qty);

                    update_products(line_update, url, access_token, country_sw, post_sw);

                    if (ret_obj.status == 0)
                    {
                        audit_api(country_sw, "Update Shopify Item", ret_obj.status.ToString(), "Success", ret_obj.parm_extra, ret_obj.message);

                        if (post_sw == "YES")
                        {
                            sql_insert = "UPDATE shopify_datafeed " +
                            "SET modified = 0 " +
                            //" ,inventory_qty  = '" + inventory_qty + "' " +
                            //" ,selling_price  = '" + inventory_price + "' " +
                            ",date_modified = now() " +
                            "WHERE shopify_id = '" + shopify_id + "' " +
                            "AND variant_id = '" + variant_id + "' " +
                            "AND item_id = '" + item_id + "'; ";

                            sql_insert = sql_insert + "COMMIT; ";
                            insert_check = db.sqlInsert(sql_insert);

                            if (insert_check != 0)
                            {
                                //Audit Request
                                audit_api(country_sw, "Error: Update shopify_datafeed", "0", "Failed", sql_insert, item_id);
                            }
                        }
                    }
                    else
                    {
                        audit_api(country_sw, "Error: Update Shopify Item", ret_obj.status.ToString(), "Failed", ret_obj.parm_extra, ret_obj.message);
                    }
                }
            }

            db.close_connection();

            return ret_obj;
        }

        public base_return update_products(product_template.products product, string url, string access_token, string country_sw, string post_sw)
        {
            ws_functions wsf = new ws_functions();
            base_return ret_obj = new base_return();
            DbCode dbInt = new DbCode();
            DataTable dt;

            string endpoint = "products/" + product.id.ToString() + ".json";

            ret_obj.status = 0;
            ret_obj.message = "Success";
            ret_obj.parm_extra = "";

            dbInt.set_db_country(country_sw);

            //check if database connection is active
            int db_active = dbInt.checkDBConnection();
            if (db_active == -1)
            {
                ret_obj.status = -999;
                ret_obj.message = "Error Connection to database";
            }

            //Build Count URL
            string full_url = url + endpoint + access_token;

            string json_body = "{" +
                            "\"product\":{" +
                                "\"id\": " + product.id + "," +
                                "\"title\":\"" + product.title + "\"," +
                                "\"body_html\":\"\u003cstrong\u003e" + product.title + "\u003c/strong\u003e\"," +
                                "\"variants\":[" +
                                    "{" +
                                        "\"product_id\": " + product.variants[1].product_id + "," +
                                        "\"price\":\"" + product.variants[1].price + "\"," +
                                        "\"inventory_quantity\":" + product.variants[1].inventory_quantity + "," +
                                        "\"barcode\":\"" + product.variants[1].barcode + "\" " +
                                    " }" +
                                " ]" +
                                " }" +
                            " }";

            if (post_sw == "YES")
            {
                //Check amount of items
                ret_obj = wsf.put_webrequest(full_url, json_body);
            }

            ret_obj.parm_extra = json_body;

            dbInt.close_connection();

            return ret_obj;
        }

        public base_return insert_products(DataRow dr_shopify_items, string url, string access_token, string status, string country_sw, string post_sw)
        {
            //Instatiate Variables
            JsonResult json_return = new JsonResult();
            ws_functions wsf = new ws_functions();
            base_return ret_obj = new base_return();

            DbCode db = new DbCode();
            db.set_db_country(country_sw);

            ret_obj.status = 0;
            ret_obj.message = "Success";
            ret_obj.parm_extra = "";

            //check if database connection is active
            int db_active = db.checkDBConnection();
            if (db_active == -1)
            {
                ret_obj.status = -999;
                ret_obj.message = "Error Connection to database";
            }

            string endpoint = "products.json";

            string ls_type = dr_shopify_items["item_type"].ToString();
            string ls_department = dr_shopify_items["dept_id"].ToString();
            string ls_availibility = dr_shopify_items["availibility"].ToString();
            string ls_published = dr_shopify_items["published"].ToString();
            string ls_item_id = dr_shopify_items["item_id"].ToString();
            string ls_isbn_number = dr_shopify_items["isbn_number"].ToString();
            string ls_title = dr_shopify_items["title"].ToString();
            string ls_barcode = dr_shopify_items["barcode"].ToString();
            string ls_weight = dr_shopify_items["weight"].ToString();
            double ld_price = double.Parse(dr_shopify_items["selling_price"].ToString());
            string ls_contributor = dr_shopify_items["contributor"].ToString();
            string ls_edition = dr_shopify_items["edition"].ToString();
            string ls_publisher = dr_shopify_items["publisher"].ToString();
            string ls_category = dr_shopify_items["cat_id"].ToString();
            string ls_inventory_qty = dr_shopify_items["inventory_qty"].ToString();
            string ls_description = "No Description";
            string ls_Image = "";

            if (status == "1")
            {
                status = "draft";
            }

            if (status == "2")
            {
                status = "active";
            }

            //Build Count URL
            string full_url = url + endpoint + access_token;

            string json_body = "{" +
                    "\"product\":{" +
                    "\"title\":\"" + ls_title + "\"," +
                    "\"body_html\":\"\u003cstrong\u003e" + ls_description + "\u003c/strong\u003e\"," +
                    "\"vendor\":\"" + ls_publisher + "\"," +
                    "\"product_type\":\"" + ls_type + "\"," +
                    "\"status\":\"" + status + "\"," +
                    "\"variants\":[" +
                    "   {" +
                    "   \"title\":\"" + ls_title + "\"," +
                    "   \"price\":\"" + ld_price + "\"," +
                    "   \"inventory_quantity\":\"" + ls_inventory_qty + "\"," +
                    "   \"barcode\":\"" + ls_isbn_number + "\"" +
                    "   }" +
                    "]," +
                    "\"image\":\"" + ls_Image + "\"" +
                    "          }" +
                    "      }";

            if (post_sw == "YES")
            {
                //Check amount of items
                ret_obj = wsf.post_webrequest(full_url, json_body);

                if (ret_obj.status == 0)
                {
                    try
                    {
                        string formated_json = ret_obj.message.Replace("null", "");
                        //Convert Json to product class
                        product_template.products product_details = JsonConvert.DeserializeObject<product_template.products>(formated_json);

                        string sql_insert = "UPDATE shopify_datafeed " +
                        "SET shopify_id = '" + product_details.id + "' " +
                        ", variant_id = '" + product_details.variants[1].id + "' " +
                        ", modified = 0 " +
                        ", date_modified = now() " +
                        "WHERE shopify_id = '0' " +
                        "AND variant_id = '0' " +
                        "AND item_id = '" + ls_item_id + "'; ";

                        sql_insert = sql_insert + "COMMIT; ";
                        int insert_check = db.sqlInsert(sql_insert);

                        if (insert_check != 0)
                        {
                            //Audit Request
                            audit_api(country_sw, "Error: Update shopify_datafeed", "0", "Failed", sql_insert, ls_item_id);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            ret_obj.parm_extra = json_body;

            return ret_obj;
        }
        public void audit_api(string country_sw, string api_function, string api_status, string api_message, string api_body, string api_response)
        {
            DbCode db = new DbCode();
            db.set_db_country(country_sw);
            string sql_audit = "";

            sql_audit = "INSERT INTO audit_api(api_name, api_function, api_status, api_message, api_request, api_response) " +
            "VALUES('VSB_Shopify_API', '" + api_function + "','" + api_status + "' ,'" + api_message + "', '" + api_body + "', '" + api_response + "'); " +
            "COMMIT; ";

            db.sqlInsert(sql_audit);

            db.close_connection();
        }
    }
}