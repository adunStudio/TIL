using System;
using System.Collections.Generic;

namespace Example02
{
    public interface DiscountPolicy
    {
        Money CalculateDiscountAmount(Screening screening);
    }
}