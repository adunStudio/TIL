namespace Example02
{
    public class Movie
    {
        private string m_Title;
        private float m_RunningTime;
        private Money m_Fee;
        private DiscountPolicy m_DiscountPolicy;

        public Movie(string title, float runningTime, Money fee, DiscountPolicy discountPolicy)
        {
            m_Title = title;
            m_RunningTime = runningTime;
            m_Fee = fee;
            m_DiscountPolicy = discountPolicy;
        }

        public Money GetFee() => m_Fee;

        public Money CalculateMovieFee(Screening screening)
        {
            
            return m_Fee.Minus(m_DiscountPolicy.CalculateDiscountAmount(screening));
        }
    }
}