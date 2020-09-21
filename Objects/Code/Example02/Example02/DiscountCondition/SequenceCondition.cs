namespace Example02
{
    public class SequenceCondition : DiscountCondition
    {
        private int m_Sequence;

        public SequenceCondition(int sequence)
        {
            m_Sequence = sequence;
        }

        public bool IsSatisfiedBy(Screening screening)
        {
            return screening.IsSequence(m_Sequence);
        }
    }
}