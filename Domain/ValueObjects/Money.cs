namespace EventManagement11.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; private set; }
        public string Currency => "ЕUR";

        protected Money()
        {
        }

        public Money(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(
                    "Сумата не може да бъде отрицателна.");
            }

            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Amount:F2} {Currency}";
        }
    }
}
