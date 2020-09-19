namespace Example02
{
    public class Money
    {
        public static readonly Money Zero = Money.Wons(0);


        private double m_Amount;

        public static Money Wons(double amount)
        {
            return new Money(amount);
        }

        private Money(double amount)
        {
            m_Amount = amount;
        }

        public Money Plus(Money money)
        {
            return new Money(m_Amount + money.m_Amount);
        }
        
        public Money Minus(Money money)
        {
            return new Money(m_Amount - money.m_Amount);
        }

        public Money Times(double percent)
        {
            return new Money(percent * m_Amount);
        }

        public bool IsLessThen(Money otherMoney)
        {
            return m_Amount.CompareTo(otherMoney.m_Amount) < 0;
        }

        public bool IsGreaterThen(Money otherMoney)
        {
            return m_Amount.CompareTo(otherMoney.m_Amount) >= 0;
        }
    }
}