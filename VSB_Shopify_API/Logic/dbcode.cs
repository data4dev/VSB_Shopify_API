using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace VSB_Shopify_API
{
    public class DbCode
    {
        //globals
        OdbcConnection conn = new OdbcConnection();
        string gs_connString;
        readonly int gi_conn_timeout = 300;

        public string dbCode(int country_sw)
        {
            //read connection string
            if (country_sw == 1)
            {
                gs_connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            }

            if (country_sw == 2)
            {
                gs_connString = ConfigurationManager.ConnectionStrings["DbConnection_bots"].ConnectionString;
            }

            if (country_sw == 3)
            {
                gs_connString = ConfigurationManager.ConnectionStrings["DbConnection_nam"].ConnectionString;
            }

            return gs_connString;
        }
        public void close_connection()
        {
            conn.Close();
        }
        public void set_db_country(string country_sw)
        {

            //SA
            if (country_sw == "1")
            {
                gs_connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
                conn.ConnectionString = gs_connString;
                conn.ConnectionTimeout = gi_conn_timeout;
                conn.Open();

            }

            //BOTSWANA
            if (country_sw == "2")
            {
                gs_connString = ConfigurationManager.ConnectionStrings["DbConnection_bots"].ConnectionString;
                conn.ConnectionString = gs_connString;
                conn.ConnectionTimeout = gi_conn_timeout;
                conn.Open();
            }

            //NAMIBIA
            if (country_sw == "3")
            {
                gs_connString = ConfigurationManager.ConnectionStrings["DbConnection_nam"].ConnectionString;
                conn.ConnectionString = gs_connString;
                conn.ConnectionTimeout = gi_conn_timeout;
                conn.Open();
            }

            //SWAZILAND
            if (country_sw == "4")
            {
                gs_connString = ConfigurationManager.ConnectionStrings["DbConnection_swa"].ConnectionString;
                conn.ConnectionString = gs_connString;
                conn.ConnectionTimeout = gi_conn_timeout;
                conn.Open();
            }
        }
        public int sqlInsert(string sql)
        {
            //OdbcConnection conn = new OdbcConnection();
            //conn.ConnectionString = gs_connString;
            //conn.ConnectionTimeout = gi_conn_timeout;
            string test = conn.State.ToString();

            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }

            try
            {
                //conn.Open();
                using (OdbcCommand com = new OdbcCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }

                // conn.Dispose();//ROCCO release DB locks
                //conn.Close();
            }
            catch (Exception ex)
            {

                string message = ex.Message.ToString();
                conn.Close();
                return -1;
            }
            return 0;
        }
        public int sqlInsert_final(string sql)
        {
            //OdbcConnection conn = new OdbcConnection();
            OdbcTransaction trans = null;

            //conn.ConnectionString = gs_connString;
            //conn.ConnectionTimeout = gi_conn_timeout;

            string test = conn.State.ToString();

            if (conn.State != ConnectionState.Open)
            {

                conn.Close();
                conn.Open();
            }

            try
            {
                //conn.Open();

                trans = conn.BeginTransaction();

                using (OdbcCommand com = new OdbcCommand(sql, conn, trans))
                {
                    com.ExecuteNonQuery();
                }

                trans.Commit();

                //conn.Dispose();//ROCCO release DB locks
                //conn.Close();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                conn.Close();
                string message = ex.Message.ToString();
                return -1;
            }

            //return
            return 0;
        }
        public string odbcSQLLookupValue(string sql)
        {
            OdbcDataReader reader;
            Dictionary<string, string> dic_online_customers = new Dictionary<string, string>();

            //OdbcConnection conn = new OdbcConnection();
            //conn.ConnectionString = gs_connString;
            //conn.ConnectionTimeout = gi_conn_timeout;

            string test = conn.State.ToString();

            if (conn.State != ConnectionState.Open)
            {

                conn.Close();
                conn.Open();
            }

            string value = "";

            try
            {
                //conn.Open();
                using (OdbcCommand com = new OdbcCommand(sql, conn))
                {
                    //com.Parameters.AddWithValue("@var", paramWord);
                    reader = com.ExecuteReader();
                }

                while (reader.Read())
                {
                    value = reader[0].ToString();
                }
                //conn.Dispose();//ROCCO release DB locks
                conn.Close();
            }
            catch (Exception ex)
            {
                //string test = "";
                //test = ex.Message.ToString();
                //return test;
                //throw;
                conn.Close();
                return "-1";
            }

            return value;
        }
        public DataTable odbcSQLLookupDT(string sql)
        {
            // OdbcConnection conn = new OdbcConnection();
            //conn.ConnectionString = gs_connString;
            //conn.ConnectionTimeout = gi_conn_timeout;

            string test = conn.State.ToString();

            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }

            DataTable dt = new DataTable();
            try
            {
                using (OdbcDataAdapter adapter = new OdbcDataAdapter(sql, conn))
                {
                    // conn.Open();

                    adapter.Fill(dt);

                }

                //conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                string test1 = ex.Message.ToString();
                //test = ex.Message.ToString();
                //return test;
                //throw;
                //return -1;
                return dt;
            }


        }
    }
}