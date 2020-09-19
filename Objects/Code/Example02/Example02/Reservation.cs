namespace Example02
{
    public class Reservation
    {
        private Customer m_Customer;
        private Screening m_Screening;
        private Money m_Fee;
        private int m_AudienceCount;

        public Reservation(Customer customer, Screening screening, Money fee, int audienceCount)
        {
            m_Customer = customer;
            m_Screening = screening;
            m_Fee = fee;
            m_AudienceCount = audienceCount;
        }
    }
}