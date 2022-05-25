﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

        public base_return process_products(List<product_template.products> product_list, string country_sw)
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


            foreach (var product_line in product_list)
            {
                foreach (var variant_line in product_line.variants)
                {
                    //clear scripts
                    sql_insert = "";

                    //Add Product to database
                    sql_insert = "INSERT INTO shopify_datafeed(shopify_id, variant_id, item_id, item_type, entity_id, sku_no, barcode, isbn_number, weight, published, selling_price, online_price, availibility, dept_id, department, cat_id, product_group, contributor, title, edition, publisher, format, release_date, genre, modified, date_created, date_published, date_modified, date_sent, current_status, inventory_qty, inventory_qty_old)" +
                    "ON EXISTING UPDATE VALUES('" + variant_line.product_id + "','" + variant_line.id + "',NULL,NULL,NULL,NULL,'" + variant_line.barcode + "',NULL,NULL,NULL," + variant_line.price + ",0.00,NULL,NULL,NULL,NULL,NULL,NULL," + variant_line.title + ",NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,'" + DateTime.Now.ToString() + "',NULL,'" + product_line.status + "','" + variant_line.inventory_quantity + "',NULL); ";
                }
            }

            sql_insert = sql_insert + "COMMIT; ";

            insert_check = db.sqlInsert(sql_insert);

            if (insert_check != 0)
            {
                //Audit Insert Script
            }

            if (sql_return == "1")
            {
                //Update status of order if required
            }

            db.close_connection();

            return ret_obj;
        }



        public base_return update_products(List<product_template.products> product_list, string country_sw)
        {
            base_return ret_obj = new base_return();

            DbCode dbInt = new DbCode();
            DataTable dt;
            OdbcDataReader dr_shopify_items;

            ret_obj.status = 0;
            ret_obj.message = "";
            ret_obj.parm_extra = "";

            dbInt.set_db_country(country_sw);

            //check if database connection is active
            int db_active = dbInt.checkDBConnection();
            if (db_active == -1)
            {
                ret_obj.status = -999;
                ret_obj.message = "Error Connection to database";
            }

            dr_shopify_items = dbInt.getShopifyDataFeedItems();

            try
            {
                //loop through the rows
                while (dr_shopify_items.Read())
                {
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

                }
            }
            catch (Exception ex)
            {
                ret_obj.status = -1;
                ret_obj.message = "";
                ret_obj.parm_extra = "";
            }





            dbInt.close_connection();

            return ret_obj;
        }
    }
}