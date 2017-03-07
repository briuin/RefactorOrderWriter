using System;
namespace RefactoringKata
{
    enum productColor
    {
        blue = 1,
        red = 2,
        yellow = 3
    }

    enum productSize
    {
        XS = 1,
        S = 2,
        M = 3,
        L = 4,
        XL = 5,
        XXL = 6
    }

    public class Product
    {
        
        public static int SIZE_NOT_APPLICABLE = -1;

        public string Code { get; set; }
        public int Color { get; set; }
        public int Size { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }

        public Product(string code, int color, int size, double price, string currency)
        {
            Code = code;
            Color = color;
            Size = size;
            Price = price;
            Currency = currency;
        }

        public string getSize()
        {
            return ((productSize)(this.Size)).ToString() ?? "Invalid Size";
        }

        public string getColor()
        {
            return ((productColor)(this.Color)).ToString() ?? "no color";
        }
    }
}
