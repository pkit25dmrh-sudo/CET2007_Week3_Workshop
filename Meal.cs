namespace CET2007_Week3_Workshop
{
    public class Meal : MenuItem
    {
        public Meal(string name, double price) : base(name, price)
        {
        }

        public override void Describe()
        {
            Console.WriteLine($"Meal: {Name} – £{Price:F2}");
        }

        public override double CalculateTotal(int quantity)
        {
            return Price * quantity * 1.10;
        }
    }
}
