namespace CET2007_Week3_Workshop
{
    public class Drink : MenuItem
    {
        public string Size { get; }

        public Drink(string name, double price, string size) : base(name, price)
        {
            Size = size;
        }

        public override void Describe()
        {
            Console.WriteLine($"Drink: {Name} ({Size}) – £{Price:F2}");
        }

        public override double CalculateTotal(int quantity)
        {
            return Price * quantity * 1.05;
        }
    }
}
