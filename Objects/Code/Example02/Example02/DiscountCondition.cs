namespace Example02
{
    public interface DiscountCondition
    {
        bool IsSatisfiedBy(Screening screening);
    }
}