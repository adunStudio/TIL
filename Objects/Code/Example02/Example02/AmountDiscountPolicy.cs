namespace Example02
{
    public class AmountDiscountPolicy : DiscountPolicy
    {
        private readonly Money m_DiscountAmount;

        public AmountDiscountPolicy(Money discountAmount, params DiscountCondition[] conditions) : base(conditions)
        {
            m_DiscountAmount = discountAmount;
        }
        
        protected override Money GetDiscountAmount(Screening screening)
        {
            return m_DiscountAmount;
        }
    }
}