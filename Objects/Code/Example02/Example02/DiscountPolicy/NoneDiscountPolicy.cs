namespace Example02
{
    public class NoneDiscountPolicy : DiscountPolicy
    {
        public Money CalculateDiscountAmount(Screening screening)
        {
            return Money.Zero;
        }
    }
}