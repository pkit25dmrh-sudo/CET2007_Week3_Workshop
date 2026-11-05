namespace CET2007_Week3_Workshop
{
    public abstract class MenuItem
    {
        public string Name { get; }
        public double Price { get; }

        protected MenuItem(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public virtual void Describe()
        {
            Console.WriteLine($"{Name} – £{Price:F2}");
        }

        public abstract double CalculateTotal(int quantity);
    }
}
