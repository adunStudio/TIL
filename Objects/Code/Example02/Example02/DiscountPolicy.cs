using System;
using System.Collections.Generic;

namespace Example02
{
    public abstract class DiscountPolicy
    {
        private List<DiscountCondition> m_Conditions;

        public DiscountPolicy(params DiscountCondition[] discountConditions)
        {
            m_Conditions = new List<DiscountCondition>(discountConditions);
        }

        public Money CalculateDiscountAmount(Screening screening)
        {
            if (m_Conditions is null) return Money.Zero;
            
            foreach (var condition in m_Conditions)
            {
                if (condition.IsSatisfiedBy(screening))
                {
                    return GetDiscountAmount(screening);
                }
            }
            
            return Money.Zero;
        }

        protected abstract Money GetDiscountAmount(Screening screening);
    }
}