namespace Example02
{
    public class PercentDiscountPolicy : DefaultDiscountPolicy
    {
        private readonly double m_Percent;

        public PercentDiscountPolicy(double percent, params DiscountCondition[] conditions) : base(conditions)
        {
            m_Percent = percent;
        }
        
        protected override Money GetDiscountAmount(Screening screening)
        {
            return screening.GetMovieFee().Times(m_Percent);
        }
    }
}