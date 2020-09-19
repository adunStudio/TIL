using System;

namespace Example02
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Movie avatar = 
                new Movie
                (
                    title: "아바타",
                    runningTime: 120,
                    fee: Money.Wons(10000),
                    discountPolicy: new AmountDiscountPolicy
                    (
                        Money.Wons(800), 
                        new SequenceCondition(1),
                        new SequenceCondition(10),
                        new PeriodCondition(DayOfWeek.Monday, new TimeSpan(10, 0, 0), new TimeSpan(11, 59, 00)), 
                        new PeriodCondition(DayOfWeek.Tuesday, new TimeSpan(10, 0, 0), new TimeSpan(20, 59, 00)
                    )
                ));
            
            Movie titanic = 
                new Movie
                (
                    title: "타이타닉",
                    runningTime: 180,
                    fee: Money.Wons(11000),
                    discountPolicy: new PercentDiscountPolicy
                    (
                        0.1d, 
                        new SequenceCondition(2),
                        new PeriodCondition(DayOfWeek.Tuesday, new TimeSpan(14, 0, 0), new TimeSpan(16, 59, 00)), 
                        new PeriodCondition(DayOfWeek.Tuesday, new TimeSpan(10, 0, 0), new TimeSpan(13, 59, 00)
                        )
                    ));
        }
    }
}