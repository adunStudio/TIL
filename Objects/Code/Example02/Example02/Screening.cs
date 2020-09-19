using System;

namespace Example02
{
    public class Screening
    {
        private Movie m_Movie;
        private int m_Sequence;
        private DateTime m_WhenScreened;

        public Screening(Movie movie, int sequence, DateTime whenScreened)
        {
            m_Movie = movie;
            m_Sequence = sequence;
            m_WhenScreened = whenScreened;
        }

        public Reservation Reserve(Customer customer, int audienceCount)
        {
            return new Reservation(customer, this, CalculateFee(audienceCount), audienceCount);
        }

        private Money CalculateFee(int audienceCount)
        {
            return m_Movie.CalculateMovieFee(this).Times(audienceCount);
        }
        
        public DateTime GetStartTime() => m_WhenScreened;
        public bool IsSequence(int sequence) => m_Sequence == sequence;
        public Money GetMovieFee() => m_Movie.GetFee();
    }
}