using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSB_Shopify_API
{
    public class product_template
    {
        public class RootObject
        {
            public products[] product_list { get; set; }
        }

        public class products
        {
            public string body_html { get; set; }
            public DateTime created_at { get; set; }
            public string handle { get; set; }
            public int id { get; set; }
            public Image[] images { get; set; }
            public Options options { get; set; }
            public string product_type { get; set; }
            public DateTime published_at { get; set; }
            public string published_scope { get; set; }
            public string status { get; set; }
            public string tags { get; set; }
            public string template_suffix { get; set; }
            public string title { get; set; }
            public DateTime updated_at { get; set; }
            public Variant[] variants { get; set; }
            public string vendor { get; set; }
        }

        public class Options
        {
            public int id { get; set; }
            public int product_id { get; set; }
            public string name { get; set; }
            public int position { get; set; }
            //public option_values[] values { get; set; }
        }

        public class option_values
        {
        }

        public class Image
        {
            public int id { get; set; }
            public int product_id { get; set; }
            public int position { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string src { get; set; }
            public Variant_Ids[] variant_ids { get; set; }
        }

        public class Variant_Ids
        {
        }

        public class Variant
        {
            public string barcode { get; set; }
            public object compare_at_price { get; set; }
            public DateTime created_at { get; set; }
            public string fulfillment_service { get; set; }
            public int grams { get; set; }
            public float weight { get; set; }
            public string weight_unit { get; set; }
            public int id { get; set; }
            public int inventory_item_id { get; set; }
            public string inventory_management { get; set; }
            public string inventory_policy { get; set; }
            public int inventory_quantity { get; set; }
            public string option1 { get; set; }
            public string option2 { get; set; }
            public string option3 { get; set; }

            public int position { get; set; }
            public float price { get; set; }
            public int product_id { get; set; }
            public bool requires_shipping { get; set; }
            public string sku { get; set; }
            public bool taxable { get; set; }
            public string title { get; set; }
            public DateTime updated_at { get; set; }
        }

    }

}