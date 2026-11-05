namespace CET2007_Week3_Workshop
{
    public class CardPayment : IPaymentProcessor
    {
        public void ProcessPayment(double amount, string reference)
        {
            Console.WriteLine($"Card payment of £{amount:F2} processed for {reference}.");
        }
    }
}
