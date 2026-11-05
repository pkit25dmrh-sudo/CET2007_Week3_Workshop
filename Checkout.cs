namespace CET2007_Week3_Workshop
{
    public class Checkout
    {
        public double PlaceOrder(MenuItem item, int quantity)
        {
            return item.CalculateTotal(quantity);
        }

        public double PlaceOrder(MenuItem item, int quantity, double discountPercent)
        {
            double total = PlaceOrder(item, quantity);
            return total - (total * discountPercent / 100);
        }

        public double PlaceOrder(MenuItem item, int quantity, double discountPercent, double deliveryFee)
        {
            return PlaceOrder(item, quantity, discountPercent) + deliveryFee;
        }
    }
}
