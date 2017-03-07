using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RefactoringKata
{
    public class OrdersWriter
    {
        private Orders _orders;

        public OrdersWriter(Orders orders)
        {
            _orders = orders;
        }

        public string GetContents()
        {
            return orderJsonFormat();
        }

        private string orderJsonFormat()
        {
            Dictionary<string, string> orderDetails = new Dictionary<string, string>()
            {
                {"orders", generateStringWithbrackets(generateOrderInfo())}
            };

            return generateJsonObject(orderDetails);

        }

        private string generateOrderInfo()
        {

            var sb = new StringBuilder();

            for (var i = 0; i < _orders.GetOrdersCount(); i++)
            {
                sb.Append(generateSingleOrderInfo(_orders.GetOrder(i)));
                if (i < _orders.GetOrdersCount() - 1) sb.Append(", ");
            }

            return sb.ToString();
        }

        private string generateSingleOrderInfo(Order order)
        {
            Dictionary<string, string> orderDetails = new Dictionary<string, string>()
            {
                {"id",order.GetOrderId().ToString()},
                {"products", generateStringWithbrackets(generateProductsInfo(order))}
            };

            return generateJsonObject(orderDetails);
        }

        private string generateProductsInfo(Order order)
        {
            var sb = new StringBuilder();
            for (var j = 0; j < order.GetProductsCount(); j++)
            {
                sb.Append(generateSingleProductInfo(order.GetProduct(j)));
            }

            return sb.ToString();
        }

        private string generateSingleProductInfo(Product product)
        {
            Dictionary<string, string> productDetails = new Dictionary<string, string>()
            {
                    {"code", product.Code},
                    {"color", product.getColor()},
                    {"size", product.getSize()},
                    {"price", product.Price.ToString()},
                    {"currency", product.Currency}
            };

            if (productSizeNotApplicable(productDetails["size"]))
                productDetails.Remove("size");

            return generateJsonObject(productDetails);
        }

        private string generateJsonObject(Dictionary<string, string> objects)
        {
            var sb = new StringBuilder();
            foreach (var singleobject in objects)
            {
                sb.Append(genarateProperty(singleobject.Key, singleobject.Value));
                if (singleobject.Key != objects.Keys.Last()) sb.Append(", ");
            }

            return generateStringWithBraces(sb.ToString());
        }

        private string genarateProperty(string key, string value)
        {
            double number;
            value = double.TryParse(value, out number) || isJsonArrayOrObject(value) ? value  : genarateStringWithDoubleQuotes(value);
            return genarateStringWithDoubleQuotes(key) + ": " + value;
        }

        private bool isJsonArrayOrObject(string stringValue)
        {
            return stringValue.ElementAt(0) == '[' || stringValue.ElementAt(0) == '{';
        }

        private string generateStringWithbrackets(string stringValue)
        {
            return "[" + stringValue + "]";
        }

        private string generateStringWithBraces(string stringValue)
        {
            return "{" + stringValue + "}";
        }

        private string genarateStringWithDoubleQuotes(string stringValue)
        {
            return "\"" + stringValue + "\"";
        }

        private bool productSizeNotApplicable(string sizeValue)
        {
            return sizeValue == (Product.SIZE_NOT_APPLICABLE).ToString();
        }
    }
}