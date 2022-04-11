using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSB_Shopify_API
{
    public class orders_template
    {

        public class Rootobject
        {
            public Order[] orders { get; set; }
        }

        public class Order
        {
            public long id { get; set; }
            public string admin_graphql_api_id { get; set; }
            public int app_id { get; set; }
            public string browser_ip { get; set; }
            public bool buyer_accepts_marketing { get; set; }
            public object cancel_reason { get; set; }
            public object cancelled_at { get; set; }
            public string cart_token { get; set; }
            public long checkout_id { get; set; }
            public string checkout_token { get; set; }
            public Client_Details client_details { get; set; }
            public object closed_at { get; set; }
            public bool confirmed { get; set; }
            public string contact_email { get; set; }
            public DateTime created_at { get; set; }
            public string currency { get; set; }
            public string current_subtotal_price { get; set; }
            public Current_Subtotal_Price_Set current_subtotal_price_set { get; set; }
            public string current_total_discounts { get; set; }
            public Current_Total_Discounts_Set current_total_discounts_set { get; set; }
            public object current_total_duties_set { get; set; }
            public string current_total_price { get; set; }
            public Current_Total_Price_Set current_total_price_set { get; set; }
            public string current_total_tax { get; set; }
            public Current_Total_Tax_Set current_total_tax_set { get; set; }
            public string customer_locale { get; set; }
            public object device_id { get; set; }
            public object[] discount_codes { get; set; }
            public string email { get; set; }
            public bool estimated_taxes { get; set; }
            public string financial_status { get; set; }
            public string fulfillment_status { get; set; }
            public string gateway { get; set; }
            public string landing_site { get; set; }
            public object landing_site_ref { get; set; }
            public object location_id { get; set; }
            public string name { get; set; }
            public object note { get; set; }
            public object[] note_attributes { get; set; }
            public int number { get; set; }
            public int order_number { get; set; }
            public string order_status_url { get; set; }
            public object original_total_duties_set { get; set; }
            public string[] payment_gateway_names { get; set; }
            public string phone { get; set; }
            public string presentment_currency { get; set; }
            public DateTime processed_at { get; set; }
            public string processing_method { get; set; }
            public string reference { get; set; }
            public string referring_site { get; set; }
            public string source_identifier { get; set; }
            public string source_name { get; set; }
            public object source_url { get; set; }
            public string subtotal_price { get; set; }
            public Subtotal_Price_Set subtotal_price_set { get; set; }
            public string tags { get; set; }
            public object[] tax_lines { get; set; }
            public bool taxes_included { get; set; }
            public bool test { get; set; }
            public string token { get; set; }
            public string total_discounts { get; set; }
            public Total_Discounts_Set total_discounts_set { get; set; }
            public string total_line_items_price { get; set; }
            public Total_Line_Items_Price_Set total_line_items_price_set { get; set; }
            public string total_outstanding { get; set; }
            public string total_price { get; set; }
            public Total_Price_Set total_price_set { get; set; }
            public string total_price_usd { get; set; }
            public Total_Shipping_Price_Set total_shipping_price_set { get; set; }
            public string total_tax { get; set; }
            public Total_Tax_Set total_tax_set { get; set; }
            public string total_tip_received { get; set; }
            public int total_weight { get; set; }
            public DateTime updated_at { get; set; }
            public object user_id { get; set; }
            public Billing_Address billing_address { get; set; }
            public Customer customer { get; set; }
            public object[] discount_applications { get; set; }
            public Fulfillment[] fulfillments { get; set; }
            public Line_Items[] line_items { get; set; }
            public object payment_terms { get; set; }
            public Refund[] refunds { get; set; }
            public Shipping_Address shipping_address { get; set; }
            public Shipping_Lines[] shipping_lines { get; set; }
        }

        public class Client_Details
        {
            public string accept_language { get; set; }
            public int? browser_height { get; set; }
            public string browser_ip { get; set; }
            public int? browser_width { get; set; }
            public object session_hash { get; set; }
            public string user_agent { get; set; }
        }

        public class Current_Subtotal_Price_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }

        public class Shop_Money
        {
            public string amount { get; set; }
            public string currency_code { get; set; }
        }

        public class Presentment_Money
        {
            public string amount { get; set; }
            public string currency_code { get; set; }
        }

        public class Current_Total_Discounts_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }

        public class Current_Total_Price_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }


        public class Current_Total_Tax_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }

        public class Subtotal_Price_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }

        public class Total_Discounts_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }

        public class Total_Line_Items_Price_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }


        public class Total_Price_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }


        public class Total_Shipping_Price_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }


        public class Total_Tax_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }


        public class Billing_Address
        {
            public string first_name { get; set; }
            public string address1 { get; set; }
            public string phone { get; set; }
            public string city { get; set; }
            public string zip { get; set; }
            public string province { get; set; }
            public string country { get; set; }
            public string last_name { get; set; }
            public string address2 { get; set; }
            public string company { get; set; }
            public float? latitude { get; set; }
            public float? longitude { get; set; }
            public string name { get; set; }
            public string country_code { get; set; }
            public string province_code { get; set; }
        }

        public class Customer
        {
            public long id { get; set; }
            public string email { get; set; }
            public bool accepts_marketing { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public int orders_count { get; set; }
            public string state { get; set; }
            public string total_spent { get; set; }
            public long last_order_id { get; set; }
            public object note { get; set; }
            public bool verified_email { get; set; }
            public object multipass_identifier { get; set; }
            public bool tax_exempt { get; set; }
            public string phone { get; set; }
            public string tags { get; set; }
            public string last_order_name { get; set; }
            public string currency { get; set; }
            public DateTime accepts_marketing_updated_at { get; set; }
            public string marketing_opt_in_level { get; set; }
            public object[] tax_exemptions { get; set; }
            public Email_Marketing_Consent email_marketing_consent { get; set; }
            public Sms_Marketing_Consent sms_marketing_consent { get; set; }
            public string admin_graphql_api_id { get; set; }
            public Default_Address default_address { get; set; }
        }

        public class Email_Marketing_Consent
        {
            public string state { get; set; }
            public string opt_in_level { get; set; }
            public object consent_updated_at { get; set; }
        }

        public class Sms_Marketing_Consent
        {
            public string state { get; set; }
            public string opt_in_level { get; set; }
            public object consent_updated_at { get; set; }
            public string consent_collected_from { get; set; }
        }

        public class Default_Address
        {
            public long id { get; set; }
            public long customer_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string company { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string country { get; set; }
            public string zip { get; set; }
            public string phone { get; set; }
            public string name { get; set; }
            public string province_code { get; set; }
            public string country_code { get; set; }
            public string country_name { get; set; }
            public bool _default { get; set; }
        }

        public class Shipping_Address
        {
            public string first_name { get; set; }
            public string address1 { get; set; }
            public string phone { get; set; }
            public string city { get; set; }
            public string zip { get; set; }
            public string province { get; set; }
            public string country { get; set; }
            public string last_name { get; set; }
            public string address2 { get; set; }
            public string company { get; set; }
            public float? latitude { get; set; }
            public float? longitude { get; set; }
            public string name { get; set; }
            public string country_code { get; set; }
            public string province_code { get; set; }
        }

        public class Fulfillment
        {
            public long id { get; set; }
            public string admin_graphql_api_id { get; set; }
            public DateTime created_at { get; set; }
            public long location_id { get; set; }
            public string name { get; set; }
            public long order_id { get; set; }
            public Origin_Address origin_address { get; set; }
            public Receipt receipt { get; set; }
            public string service { get; set; }
            public object shipment_status { get; set; }
            public string status { get; set; }
            public object tracking_company { get; set; }
            public object tracking_number { get; set; }
            public object[] tracking_numbers { get; set; }
            public object tracking_url { get; set; }
            public object[] tracking_urls { get; set; }
            public DateTime updated_at { get; set; }
            public Line_Items[] line_items { get; set; }
        }

        public class Origin_Address
        {
        }

        public class Receipt
        {
        }

        public class Line_Items
        {
            public long id { get; set; }
            public string admin_graphql_api_id { get; set; }
            public int fulfillable_quantity { get; set; }
            public string fulfillment_service { get; set; }
            public string fulfillment_status { get; set; }
            public bool gift_card { get; set; }
            public int grams { get; set; }
            public string name { get; set; }
            public Origin_Location origin_location { get; set; }
            public string price { get; set; }
            public Price_Set price_set { get; set; }
            public bool product_exists { get; set; }
            public long product_id { get; set; }
            public object[] properties { get; set; }
            public int quantity { get; set; }
            public bool requires_shipping { get; set; }
            public string sku { get; set; }
            public bool taxable { get; set; }
            public string title { get; set; }
            public string total_discount { get; set; }
            public Total_Discount_Set total_discount_set { get; set; }
            public long variant_id { get; set; }
            public string variant_inventory_management { get; set; }
            public string variant_title { get; set; }
            public string vendor { get; set; }
            public Tax_Lines[] tax_lines { get; set; }
            public object[] duties { get; set; }
            public object[] discount_allocations { get; set; }
        }

        public class Origin_Location
        {
            public long id { get; set; }
            public string country_code { get; set; }
            public string province_code { get; set; }
            public string name { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string zip { get; set; }
        }

        public class Price_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }

        public class Total_Discount_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }


        public class Tax_Lines
        {
            public bool channel_liable { get; set; }
            public string price { get; set; }
            public Price_Set price_set { get; set; }
            public float rate { get; set; }
            public string title { get; set; }
        }

        public class Refund
        {
            public long id { get; set; }
            public string admin_graphql_api_id { get; set; }
            public DateTime created_at { get; set; }
            public string note { get; set; }
            public long order_id { get; set; }
            public DateTime processed_at { get; set; }
            public bool restock { get; set; }
            public Total_Duties_Set total_duties_set { get; set; }
            public long user_id { get; set; }
            public Order_Adjustments[] order_adjustments { get; set; }
            public Transaction[] transactions { get; set; }
            public object[] refund_line_items { get; set; }
            public object[] duties { get; set; }
        }

        public class Total_Duties_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }

        public class Order_Adjustments
        {
            public long id { get; set; }
            public string amount { get; set; }
            public Amount_Set amount_set { get; set; }
            public string kind { get; set; }
            public long order_id { get; set; }
            public string reason { get; set; }
            public long refund_id { get; set; }
            public string tax_amount { get; set; }
            public Tax_Amount_Set tax_amount_set { get; set; }
        }

        public class Amount_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }

        public class Tax_Amount_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }

        public class Transaction
        {
            public long id { get; set; }
            public string admin_graphql_api_id { get; set; }
            public string amount { get; set; }
            public string authorization { get; set; }
            public DateTime created_at { get; set; }
            public string currency { get; set; }
            public object device_id { get; set; }
            public object error_code { get; set; }
            public string gateway { get; set; }
            public string kind { get; set; }
            public object location_id { get; set; }
            public string message { get; set; }
            public long order_id { get; set; }
            public long parent_id { get; set; }
            public DateTime processed_at { get; set; }
            public Receipt receipt { get; set; }
            public string source_name { get; set; }
            public string status { get; set; }
            public bool test { get; set; }
            public long user_id { get; set; }
        }

        public class Shipping_Lines
        {
            public long id { get; set; }
            public string carrier_identifier { get; set; }
            public string code { get; set; }
            public object delivery_category { get; set; }
            public string discounted_price { get; set; }
            public Discounted_Price_Set discounted_price_set { get; set; }
            public string phone { get; set; }
            public string price { get; set; }
            public Price_Set price_set { get; set; }
            public object requested_fulfillment_service_id { get; set; }
            public string source { get; set; }
            public string title { get; set; }
            public object[] tax_lines { get; set; }
            public object[] discount_allocations { get; set; }
        }

        public class Discounted_Price_Set
        {
            public Shop_Money shop_money { get; set; }
            public Presentment_Money presentment_money { get; set; }
        }


    }
}