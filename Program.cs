using System.Text;

namespace CET2007_Week3_Workshop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            MenuItem[] menu =
            {
                new Meal("Burger", 5.99),
                new Meal("Pizza", 7.49),
                new Drink("Cola", 1.50, "500ml")
            };
            Checkout checkout = new Checkout();

            // Show the full menu (polymorphism: same Describe() call, different results)
            Console.WriteLine("=== Full Menu ===");
            for (int i = 0; i < menu.Length; i++)
            {
                Console.Write($"{i + 1}. ");
                menu[i].Describe();
            }
            Console.WriteLine();

            // User chooses the items and quantities
            int[] quantities = new int[menu.Length];
            List<string> orderLines = new List<string>();
            bool hasMeal = false;
            bool hasDrink = false;

            for (int i = 0; i < menu.Length; i++)
            {
                quantities[i] = ReadQuantity($"Quantity of {menu[i].Name} (0 = none): ");
                if (quantities[i] > 0)
                {
                    orderLines.Add($"{quantities[i]} x {menu[i].Name}");
                    if (menu[i] is Meal)
                    {
                        hasMeal = true;
                    }
                    else if (menu[i] is Drink)
                    {
                        hasDrink = true;
                    }
                }
            }

            if (orderLines.Count == 0)
            {
                Console.WriteLine("No items selected.");
                return;
            }

            // Higher discount when buying both a meal and a drink, lower discount for one type only
            double discountPercent = (hasMeal && hasDrink) ? 10 : 5;

            double total = 0;
            for (int i = 0; i < menu.Length; i++)
            {
                if (quantities[i] > 0)
                {
                    total += checkout.PlaceOrder(menu[i], quantities[i], discountPercent);
                }
            }

            string orderText = string.Join(", ", orderLines);

            Console.WriteLine();
            Console.WriteLine("=== Order Summary ===");
            foreach (string line in orderLines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine($"Discount: {discountPercent}%");
            Console.WriteLine($"Total: £{total:F2}");
            Console.WriteLine();

            // Payment and notification
            int paymentChoice = ReadOption("Choose payment method: (1 = Card, 2 = Cash)");
            int notificationChoice = ReadOption("Choose notification type: (1 = Email, 2 = SMS)");

            IPaymentProcessor processor;
            if (paymentChoice == 1)
            {
                processor = new CardPayment();
            }
            else
            {
                processor = new CashPayment();
            }

            INotifier notifier;
            if (notificationChoice == 1)
            {
                notifier = new EmailNotifier();
            }
            else
            {
                notifier = new SmsNotifier();
            }

            string methodName = paymentChoice == 1 ? "Card" : "Cash";
            int confirm = ReadOption($"Are you sure you want to pay by {methodName}? (1 = Yes, 2 = No)");
            if (confirm != 1)
            {
                Console.WriteLine("Payment cancelled.");
                return;
            }

            processor.ProcessPayment(total, orderText);
            notifier.Notify($"Order confirmed for {orderText}. Total £{total:F2}.");
        }

        static int ReadQuantity(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity >= 0 && quantity <= 50)
                {
                    return quantity;
                }
                Console.WriteLine("Please enter a whole number between 0 and 50.");
            }
        }

        static int ReadOption(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                if (input == "1")
                {
                    return 1;
                }
                if (input == "2")
                {
                    return 2;
                }
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }
        }
    }
}
